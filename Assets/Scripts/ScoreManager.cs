using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    int score = 0;

    void Start()
    {
        EventManager.eventManager.BurgerCompleteEvent += OnBurgerCompleteEvent;
    }

    void OnBurgerCompleteEvent(string burger_info)
    {
        if(burger_info.Equals("burger done!"))
        {
            score += 10;
            EventManager.eventManager.Invoke_ScoreChangedEvent(score);
        }
    }
}
