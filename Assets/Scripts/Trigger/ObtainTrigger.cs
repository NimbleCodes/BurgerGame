using UnityEngine;

public class ObtainTrigger : Trigger
{
    protected override void Action(GameObject g)
    {
        base.Action(g);
        EventManager.eventManager.Invoke_IngrObtainedEvent(g.GetComponent<Ingredient>().ingrName);
        g.SetActive(false);
    }
}