using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayScore : MonoBehaviour
{
    public static DisplayScore displaysocre;
    float score = 0;
    public float correctBurger = 10;
    public float wrongBurger = 5;
    HealthManager healthManager;
    private void Start()
    {
        EventManager.eventManager.BurgerCompleteEvent += OnBurgerCompleteEvent;
    }

   public void OnBurgerCompleteEvent(bool success)
    {
        if (success)
        {
            score += correctBurger;
            healthManager.addHealth(correctBurger);
        }
        else
        {
            score = Mathf.Clamp(score-wrongBurger, 0, score);
            healthManager.minusHealth(wrongBurger);
        }
        gameObject.GetComponent<TextMeshProUGUI>().text = "Score: " + score.ToString();
    }
}
