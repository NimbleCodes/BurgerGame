using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Image cooldown;
    public bool coolingDown;//체력감소 트리거
    public float calHealth; // 체력 계산을 위한 변수
    public float calTime;
    public float GameTime = 60.0f;//시작시 게임시간
    public float minimumT = 20.0f;//최소시간
    public float Score = 0.00003f;
    public float minusScore = -0.00003f;
    public float decTimeval = 5.0f;//줄어드는 시간

    void Start(){
        coolingDown = true;
    }

    void Update()
    {
        DecHealth();

        if(Input.GetKeyDown(KeyCode.S)){
            AddHealth(Score);
        }
        if(Input.GetKeyDown(KeyCode.D)){
            MinHealth(minusScore);
        }
        if(Input.GetKeyDown(KeyCode.F)){
            DecTime();
        }
        
    }

    public void AddHealth(float val){
        cooldown.fillAmount += val;
    }

    public void MinHealth(float val){
        cooldown.fillAmount += val;
    }

    public void DecTime(){
        if(GameTime > 0)
        {
            if(calTime % Time.deltaTime == 0)
            {
                GameTime -= decTimeval;
            }
        }
        if(GameTime <= 20)//최소시간 이하로 내려가는걸 방지
        {
            GameTime = 20;
        }
    }

    public void DecHealth(){
        float percent = Time.deltaTime/GameTime;
        calTime = Time.deltaTime;
        if(coolingDown){
            cooldown.fillAmount -= Mathf.Lerp(1,0,percent) * 0.0001f;
        }
    }

}
