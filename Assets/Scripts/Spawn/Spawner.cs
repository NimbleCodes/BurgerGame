using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public string objTag;
    public float nextSpawnTime = 1f;
    bool spawnTimerOn = false;

    public int randSeed = -1;
    System.Random rand;

<<<<<<< Updated upstream
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

    void chooseRandomIngr()
    {
        int ingrIndex = rand.Next(0, ObjectManager.objectManager.poolInfo.Count);
        objTag = ObjectManager.objectManager.poolInfo[ingrIndex].tag;
=======
    /*------------------------------오브젝트 생성-------------------------------*/
    int ingrHistorySize = 5;
    public Queue<string> ingrHistory;
    int checkForIgnoredIngr()
    {
        int numIngrType = ObjectManager.objectManager.poolInfo.Count;
        int[] objOccurNum = new int[numIngrType];
        //initialize
        for(int i = 0; i < numIngrType; i++)
        {
            objOccurNum[i] = 0;
        }
        //find the index of str and increase its objOccurNum value by 1
        foreach(string str in ingrHistory)
        {
            for(int i = 0; i < numIngrType; i++)
            {
                if (str == ObjectManager.objectManager.poolInfo[i].ingreName)
                    objOccurNum[i]++;
            }
        }
        //look for ingr with 0 occurences
        for (int i = 0; i < numIngrType; i++)
        {
            if (objOccurNum[i] == 0)
                return i;
        }
        return -1;
    }
    void chooseRandomIngr()
    {
        int ignored = 0;
        //if no ingr have not been spawned in the last ingrHistorySize spawns
        //spawn random ingr
        if (ingrHistory.Count >= ingrHistorySize && (ignored = checkForIgnoredIngr()) != -1)
        {
            Debug.Log(gameObject.name + ": " + objTag + " ignored!");
            objTag = ObjectManager.objectManager.poolInfo[ignored].ingreName;
        }
        //spawn the 'ignored ingr'
        else
        {
            int ingrIndex = rand.Next(0, ObjectManager.objectManager.poolInfo.Count);
            objTag = ObjectManager.objectManager.poolInfo[ingrIndex].ingreName;
        }
        if (ingrHistory.Count >= 5)
            ingrHistory.Dequeue();
        ingrHistory.Enqueue(objTag);
>>>>>>> Stashed changes
    }
    private void spawnObject()
    {
        string total = "";
        foreach (string str in ingrHistory)
        {
            total += str + " ";
        }
        Debug.Log(gameObject.name + ": " + total);

        chooseRandomIngr();
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
<<<<<<< Updated upstream
=======
    /*------------------------------오브젝트 생성-------------------------------*/
    
    private void Start()
    {
        ingrHistory = new Queue<string>();
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
>>>>>>> Stashed changes
}
