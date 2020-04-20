﻿using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class IngrObjectManager : ObjectManager
{
    //JSON STRUCTS
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
    IngrArr burgerIngrArr;

    protected override void InitObjPools()
    {
        //LOAD INGR DATA FROM JSON
        string burgerIngrArrJsonStr = File.ReadAllText(Application.dataPath + "/Resources/Json/Ingredients.json");
        burgerIngrArr = JsonUtility.FromJson<IngrArr>(burgerIngrArrJsonStr);

        //INIT OBJPOOLS
        foreach(Ingr i in burgerIngrArr.ingrArr)
        {
            GameObject refParent = new GameObject(i.ingrName + "Group");
            objPools.Add(i.ingrName, new Queue<GameObject>());
            spawnableObjNames.Add(i.ingrName);
            for(int j = 0; j < poolSize; j++)
            {
                GameObject newObj = new GameObject(i.ingrName + j.ToString());
                newObj.transform.parent = refParent.transform;
                ApplyToAllObj(newObj);
                newObj.AddComponent<Ingredient>();
                newObj.GetComponent<Ingredient>().ingrName = i.ingrName;
                newObj.SetActive(false);
                objPools[i.ingrName].Enqueue(newObj);
            }
        }

    }
}
