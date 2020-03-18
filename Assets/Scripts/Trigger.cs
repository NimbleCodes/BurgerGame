﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public string key = "q";
    public Vector2 size;
    public float triggerOnTime = .25f;
    public float coolDownTime = .5f;

    //public 이 아니면 안됨. 왜 public이면 되는거지?
    //버튼 누르고 있기 및 버튼 연타로 다 먹기 안됨
    public bool coolDown = false;
    public bool click = false;
    public bool triggOn = false;

    private void Start()
    {
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
            Collider2D[] inTrigger = Physics2D.OverlapBoxAll(transform.position, size, 0);
            if (inTrigger.Length > 0)
            {
                foreach(Collider2D c in inTrigger)
                {
                    Debug.Log(c.gameObject.name);
                    //EventManager.eventManager.Invoke_IngrDestroyedEvent("Trigger,Eaten,Tomato");
                }
            }
        }
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
