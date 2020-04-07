using System;
using UnityEngine;

class EventManager : MonoBehaviour
{
    public static EventManager eventManager;

    public event Action GameOverEvent;
    public void Invoke_GameOverEvent()
    {
        if (GameOverEvent != null)
            GameOverEvent();
    }

    public event Action<string> IngrObtainedEvent;
    public event Action IngrDestroyedEvent;
    public void Invoke_IngrObtainedEvent(string what)
    {
        if (IngrObtainedEvent != null)
            IngrObtainedEvent(what);
    }
    public void Invoke_IngrDestroyedEvent()
    {
        if (IngrDestroyedEvent != null)
            IngrDestroyedEvent();
    }

    public event Action RefreshEvent;
    public void Invoke_RefreshEvent()
    {
        if (RefreshEvent != null)
            RefreshEvent();
    }

    public event Action<bool> BurgerCompleteEvent;
    public void Invoke_BurgerCompleteEvent(bool cor)
    {
        if (BurgerCompleteEvent != null)
            BurgerCompleteEvent(cor);
    }

    public event Action<int> BreakpointReachedEvent;
    public void Invoke_BreakpointReachedEvent(int bpNum)
    {
        if (BreakpointReachedEvent != null)
            BreakpointReachedEvent(bpNum);
    }

    private void Awake()
    {
        eventManager = this;
    }
}