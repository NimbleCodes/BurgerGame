using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Image cooldown;
    public bool coolingDown;
    public float waitTime = 30.0f;
   /// void Start()
    ///{
       
    ///}

    // Update is called once per frame
    void Update()
    {
        if(coolingDown==true)
        {
            cooldown.fillAmount -= 1.0f / waitTime * Time.deltaTime;
        }
        
    }
}
