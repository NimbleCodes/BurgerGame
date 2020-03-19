using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonEvent : MonoBehaviour
{
    public bool triggered = false;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            KeyDown_A();
            triggered = true;
        }
    }

    public void KeyDown_A()
    {
        
    }
}
