using System;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager eventManager;
    private void Awake()
    {
        eventManager = this;
    }

    public event Action GameOverEvent;
    public event Action<string> IngrDestroyedEvent;
    public event Action<string> BurgerCompleteEvent;
    
    public void Invoke_GameOverEvent()
    {
        if(GameOverEvent != null)
            GameOverEvent();
    }
    public void Invoke_IngrDestroyedEvent(string destr_info)
    {
        if (IngrDestroyedEvent != null)
            IngrDestroyedEvent(destr_info);
    }
    public void Invoke_BurgerCompleteEvent(string burger_info)
    {
        if (BurgerCompleteEvent != null)
            BurgerCompleteEvent(burger_info);
    }
}
