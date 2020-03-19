using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    int health = 100;
    int healthReduc_perSec = 5;

    bool GameOver = false;
    
    string current_burgerOrder;
    float nextBurgerOrderTime = 10f;
    int correctBurger = 5;
    int wrongBurger = 1;

    bool healthReducTimerOn = false;
    bool newBurgerOrderTimerOn = false;

    private void Start()
    {
        EventManager.eventManager.BurgerCompleteEvent += OnBurgerCompleteEvent;
    }

    private void Update()
    {
        if (!GameOver)
        {
            if (health <= 0)
            {
                EventManager.eventManager.Invoke_GameOverEvent();
                GameOver = true;
            }
            if (!healthReducTimerOn)
            {
                StartCoroutine("healthReducTimer");
                healthReducTimerOn = true;
            }
            if (!newBurgerOrderTimerOn)
            {
                StartCoroutine("newBurgerOrderTimer");
                newBurgerOrderTimerOn = true;
            }
        }
    }

    public void decHealth(int val)
    {
        health -= val;
    }
    private void incHealth(int val)
    {
        health += val;
    }
    
    public void OnBurgerCompleteEvent(string burger_info)
    {
        if (burger_info.Equals(current_burgerOrder))
            incHealth(correctBurger);
        else
            decHealth(wrongBurger);
    }
    
    private string getNewBurgerOrder()
    {
        return "";
    }

    private IEnumerator healthReducTimer()
    {
        yield return new WaitForSeconds(1);
        decHealth(healthReduc_perSec);
        healthReducTimerOn = false;
    }
    private IEnumerator newBurgerOrderTimer()
    {
        current_burgerOrder = getNewBurgerOrder();
        yield return new WaitForSeconds(nextBurgerOrderTime);
        newBurgerOrderTimerOn = false;
    }
}
