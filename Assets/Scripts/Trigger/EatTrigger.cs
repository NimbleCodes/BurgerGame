using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatTrigger : Trigger
{
    override public void Action(GameObject g)
    {
        g.SetActive(false);
        EventManager.eventManager.Invoke_IngrEatenEvent(g.name);
    }
}
