using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class BpWatcher : MonoBehaviour
{
    Queue<Breakpoint> breakpoints;
    bool timerStart = false;

    IEnumerator DelayedBpTimer(float delay, int bpNum)
    {
        yield return new WaitForSeconds(delay);
        EventManager.eventManager.Invoke_BreakpointReachedEvent(bpNum);
        timerStart = false;
    }

    private void Awake()
    {
        breakpoints = new Queue<Breakpoint>();
        //add breakpoints here
        breakpoints.Enqueue(new Breakpoint_WaitOneSecond(0));
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
                    }
                }
            }
        }
        else
        {
            gameObject.GetComponent<BpWatcher>().enabled = false;
        }
    }
}