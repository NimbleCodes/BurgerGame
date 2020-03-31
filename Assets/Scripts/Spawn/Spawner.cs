using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //Testing
    public bool SpawnerErrorOccured = false;
    float faster = 10f;
    public float spawnerSpeed = 5f;

    public string objTag;
    public float nextSpawnTime = 1f;
    bool spawnTimerOn = false;

    public int randSeed = -1;
    System.Random rand;

    /*------------------------------오브젝트 생성-------------------------------*/
    int ingrHistorySize = 5;
    Queue<string> ingrHistory;
    List<tuple> occurList;
    public class tuple
    {
        public string ingrName;
        public int occur;
        public tuple(string name)
        {
            ingrName = name;
            occur = 0;
        }
    }

    int findIgnoredIngr()
    {
        for(int i = 0; i < occurList.Count; i++)
        {
            if (occurList[i].occur == 0)
                return i;
        }
        return -1;
    }
    void chooseIngr()
    {
        int ignored;
        if (ingrHistory.Count < ingrHistorySize || (ignored = findIgnoredIngr()) == -1) {
            int ingrIndex = rand.Next(0, ObjectManager.objectManager.poolInfo.Count);
            objTag = ObjectManager.objectManager.poolInfo[ingrIndex].ingreName;
        }
        else
        {
            objTag = occurList[ignored].ingrName;
            //DEBUG
            /*
            string total = gameObject.name + ": ";
            foreach (string str in ingrHistory)
                total += (str + " ");
            Debug.Log(total);
            Debug.Log(objTag + " ignored!");
            */
        }
    }
    private void spawnObject()
    {
        chooseIngr();
        if(ingrHistory.Count < ingrHistorySize)
        {
            ingrHistory.Enqueue(objTag);
        }
        else
        {
            string remove = ingrHistory.Dequeue();
            ingrHistory.Enqueue(objTag);
            for (int i = 0; i < occurList.Count; i++)
            {
                if (occurList[i].ingrName == remove)
                    occurList[i].occur -= 1;
            }
        }
        for (int i = 0; i < occurList.Count; i++)
        {
            if (occurList[i].ingrName == objTag)
                occurList[i].occur += 1;
        }

        GameObject spawnedObj = ObjectManager.objectManager.getGameObject(objTag);
        spawnedObj.transform.position = gameObject.transform.position;
        spawnedObj.SetActive(true);
        spawnedObj.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -spawnerSpeed), ForceMode2D.Impulse);
        if (SpawnerErrorOccured){
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
        ingrHistory = new Queue<string>();
        occurList = new List<tuple>();
        for(int i = 0; i < ObjectManager.objectManager.poolInfo.Count; i++)
        {
            occurList.Add(new tuple(ObjectManager.objectManager.poolInfo[i].ingreName));
        }

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
