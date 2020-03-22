using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ObjectManager : MonoBehaviour
{
    
    [System.Serializable]
    public class IngreArr{
        //재료의 분류 (고기, 야채, 소스 등등)
        public string ingreClass;
        //재료의 이름
        public string ingreName;
        //재료의 Sprite 경로

    }

    [System.Serializable]
    class ingreTable{
        public IngreArr[] IngreArr;
    }


    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }
    public List<IngreArr> poolInfo;
    public static ObjectManager objectManager;

    //Json을 String으로 받아온다.
    string ingreJson;
    //Ingredient 정보를 쉽게 받아오기 위한 Table
    ingreTable IngreTable;
    
    Dictionary<string, Queue<GameObject>> objPools;
    private void Awake()
    {
        objectManager = this;
        getJson();

        objPools = new Dictionary<string, Queue<GameObject>>();
        // foreach (Pool pool in poolInfo)
        // {
        //     Queue<GameObject> tempQueue = new Queue<GameObject>();
        //     for (int i = 0; i < pool.size; i++)
        //     {
        //         GameObject temp = Instantiate(pool.prefab);
        //         temp.SetActive(false);
        //         tempQueue.Enqueue(temp);
        //     }
        //     objPools.Add(pool.tag, tempQueue);
        // }

        foreach(var cell in IngreTable.IngreArr){

            Queue<GameObject> tempQueue = new Queue<GameObject>();
            poolInfo.Add(cell);
            for(int i=0; i<5; i++){
                GameObject tempObj = Instantiate((GameObject)Resources.Load("Prefab/IngrePrefab"));
                tempObj.name = cell.ingreName + i;
                tempObj.GetComponent<Ingredient>().initIngre(cell.ingreClass,cell.ingreName);
                tempObj.SetActive(false);
                tempQueue.Enqueue(tempObj);
            }
            objPools.Add(cell.ingreName, tempQueue);
        }
    }
    
    //Json 파일 받아오기
    void getJson(){
        ingreJson = File.ReadAllText(Application.dataPath + "/Resources/Json/ingredient.json");
        IngreTable = JsonUtility.FromJson<ingreTable>(ingreJson);    
    }
    public GameObject getGameObject(string tag)
    {
        if (objPools.ContainsKey(tag))
        {
            GameObject temp = objPools[tag].Dequeue();
            objPools[tag].Enqueue(temp);
            return temp;
        }
        else
        {
            Debug.Log("ObjectManager: Invalid tag value from spawner");
            Debug.Log(tag);
            return null;
        }
    }


}
