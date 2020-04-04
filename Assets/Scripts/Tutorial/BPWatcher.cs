using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using BreakPoints;

public class BPWatcher : MonoBehaviour
{
    List<BreakPoint> breakpoints;
    bool watch = true;
    int curBP = 0;

    private void Awake()
    {
        breakpoints = new List<BreakPoint>();
    }
    private void Start()
    {
        EventManager.eventManager.GamePausedEvent += stopWatching;
        breakpoints.Add(new BreakPointFirstIngrSpawned(0));
        breakpoints.Add(new BreakPointFirstIngrEaten(1));
        breakpoints.Add(new BreakPointFirstIngrDestroyed(2));
    }
    private void Update()
    {
        if (watch)
        {
            foreach (BreakPoint bp in breakpoints)
            {
                if (bp.BPReached() && curBP == bp.getBPNum())
                {
                    StartCoroutine(InvokeWithDelay(bp.getBPdelay(), bp.getBPNum()));
                    breakpoints.Remove(bp);
                    break;
                }
            }
            if(breakpoints.Count <= 0)
            {
                gameObject.GetComponent<BPWatcher>().enabled = false;
            }
        }
    }
    void stopWatching(bool pause, string who)
    {
        watch = !pause;
    }
    IEnumerator InvokeWithDelay(float delay, int bpnum)
    {
        yield return new WaitForSeconds(delay);
        EventManager.eventManager.Invoke_BpReachedEvent(bpnum);
        curBP = bpnum+1;
    }
}
