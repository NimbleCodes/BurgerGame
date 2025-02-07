﻿using System;
using UnityEngine;

class EventManager : MonoBehaviour
{
    public static EventManager eventManager;

    string prevWho = null;
    public event Action GameOverEvent;
    public event Action GamePausedEvent;
    public event Action GameResumeEvent;
    public event Action<string, int> IngrObtainedEvent;
    public event Action IngrDestroyedEvent;
    public event Action<int> IngrReturnedEvent;
    public void Invoke_GamePausedEvent(string who)
    {
        if (prevWho == null)
        {
            prevWho = who;
            GamePausedEvent();
        }
    }
    public void Invoke_GameOverEvent()
    {
        if (GameOverEvent != null)
            GameOverEvent();
    }
    public void Invoke_IngrObtainedEvent(string what, int trignum)
    {
        if (IngrObtainedEvent != null)
            IngrObtainedEvent(what, trignum);
    }
    public void Invoke_IngrDestroyedEvent()
    {
        if (IngrDestroyedEvent != null)
            IngrDestroyedEvent();
    }
    public void Invoke_IngrReturnedEvent(int trignum)
    {
        if (IngrReturnedEvent != null)
            IngrReturnedEvent(trignum);
    }
    public void Invoke_GameResumeEvent(string who)
    {
        if (who == prevWho)
        {
            prevWho = null;
            GameResumeEvent();
        }
    }

    //난이도 상승시 호출
    //스포너 및 트리거의 변경 가능한 값들 다시 할당
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

    //튜토리얼 관련 이벤트
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