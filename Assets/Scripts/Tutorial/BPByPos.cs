using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BPByPos : BreakPoint
{
    public Vector3 position;

    protected override bool BPCondition()
    {
        if(Input.GetKeyDown(KeyCode.Alpha2))
            return true;
        return false;
    }
}
