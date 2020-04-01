using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecycleTrigger : Trigger
{
    override public void Action(GameObject g)
    {
        base.Action(g);
        g.GetComponent<Ingredient>().Recycled();
    }
}
