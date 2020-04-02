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
    
    public event Action GameOverEvent;                      //게임 오버
    public event Action<bool, string> GamePausedEvent;      //게임 일시정지
    //bool 값은 true이면 멈추고 false이면 재시작. string 값은 호출한 코드의 이름
    public event Action <int>DiffIncEvent;                  //난이도 상승 이벤트
    public event Action IngrDestroyedEvent;                 //재료가 당에 떨어짐
    public event Action<string> IngrEatenEvent;             //재료가 플레이어에게 수거됨
    public event Action<bool> BurgerCompleteEvent;          //버거가 완성됨
    public event Action<int> BpReachedEvent;                //튜토리얼 멈춤 지점 발생 이벤트

    public void Invoke_GameOverEvent()
    {
        if(GameOverEvent != null)
            GameOverEvent();
    }
    public void Invoke_GamePausedEvent(bool action, string who)
    {
        if (GamePausedEvent != null)
            GamePausedEvent(action, who);
    }
    public void Invoke_DiffIncEvent(int stage_num)
    {
        if (DiffIncEvent != null)
            DiffIncEvent(stage_num);
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
    public void Invoke_BpReachedEvent(int bpnum)
    {
        if (BpReachedEvent != null)
            BpReachedEvent(bpnum);
    }
}
