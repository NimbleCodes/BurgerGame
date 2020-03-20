using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    int count = 0;

    private void Start()
    {
        EventManager.eventManager.IngrEatenEvent += OnIngrEaten;
    }

    void OnIngrEaten(string ingr_info)
    {
        count++;
        if (count >= 3)
            EventManager.eventManager.Invoke_BurgerCompleteEvent("burger done!");
    }
}
