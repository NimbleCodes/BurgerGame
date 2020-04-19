using UnityEngine;

public class ReturnTrigger : Trigger
{
    protected override void Action(GameObject g)
    {
        ObjectManager.objectManager.removeFromCurActiveList(g);
        g.SetActive(false);
    }
}
