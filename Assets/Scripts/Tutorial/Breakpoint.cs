using System;
using UnityEngine;

class Breakpoint
{
    public bool active;
    int bpNum;
    float delay;
    Action executeOnBp;
    Action executeOnClear;

    public Breakpoint(int _bpNum, float _delay, Action _executeOnBP, Action _executeOnClear)
    {
        active = false;
        bpNum = _bpNum;
        delay = _delay;
        executeOnBp = _executeOnBP;
        executeOnClear = _executeOnClear;
    }
    public Breakpoint(int _bpNum)
    {
        active = false;
        bpNum = _bpNum;
        delay = 0;
    }
    protected virtual bool BpReached()
    {
        return true;
    }
    public bool BpQuery()
    {
        if (active)
        {
            active = false;
            return BpReached();
        }
        return false;
    }

    public void Execute()
    {
        executeOnBp.Invoke();
    }
    public void ExecuteClear()
    {
        executeOnClear.Invoke();
    }
    public virtual bool ClearCond()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            return true;
        }
        return false;
    }

    public float getDelay()
    {
        return delay;
    }
    public int getBpNum()
    {
        return bpNum; 
    }
}
