using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayScore : MonoBehaviour
{
    float score = 0;
    public float correctBurger = 10;
    public float wrongBurger = 5;
    private void Start()
    {
        EventManager.eventManager.BurgerCompleteEvent += OnBurgerCompleteEvent;
    }

    void OnBurgerCompleteEvent(bool success)
    {
        if (success)
            score += correctBurger;
        else
        {
            score = Mathf.Clamp(score-wrongBurger, 0, score);
        }
        gameObject.GetComponent<TextMeshProUGUI>().text = "Score: " + score.ToString();
    }
}
