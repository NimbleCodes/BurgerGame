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
    public event Action<string> BurgerCompleteEvent;

    public event Action<string> PauseButtonPressed;
    public event Action<int> ScoreChangedEvent;
    
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
    public void Invoke_BurgerCompleteEvent(string burger_info)
    {
        if (BurgerCompleteEvent != null)
            BurgerCompleteEvent(burger_info);
    }
    
    public void Invoke_PauseButtonPressed(string status)
    {
        if (PauseButtonPressed != null)
            PauseButtonPressed(status);
    }
    public void Invoke_ScoreChangedEvent(int val)
    {
        if (ScoreChangedEvent != null)
            ScoreChangedEvent(val);
    }

}
