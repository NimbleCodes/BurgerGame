using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private void Start()
    {
        EventManager.eventManager.IngrEatenEvent += OnIngrEaten;
    }

    void OnIngrEaten(string ingr_info)
    {
        Debug.Log(ingr_info);
    }
}
