using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public string key = "q";
    public Vector2 size;
    public float triggerOnTime = .25f;
    public float coolDownTime = .5f;

    public LayerMask triggeredBy;

    //public 이 아니면 안됨. 왜 public이면 되는거지?
    //버튼 누르고 있기 및 버튼 연타로 다 먹기 안됨
    bool coolDown = false;
    public bool click = false;
    bool triggOn = false;

    private void Start()
    {
        EventManager.eventManager.GameOverEvent += OnGameOver;
        if (size == null)
            size = new Vector2(1, 0.5f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(key) & !coolDown & !click & !triggOn)
        {
            triggOn = true;
            click = true;
            coolDown = true;
            StartCoroutine("triggerOnTimer");
            StartCoroutine("coolDownTimer");
        }
        if (Input.GetKeyUp(key) && click)
        {
            click = false;
        }

        if (triggOn)
        {
            Collider2D[] inTrigger = Physics2D.OverlapBoxAll(transform.position, size, 0, triggeredBy);
            if (inTrigger.Length > 0)
            {
                foreach(Collider2D c in inTrigger)
                {
                    Action(c.gameObject);
                }
            }
        }
    }

    private void OnGameOver()
    {
        gameObject.SetActive(false);
    }
    public virtual void Action(GameObject g)
    {
        Debug.Log(g.name);
    }

    private IEnumerator triggerOnTimer()
    {
        yield return new WaitForSeconds(triggerOnTime);
        triggOn = false;
    }
    private IEnumerator coolDownTimer()
    {
        yield return new WaitForSeconds(coolDownTime);
        coolDown = false;
    }
}
