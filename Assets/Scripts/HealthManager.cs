using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Image cooldown;
    public bool coolingDown;
    public float waitTime = 30.0f;
    public float Score = 5.0f;

    void Update()
    {
        if(coolingDown==true)
        {
            cooldown.fillAmount -= 1.0f / waitTime * Time.deltaTime;
        }
        
    }

    void AddHealth(Score){
        cooldown.fillAmount =+ Score;
    }
}
