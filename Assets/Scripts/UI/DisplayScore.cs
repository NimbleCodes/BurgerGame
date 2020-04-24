using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayScore : MonoBehaviour
{
    public float score = 0;
    public static DisplayScore Instance;//싱글턴
    TriggerManager temp;
    void Awake(){
        Instance = this;
        temp = FindObjectOfType<TriggerManager>();
    }
    private void Start()
    {
        EventManager.eventManager.BurgerCompleteEvent += OnBurgerCompleteEvent;
    }

    void OnBurgerCompleteEvent(bool success)
    {
        if (success && score%5 ==0){
            gameObject.GetComponent<TextMeshProUGUI>().text = "Burgers: \n\n" + score.ToString();
            temp.changeTriggerKeys_random();
        }
        if(success){
            gameObject.GetComponent<TextMeshProUGUI>().text = "Burgers: \n\n" + score.ToString();
        }
    }

    public void AddScore(float correctBurger){
        score += correctBurger;
    }
}
