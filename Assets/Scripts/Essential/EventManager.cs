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

    //게임 일시정지 이벤트
    string prevWho = null;
    public event Action GamePausedEvent;
    public void Invoke_GamePausedEvent(string who)
    {
        if (prevWho == null)
        {
            prevWho = who;
            GamePausedEvent();
        }
    }
    public event Action GameResumeEvent;
    public void Invoke_GameResumeEvent(string who)
    {
        if (who == prevWho)
        {
            prevWho = null;
            GameResumeEvent();
        }
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