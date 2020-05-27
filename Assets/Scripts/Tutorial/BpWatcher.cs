using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

class BpWatcher : MonoBehaviour
{
    public Image ingr_highlight;
    public Image lane_highlight;
    public Image trigpoint_highlight;
    public Image trigger_highlight;
    public TextMeshProUGUI ingr_text;
    public TextMeshProUGUI lane_text;
    public TextMeshProUGUI trigpoint_text;
    public TextMeshProUGUI trigger_text;
    Action startQue;
    Queue<Breakpoint> breakpoints;
    bool timerStart = false;

    IEnumerator DelayedBpTimer(float delay, int bpNum)
    {
        yield return new WaitForSeconds(delay);
        EventManager.eventManager.Invoke_BreakpointReachedEvent(bpNum);
        timerStart = false;
        if (breakpoints.Count > 0)
            breakpoints.Peek().active = true;
    }


    private void Awake()
    {
        breakpoints = new Queue<Breakpoint>();
        //add breakpoints here
        breakpoints.Enqueue(new Breakpoint_WaitOneSecond(0));
        breakpoints.Enqueue(new Breakpoint_WaitOneSecond(1));
        breakpoints.Enqueue(new Breakpoint_WaitOneSecond(2));
        if (breakpoints.Count > 0)
            breakpoints.Peek().active = true;
    }
    
    private void Update()
    {
        if (breakpoints.Count > 0)
        {
            if (!timerStart)
            {
                Breakpoint curBp = breakpoints.Dequeue();
                if (curBp.active & curBp.BpQuery())
                {
                    if (curBp.getDelay() > 0)
                    {
                        //delayed bp
                        StartCoroutine(DelayedBpTimer(curBp.getDelay(), curBp.getBpNum()));
                        timerStart = true;
                    }
                    else
                    {
                        EventManager.eventManager.Invoke_BreakpointReachedEvent(curBp.getBpNum());
                        if (breakpoints.Count > 0)
                            breakpoints.Peek().active = true;
                    }
                }
            }
        }
        else
        {
            gameObject.GetComponent<BpWatcher>().enabled = false;
        }
    }


//-----------Order[ingr -> lane -> trigpoint -> trigger]
    private void Active_ingr(){ //Activate ingr tutorial
        ingr_highlight.enabled = true;
        ingr_text.enabled = true;
    }

    private void Active_lane(){//Activate lane tutorial
        ingr_highlight.enabled =false;
        ingr_text.enabled = false;
        lane_highlight.enabled = true;
        lane_text.enabled = true;
    }

    private void Active_trigpoint(){//Active trigpoint tutorial
        lane_highlight.enabled = false;
        lane_text.enabled = false;
        trigpoint_highlight.enabled = true;
        trigpoint_text.enabled = true;
    }

    private void Active_trigger(){//Active trigger tutorial
        trigpoint_highlight.enabled = false;
        trigpoint_text.enabled = false;
        trigger_highlight.enabled = true;
        trigger_text.enabled = true;
    }
}