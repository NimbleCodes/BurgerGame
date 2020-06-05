using UnityEngine;

public class ObtainTrigger : Trigger
{
    public GameObject obtainEffect;

    protected override void Action(GameObject g)
    {
        base.Action(g);
        EventManager.eventManager.Invoke_IngrObtainedEvent(g.GetComponent<Ingredient>().ingrName);
        obtainEffect.GetComponent<ParticleSystem>().Play();
        g.SetActive(false);
    }
}