using UnityEngine;
using System.Collections;

public class Trigger: MonoBehaviour
{
    bool click = false;
    bool coolDown = false;
    bool triggOn = false;
    LayerMask triggeredBy;
    Vector2 size;

    public struct triggerVars
    {
        public string key;
        public float triggerOnTime;
        public float coolDownTime;
        public triggerVars(string _key, float _triggerOnTime, float _coolDownTime)
        {
            key = _key;
            triggerOnTime = _triggerOnTime;
            coolDownTime = _coolDownTime;
        }
    }
    triggerVars vars;

    private void Awake()
    {
        triggeredBy = LayerMask.GetMask("Default");
        size = new Vector2(1, 0.5f);
        vars = new triggerVars("q", .25f, .5f);
    }
    private void Start()
    {
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(vars.key) & !coolDown & !click & !triggOn)
        {
            triggOn = true;
            click = true;
            coolDown = true;
            StartCoroutine("triggerOnTimer");
            StartCoroutine("coolDownTimer");
        }
        if (Input.GetKeyUp(vars.key) && click)
        {
            click = false;
        }
        if (triggOn)
        {
            Collider2D[] inTrigger = Physics2D.OverlapBoxAll(transform.position, size, 0, triggeredBy);
            if (inTrigger.Length > 0)
            {
                foreach (Collider2D c in inTrigger)
                {
                    Action(c.gameObject);
                }
            }
        }
    }

    public void setSize(Vector2 _size)
    {
        size = _size;
    }
    public void setTrigLayerMask(string _triggeredBy)
    {
        triggeredBy = LayerMask.GetMask(_triggeredBy);
    }
    public void ChangeTriggerVariables(triggerVars vals)
    {
        vars = vals;
    }

    public virtual void Action(GameObject g)
    {
        g.SetActive(false);
    }
    private IEnumerator triggerOnTimer()
    {
        yield return new WaitForSeconds(vars.triggerOnTime);
        triggOn = false;
    }
    private IEnumerator coolDownTimer()
    {
        yield return new WaitForSeconds(vars.coolDownTime);
        coolDown = false;
    }

    //임시코드 - 테스트 환경용
    private void OnDrawGizmos()
    {
        EatTrigger temp;
        if (gameObject.TryGetComponent<EatTrigger>(out temp))
        {
            Gizmos.color = Color.yellow;
            if (coolDown)
                Gizmos.color = Color.red;
            if(triggOn)
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