using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DisplayScore : MonoBehaviour
{
    public GameObject left_hideOut;
    public GameObject right_hideOut;
    public int score = 0;
    public static DisplayScore Instance;//싱글턴
    TriggerManager temp;
    void Awake(){
        Instance = this;
        temp = FindObjectOfType<TriggerManager>();
    }
    private void Start()
    {
        EventManager.eventManager.BurgerCompleteEvent += OnBurgerCompleteEvent;
        Initiate_hideOuts();
    }
    public void Update(){
        Char_Ani.Character_Animation.getCountBurger(score);
        Left_Ani.left_Animation.getBurgerCount(score);
        showHideOut();
        if(Input.GetKeyDown(KeyCode.M)){
            score = 20;
        }
    }

    public void Initiate_hideOuts(){
        left_hideOut.SetActive(true);
        right_hideOut.SetActive(true);
    }

    void OnBurgerCompleteEvent(bool success)
    {
        if (success && score%5 ==0){
            gameObject.GetComponent<TextMeshProUGUI>().text = "\n" + score.ToString();
            //temp.changeTriggerKeys_random();
            HealthManager.Instance.minusTime(2.5f);
        }
        if(success){
            gameObject.GetComponent<TextMeshProUGUI>().text = "\n" + score.ToString();
        }
    }

    void showHideOut(){
        if(score == 20){
            left_hideOut.SetActive(false);
            right_hideOut.SetActive(false);
        }
    }

    public void AddScore(int correctBurger){
        score += correctBurger;
    }
}
