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

    public event Action GameOverEvent;              //게임 오버
    public event Action<string> GamePausedEvent;    //게임 일시정지
    public event Action DiffIncEvent;               //난이도 상승 이벤트
    public event Action IngrDestroyedEvent;         //재료가 당에 떨어짐
    public event Action<string> IngrEatenEvent;     //재료가 플레이어에게 수거됨
    public event Action<bool> BurgerCompleteEvent;  //버거가 완성됨
    
    public void Invoke_GameOverEvent()
    {
        if(GameOverEvent != null)
            GameOverEvent();
    }
    public void Invoke_GamePausedEvent(string str)
    {
        if (GamePausedEvent != null)
            GamePausedEvent(str);
    }
    public void Invoke_DiffIncEvent()
    {
        if (DiffIncEvent != null)
            DiffIncEvent();
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
    public void Invoke_BurgerCompleteEvent(bool burger_info)
    {
        if (BurgerCompleteEvent != null)
            BurgerCompleteEvent(burger_info);
    }
}
