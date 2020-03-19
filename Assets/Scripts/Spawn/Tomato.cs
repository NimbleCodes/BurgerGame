using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tomato : Ingredient
{
    private void Start()
    {
        setName("Tomato");
    }

    public override void Eaten()
    {
        Debug.Log(getName() + " was Eaten");
    }
    public override void Recycled()
    {
        Debug.Log(getName() + " was Recycled");
    }
    public override void Destroyed()
    {
        Debug.Log(getName() + " was Destroyed");
    }
}
