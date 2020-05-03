using UnityEngine;

public class ReturnTrigger : Trigger
{
    protected override void Action(GameObject g)
    {
        base.Action(g);
        EventManager.eventManager.Invoke_IngrReturnedEvent();
        g.SetActive(false);
    }
}
