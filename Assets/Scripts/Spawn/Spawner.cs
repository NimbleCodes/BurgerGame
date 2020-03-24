using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //Testing
    public bool SpawnerErrorOccured = false;
    float faster = 10f;

    public string objTag;
    public float nextSpawnTime = 1f;
    bool spawnTimerOn = false;

    public int randSeed = -1;
    System.Random rand;

    /*------------------------------오브젝트 생성-------------------------------*/
    void chooseRandomIngr()
    {
        int ingrIndex = rand.Next(0, ObjectManager.objectManager.poolInfo.Count);
        objTag = ObjectManager.objectManager.poolInfo[ingrIndex].ingreName;
    }
    private void spawnObject()
    {
        chooseRandomIngr();
        GameObject spawnedObj = ObjectManager.objectManager.getGameObject(objTag);
        spawnedObj.SetActive(true);
        spawnedObj.transform.position = gameObject.transform.position;
        if(SpawnerErrorOccured){
            spawnedObj.GetComponent<Rigidbody2D>().AddForce(new Vector2(0,-faster),ForceMode2D.Impulse);
            SpawnerErrorOccured = false;
        }
        spawnedObj.GetComponent<ISpawnedObject>().OnSpawn();
    }
    IEnumerator objectSpawnTimer()
    {
        yield return new WaitForSeconds(nextSpawnTime);
        spawnObject();
        spawnTimerOn = false;
    }
    /*------------------------------오브젝트 생성-------------------------------*/
    
    private void Start()
    {
        if (randSeed == -1)
            rand = new System.Random();
        else
            rand = new System.Random(randSeed);
    }
    private void Update()
    {
        if (!spawnTimerOn)
        {
            spawnTimerOn = true;
            StartCoroutine("objectSpawnTimer");
        }
    }
}
