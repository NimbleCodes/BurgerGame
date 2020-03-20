using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ingredient : MonoBehaviour, ISpawnedObject
{
    string typeOfIngr = "UNDEF";

    public abstract void Eaten();
    public abstract void Recycled();
    public abstract void Destroyed();

    public string getName()
    {
        return typeOfIngr;
    }
    protected void setName(string val)
    {
        typeOfIngr = val;
    }

    public void OnSpawn()
    {
        //do nothing
    }
}
