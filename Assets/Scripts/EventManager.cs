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
    public event Action IngrDestroyedEvent;
    public event Action<string> IngrEatenEvent;
    //public event Action<string> IngrRecycledEvent;
    public event Action<string> BurgerCompleteEvent;
    
    public void Invoke_GameOverEvent()
    {
        if(GameOverEvent != null)
            GameOverEvent();
    }
    public void Invoke_IngrDestroyedEvent()
    {
        if (IngrDestroyedEvent != null)
            IngrDestroyedEvent();
    }
    public void Invoke_IngrEatenEvent(string ingr_info)
    {
        if (IngrEatenEvent != null)
            IngrEatenEvent(ingr_info);
    }
    /*
    public void Invoke_IngrRecycledEvent(string ingr_info)
    {
        if (IngrRecycledEvent != null)
            IngrRecycledEvent(ingr_info);
    }
    */
    public void Invoke_BurgerCompleteEvent(string burger_info)
    {
        if (BurgerCompleteEvent != null)
            BurgerCompleteEvent(burger_info);
    }
}
