using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BPWatcher : MonoBehaviour
{
    public abstract class BreakPoint
    {
        int BPNum = -1;
        public BreakPoint(int bpn)
        {
            BPNum = bpn;
        }
        protected abstract bool BPCondition();
        public bool BPReached()
        {
            return BPCondition();
        }
        public int getBPNum()
        {
            return BPNum;
        }
    }
    public class BPbyPos : BreakPoint
    {
        GameObject objToWatch;
        Vector3 position;
        public BPbyPos(int BPNum, GameObject t, Vector3 pos) : base(BPNum)
        {
            objToWatch = t;
            position = pos;
        }
        protected override bool BPCondition()
        {
            if (objToWatch.GetComponent<Transform>().position == position)
                return true;
            return false;
        }
    }
    List<BreakPoint> breakpoints;

    private void Awake()
    {
        breakpoints = new List<BreakPoint>();
    }
    private void Start()
    {
        //임시코드
        breakpoints.Add(new BPbyPos(0, FindObjectOfType<ObjectManager>().gameObject, new Vector3(0, 0, 0)));
        StartCoroutine("myCoroutine");
    }

    IEnumerator myCoroutine()
    {
        yield return new WaitForSeconds(3);
        if (breakpoints[0].BPReached())
        {
            EventManager.eventManager.Invoke_BpReachedEvent(breakpoints[0].getBPNum());
        }
    }
}
