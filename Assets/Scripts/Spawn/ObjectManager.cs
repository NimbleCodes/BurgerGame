using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    //싱글턴
    public static ObjectManager objectManager;
    
    Dictionary<string, Queue<GameObject>> objPools;
    //각 오브젝트를 몇개 생성 할 것인가
    int poolSize = 10;

    //Json 로딩 관련 구조체 및 함수
    [Serializable]
    public struct Ingr
    {
        public string ingrClass;
        public string ingrName;
    }
    [Serializable]
    struct IngrArr
    {
        public Ingr[] ingrArr;
    }
    string burgerIngrJson;
    IngrArr burgerIngrArr;
    void loadBurgerIngreFromJson()
    {
        burgerIngrJson = File.ReadAllText(Application.dataPath + "/Resources/Json/Ingredients.json");
        burgerIngrArr = JsonUtility.FromJson<IngrArr>(burgerIngrJson);
    }

    //재료 초기화
    List<GameObject> curActiveObjects;
    public void removeFromCurActiveList(GameObject g)
    {
        curActiveObjects.Remove(g);
    }
    public void disableAllActive(bool cor)
    {
        if (cor)
        {
            foreach (GameObject g in curActiveObjects)
            {
                g.SetActive(false);
            }
        }
    }

    private void Awake()
    {
        //싱글턴
        objectManager = this;

        objPools = new Dictionary<string, Queue<GameObject>>();
        //Json파일에서 재료의 종류 받아옴
        loadBurgerIngreFromJson();
        foreach(Ingr ingr in burgerIngrArr.ingrArr)
        {
            GameObject refGroupObj = new GameObject(ingr.ingrName + "IngrGroup");
            Queue<GameObject> ingrQueue = new Queue<GameObject>();
            //오브젝트 풀 초기화
            for(int i = 0; i < poolSize; i++)
            {
                GameObject temp = Instantiate((GameObject)Resources.Load("Prefab/ingrPrefab"));
                //이름 지정과 동시에 스프라이트를 가지고 옴. Ingredient 클래스 참조
                temp.GetComponent<Ingredient>().ingrName = ingr.ingrName;
                temp.GetComponent<Ingredient>().ingrClass = ingr.ingrClass;
                //스프라이트를 먼저 로딩하고 콜라이더를 넣는다 -> 자동으로 스프라이트 크기에 맞게 콜라이더 생성
                temp.AddComponent<BoxCollider2D>();
                temp.transform.parent = refGroupObj.transform;
                temp.name = ingr.ingrName + "_" + i;
                temp.SetActive(false);
                ingrQueue.Enqueue(temp);
            }
            objPools.Add(ingr.ingrName, ingrQueue);
        }

        curActiveObjects = new List<GameObject>();
    }
    private void Start()
    {
        EventManager.eventManager.BurgerCompleteEvent += disableAllActive;
    }
    //오브젝트 풀에서 오브젝트를 가지고 온다
    public GameObject getGameObject(string tag)
    {
        if (objPools.ContainsKey(tag))
        {
            GameObject temp = objPools[tag].Dequeue();
            curActiveObjects.Add(temp);
            objPools[tag].Enqueue(temp);
            return temp;
        }
        else
        {
            Debug.Log(tag);
            Debug.Log("ObjectManager: Invalid tag value from spawner");
            return null;
        }
    }
    //생성 가능한 오브젝트의 이름을 output에 저장해서 보내준다
    public void GetSpawnableObjTypes(ref string[] output)
    {
        output = new string[burgerIngrArr.ingrArr.Length];
        for (int i = 0; i < output.Length; i++)
            output[i] = burgerIngrArr.ingrArr[i].ingrName;
    }
}
