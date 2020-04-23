using UnityEngine;

public class ReturnTrigger : Trigger
{
    protected override void Action(GameObject g)
    {
        base.Action(g);
        g.SetActive(false);
    }
}
