using System.Collections;
using UnityEngine;

public abstract class Trigger : MonoBehaviour
{
    public bool active;
    public float triggOnTime = 0.25f;
    bool triggOn;
    public float coolDownTime = 0.5f;
    bool coolDown;
    bool click;

    public string key;
    public LayerMask triggeredBy;
    public Vector2 size;

    IEnumerator TriggOnTimer()
    {
        yield return new WaitForSeconds(triggOnTime);
        triggOn = false;
        coolDown = true;
        StartCoroutine("CoolDownTimer");
    }
    IEnumerator CoolDownTimer()
    {
        yield return new WaitForSeconds(coolDownTime);
        coolDown = false;
    }

    private void Awake()
    {
        triggOn = false;
        coolDown = false;
        click = false;
    }
    private void Update()
    {
        if (active)
        {
            if(!click & !coolDown & Input.GetKeyDown(key))
            {
                click = true;
                triggOn = true;
                StartCoroutine("TriggOnTimer");
            }
            if(click & Input.GetKeyUp(key))
            {
                click = false;
            }

            if (triggOn)
            {
                Collider2D[] objInTrigger = Physics2D.OverlapBoxAll(gameObject.transform.position, size, 0, triggeredBy);
                if (objInTrigger.Length > 0)
                {
                    foreach (Collider2D c in objInTrigger)
                    {
                        Action(c.gameObject);
                    }
                }
            }
        }
    }
    protected abstract void Action(GameObject g);

    //Test code
    private void OnDrawGizmos()
    {
        ObtainTrigger temp;
        if (gameObject.TryGetComponent<ObtainTrigger>(out temp))
        {
            Gizmos.color = Color.yellow;
            if (coolDown)
                Gizmos.color = Color.red;
            if (triggOn)
                Gizmos.color = Color.green;

            Vector3 center = transform.position;
            Vector3 upperLeft = center + new Vector3(-size.x, size.y, 0);
            Vector3 upperRight = center + new Vector3(size.x, size.y, 0);
            Vector3 lowerLeft = center + new Vector3(-size.x, -size.y, 0);
            Vector3 lowerRight = center + new Vector3(size.x, -size.y, 0);

            Gizmos.DrawLine(upperLeft, upperRight);
            Gizmos.DrawLine(upperRight, lowerRight);
            Gizmos.DrawLine(lowerRight, lowerLeft);
            Gizmos.DrawLine(lowerLeft, upperLeft);
        }
    }
}
