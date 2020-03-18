using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthManager : MonoBehaviour
{

    //MaxHealth의 기본 기준은 100이다. 쓰이는 곳은 없으나 계산의 용이성을 위해 존재한다.
    float maxHealth = 100;
    //현재 체력. 개발의 용이성을 위해 존재한다.(디버그용임)
    float curHealth = 100;
    //MaxHealth가 자연적으로 소진되는 시간.
    float decrTime = 20;
    //hp와 Time 의 비율. hp 증감의 쉬운 계산을 위해 이용된다.
    float hpToTime;
    //현재 흘러가는 시간 
    float activeTime = 0;
    //True 이면 HP가 감소하기 시작한다.
    bool decrStart = false;
    
    Slider healthBar;
    
    void Start()
    {
        healthBar = GetComponent<Slider>();
    }
    
    void Update()
    {
        Debug.Log(activeTime);
        //hp 증감의 쉬운 계산을 위해 이용된다.
        hpToTime = decrTime/maxHealth;


        //체력이 기준을 넘기지 않도록 한다.
        if(curHealth <= 0){
            curHealth = 0;
        }
        if(curHealth >= maxHealth){
            curHealth = maxHealth;
        }

        #region ForTest(erasable)
        if(Input.GetKeyDown(KeyCode.A)&&!decrStart){
            startDecr();
        }else if(Input.GetKeyDown(KeyCode.A)&&decrStart){
            stopDecr();
        }
        if(Input.GetKeyDown(KeyCode.S)){
            addHealth(20);
        }
        if(Input.GetKeyDown(KeyCode.D)){
            minusHealth(20);
        }
        decrHealth();
        #endregion
    }

 
    #region Functions
    //감소를 시작한다. decrStart를 true로 설정한다.
    public void startDecr(){
        decrStart = true;
    }
    //감소를 멈춘다. decrStart를 false로 설정한다.
    public void stopDecr(){
        decrStart = false;
    }
    //hp만큼 체력을 회복한다. 
    public void addHealth(float hp){
        curHealth += hp;
        activeTime -= hp*hpToTime;
    }
    //hp만큼 체력을 감소한다.
    public void minusHealth(float hp){
        curHealth -= hp;
        activeTime += hp*hpToTime;
    }
    //HP가 시간에 따라 감소하는 것을 실행한다.
    public void decrHealth(){

        //흘러가는 시간이 기준을 넘기지 않도록 한다.
        if(activeTime >= decrTime){
            activeTime = decrTime;
        }
        if(activeTime <= 0){
            activeTime = 0;
        }

        if(decrStart){
            activeTime += Time.deltaTime;
            float percent = activeTime/decrTime;

            healthBar.value = Mathf.Lerp(1,0,percent);
        }
    }
    #endregion

}
