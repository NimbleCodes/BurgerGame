using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayScore : MonoBehaviour
{
    public float score = 0;
    public static DisplayScore Instance;//싱글턴
    void Awake(){
        Instance = this;
    }
    private void Start()
    {
        EventManager.eventManager.BurgerCompleteEvent += OnBurgerCompleteEvent;
    }

    void OnBurgerCompleteEvent(bool success)
    {
        if (success){
            gameObject.GetComponent<TextMeshProUGUI>().text = "Burgers: \n\n" + score.ToString();
        }
    }

    public void AddScore(float correctBurger){
        score += correctBurger;
    }
}
