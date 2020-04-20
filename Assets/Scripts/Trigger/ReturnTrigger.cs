using UnityEngine;

public class ReturnTrigger : Trigger
{
    protected override void Action(GameObject g)
    {
        g.SetActive(false);
    }
}
