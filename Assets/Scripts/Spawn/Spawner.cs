using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public string objTag;
    public float nextSpawnTime = 1f;
    bool spawnTimerOn = false;

    private void Start()
    {
        //disable Spawner if objTag is not specified
        if(objTag == null)
        {
            Debug.Log("Spawner: objTag not initialized.");
            GetComponent<Spawner>().enabled = false;
        }
    }

    private void Update()
    {
        if (!spawnTimerOn)
        {
            spawnTimerOn = true;
            StartCoroutine("objectSpawnTimer");
        }
    }

    private void spawnObject()
    {
        GameObject spawnedObj = ObjectManager.objectManager.getGameObject(objTag);
        spawnedObj.SetActive(true);
        spawnedObj.transform.position = gameObject.transform.position;
        spawnedObj.GetComponent<ISpawnedObject>().OnSpawn();
    }

    IEnumerator objectSpawnTimer()
    {
        yield return new WaitForSeconds(nextSpawnTime);
        spawnObject();
        spawnTimerOn = false;
    }
}
