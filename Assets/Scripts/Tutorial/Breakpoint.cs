using System;
using UnityEngine;

abstract class Breakpoint
{
    public bool active;
    int bpNum;
    float delay;
    Action executeOnBp;

    public Breakpoint(int _bpNum, float _delay, Action _executeOnBP)
    {
        active = false;
        bpNum = _bpNum;
        delay = _delay;
        executeOnBp = _executeOnBP;
    }
    public Breakpoint(int _bpNum)
    {
        active = false;
        bpNum = _bpNum;
        delay = 0;
    }
    protected abstract bool BpReached();
    public virtual void Execute()
    {
        executeOnBp.Invoke();
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
    public float getDelay()
    {
        return delay;
    }
    public int getBpNum()
    {
        return bpNum; 
    }
}

//테스트용
class Breakpoint_Wait : Breakpoint
{
    bool finished = false;

    public Breakpoint_Wait(int _bpNum, float _delay, Action _executeOnBp) : base(_bpNum, _delay, _executeOnBp)
    {
        
    }
    public override void Execute()
    {
        finished = false;
        base.Execute();
        finished = true;
    }
    protected override bool BpReached()
    {
        return true;
    }
}