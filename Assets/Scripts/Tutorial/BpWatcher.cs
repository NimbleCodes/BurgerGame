using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

class BpWatcher : MonoBehaviour
{
    //public Image ingr_highlight;
    public GameObject ingr_highlight;
    public GameObject lane_highlight;
    public GameObject trigpoint_highlight;
    public GameObject trigger_highlight;

    //public TextMeshProUGUI ingr_text;
    public GameObject ingr_text;
    public GameObject lane_text;
    public GameObject trigpoint_text;
    public GameObject trigger_text;

    bool timer = false;
    Queue<Breakpoint> breakpoints;
    IEnumerator DelayedBpTimer(float delay, int bpNum)
    {
        yield return new WaitForSeconds(delay);
        EventManager.eventManager.Invoke_BreakpointReachedEvent(bpNum);
        EventManager.eventManager.Invoke_GamePausedEvent("Tutorial");

        breakpoints.Peek().Execute();
        timer = false;
    }

    private void Awake()
    {
        breakpoints = new Queue<Breakpoint>();
        //add breakpoints here
        breakpoints.Enqueue(new Breakpoint(0, 0, Active_ingr,       Deactive_ingr));
        breakpoints.Enqueue(new Breakpoint(0, 0, Active_lane,       Deactive_lane));
        breakpoints.Enqueue(new Breakpoint(0, 0, Active_trigpoint,  Deactive_trigpoint));
        breakpoints.Enqueue(new Breakpoint(0, 0, Active_trigger,    Deactive_trigger));

        if (breakpoints.Count > 0)
            breakpoints.Peek().active = true;
    }
    private void Update()
    {
        if(breakpoints.Count > 0)
        {
            //첫번째 bp가 active이고 BpReached를 만족하면 true
            if (breakpoints.Peek().BpQuery())
            {
                if(breakpoints.Peek().getDelay() > 0)
                {
                    StartCoroutine(DelayedBpTimer(breakpoints.Peek().getDelay(), breakpoints.Peek().getBpNum()));
                    timer = true;
                }
                else
                {
                    EventManager.eventManager.Invoke_BreakpointReachedEvent(breakpoints.Peek().getBpNum());
                    EventManager.eventManager.Invoke_GamePausedEvent("Tutorial");
                    breakpoints.Peek().Execute();
                }
            }
            if (!timer && breakpoints.Peek().ClearCond() == true)
            {
                breakpoints.Peek().ExecuteClear();
                breakpoints.Dequeue();
                if (breakpoints.Count > 0)
                    breakpoints.Peek().active = true;
                EventManager.eventManager.Invoke_GameResumeEvent("Tutorial");
            }
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

//-----------Order[ingr -> lane -> trigpoint -> trigger]
    private void Active_ingr(){
        ingr_highlight.SetActive(true);
        ingr_text.SetActive(true);
    }
    private void Deactive_ingr()
    { 
        ingr_highlight.SetActive(false);
        ingr_text.SetActive(false);
    }
    private void Active_lane(){
        lane_highlight.SetActive(true);
        lane_text.SetActive(true);
    }
    private void Deactive_lane()
    {
        lane_highlight.SetActive(false);
        lane_text.SetActive(false);
    }
    private void Active_trigpoint(){
        trigpoint_highlight.SetActive(true);
        trigpoint_text.SetActive(true);
    }
    private void Deactive_trigpoint()
    {
        trigpoint_highlight.SetActive(false);
        trigpoint_text.SetActive(false);
    }
    private void Active_trigger(){
        trigger_highlight.SetActive(true);
        trigger_text.SetActive(true);
    }
    private void Deactive_trigger()
    {
        trigger_highlight.SetActive(false);
        trigger_text.SetActive(false);
    }
}