using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ObjectManager : MonoBehaviour
{
    //Singleton
    public static ObjectManager objectManager;
    //오브젝트 풀
    Dictionary<string, Queue<GameObject>> objPools;

    /*------------------------------재료 관련-----------------------------*/
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }
    public List<IngreArr> poolInfo;

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
    //Json을 String으로 받아온다.
    string ingreJson;
    //Ingredient 정보를 쉽게 받아오기 위한 Table
    ingreTable IngreTable;
    //Json 파일에서 버거재료 데이터 받아오기
    void getJson(){
        ingreJson = File.ReadAllText(Application.dataPath + "/Resources/Json/ingredient.json");
        IngreTable = JsonUtility.FromJson<ingreTable>(ingreJson);    
    }
    /*------------------------------재료 관련-----------------------------*/

    /*--------------------------오브젝트 풀 사용--------------------------*/
    [HideInInspector]
    public bool firstSpawn = false;
    public GameObject getGameObject(string tag)
    {
        if (objPools.ContainsKey(tag))
        {
            firstSpawn = true;
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
    /*--------------------------오브젝트 풀 사용--------------------------*/

    /*----------------------------이벤트 관련-----------------------------
    //난이도 상승 이벤트
    void OnDiffIncEvent()
    {
        //increase difficulty
        foreach (GameObject g in objPools["lettuce"])
            g.GetComponent<GravityTest>().speed = -20;
    }
    /*----------------------------이벤트 관련-----------------------------*/

    private void Awake()
    {
        objectManager = this;
        getJson();
        //오브젝트 풀 초기화
        objPools = new Dictionary<string, Queue<GameObject>>();
        foreach(var cell in IngreTable.IngreArr){
            Queue<GameObject> tempQueue = new Queue<GameObject>();
            poolInfo.Add(cell);
            for(int i=0; i<8; i++){
                GameObject tempObj = Instantiate((GameObject)Resources.Load("Prefab/IngrePrefab"));
                tempObj.name = cell.ingreName + i;
                tempObj.GetComponent<Ingredient>().initIngre(cell.ingreClass,cell.ingreName);
                tempObj.SetActive(false);
                tempQueue.Enqueue(tempObj);
            }
            objPools.Add(cell.ingreName, tempQueue);
        }
    }
    
    private void Start(){
        //EventManager.eventManager.DiffIncEvent += OnDiffIncEvent;
    }
}
