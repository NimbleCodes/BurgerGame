using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;
public class Left_Ani : MonoBehaviour
{
    //캐릭터 오브젝트(이미지 오브젝트는 애니메이션 적용시 지우면 됨)
    public GameObject Ani_Object;
    public SpriteRenderer showChar;
    string CharName;
    int burgerCount; //애니메이션 속도 증가 판별을 위한 변수
    Animator charAni;
    //배경 오브젝트
    public Image B_Panel;
    public static Left_Ani left_Animation;

    public void Awake(){
        left_Animation = this;
    }
    public void Initiate_Ani(){
        showChar = Ani_Object.GetComponent<SpriteRenderer>();
        /* 이미지로 배경을 넣어도되고, 색상만 넣어줘도 되고(원하는 대로)
        B_Panel.color = UnityEngine.Color.Black;
        B_Panel.Sprite = Resources.Load<Sprite>("");
        */
        charAni = Ani_Object.GetComponent<Animator>();
        charAni.enabled = false;
        showChar.sprite = Resources.Load<Sprite>("Sprites/Characters/Protagonist");
        showChar.transform.localScale = new Vector3(650,650,100);
        showChar.enabled = true;

    }
    //실패시 감정표현 재생
    public void show_Ani_Fail(bool burgerFail){
       if(burgerFail == false){
            //showChar.enabled = false;
            charAni.enabled = true;
            charAni.Play("Protagonist_Ani");
       }else if(burgerFail == true){
            charAni.enabled = true;
            charAni.Play("Protagonist_Ani");
            if(burgerCount%5 ==0){
                charAni.speed += 0.2f;
            }
       }
    }

    public void getBurgerCount(int count){
        count = burgerCount;
    }
    void Start()
    {
        EventManager.eventManager.BurgerCompleteEvent += show_Ani_Fail;
        Initiate_Ani();
    }
}
