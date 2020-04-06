using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public static ObjectManager objectManager;
    
    Dictionary<string, Queue<GameObject>> objPools;
    int poolSize = 10;

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

    private void Awake()
    {
        objectManager = this;

        objPools = new Dictionary<string, Queue<GameObject>>();

        loadBurgerIngreFromJson();
        foreach(Ingr ingr in burgerIngrArr.ingrArr)
        {
            GameObject refGroupObj = new GameObject(ingr.ingrName + "IngrGroup");
            Queue<GameObject> ingrQueue = new Queue<GameObject>();
            for(int i = 0; i < poolSize; i++)
            {
                GameObject temp = Instantiate((GameObject)Resources.Load("Prefab/ingrPrefab"));
                temp.GetComponent<Ingredient>().ingrName = ingr.ingrName;
                temp.GetComponent<Ingredient>().ingrClass = ingr.ingrClass;
                temp.AddComponent<BoxCollider2D>();
                temp.transform.parent = refGroupObj.transform;
                temp.name = ingr.ingrName + "_" + i;
                temp.SetActive(false);
                ingrQueue.Enqueue(temp);
            }
            objPools.Add(ingr.ingrName, ingrQueue);
        }
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
            Debug.Log(tag);
            Debug.Log("ObjectManager: Invalid tag value from spawner");
            return null;
        }
    }
    public void GetSpawnableObjTypes(ref string[] output)
    {
        output = new string[burgerIngrArr.ingrArr.Length];
        for (int i = 0; i < output.Length; i++)
            output[i] = burgerIngrArr.ingrArr[i].ingrName;
    }
}
