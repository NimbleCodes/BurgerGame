using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Ingredient : MonoBehaviour, ISpawnedObject
{


        //재료의 분류 (고기, 야채, 소스 등등)
    public string ingreClass;
    //재료의 이름
    public string ingreName;
    //재료의 Sprite 경로
    public Sprite ingreSprite;

    public abstract void Eaten();
    public abstract void Recycled();
    public abstract void Destroyed();

    public string getName()
    {
        return ingreName;
    }
    protected void setName(string val)
    {
        ingreName = val;
    }

    public void OnSpawn()
    {
        //do nothing
    }
}
