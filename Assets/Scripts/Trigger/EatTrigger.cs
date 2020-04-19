using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatTrigger : Trigger
{
    override public void Action(GameObject g)
    {
        base.Action(g);
        g.GetComponent<Ingredient>().Eaten();
        EventManager.eventManager.Invoke_IngrEatenEvent(g.GetComponent<Ingredient>().getName());
    }
}
