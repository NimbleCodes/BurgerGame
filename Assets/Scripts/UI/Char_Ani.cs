using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Char_Ani : MonoBehaviour
{  
    //캐릭터 오브젝트(이미지 오브젝트는 애니메이션 적용시 지우면 됨)
    public GameObject GObject;
    public SpriteRenderer ShowChar;
    public string CharName;
    private string Ani_Name;
    private int CountBurger;//애니메이션 속도 증가 판별 변수
    public Animator charAnima;
    //배경 오브젝트
    public Image B_Panel;
    public static Char_Ani Character_Animation;
    public showEaten showObtain;
    public ShowBun showBun;

    void Awake(){
        Character_Animation = this;
    }
    
    public void Initiate_Ani(){
        ShowChar = GObject.GetComponent<SpriteRenderer>();
        /* 이미지로 배경을 넣어도되고, 색상만 넣어줘도 되고(원하는 대로)
        B_Panel.color = UnityEngine.Color.Black;
        B_Panel.Sprite = Resources.Load<Sprite>("");
        */
        ShowChar.enabled = false;
        charAnima.enabled = false;
    }
    //캐릭터 이름 받아와 보여주기
    public void show_Char(){
        BurgerRecipe.burgerRecipe.currentChar(ref CharName);
        Ani_Name = CharName;
        Ani_Name +="_Ani";
        charAnima.enabled = false;
        /*
        charAni = Ani_Object.GetComponent<Animation>();
        charAni.animation.Play("animation_Name");
        */
        ShowChar.sprite = Resources.Load<Sprite>("Sprites/Characters/"+ CharName);
        ShowChar.transform.localScale = new Vector3(650,650,100);
        ShowChar.enabled = true;
    }

    public void show_shutter(){
        ShowChar.transform.localScale = new Vector3(100,100,100);
        charAnima.Play("shutter");
    }
    public void show_Ani_Failed(bool Fail){
        if(Fail == false){
            charAnima.enabled = true;
            charAnima.Play(Ani_Name);
        }
    }
    public void onComplete(bool TF){
        if(TF == true){
            charAnima.enabled = true;
            charAnima.Play(Ani_Name);
            if(CountBurger%5 == 0){
                charAnima.speed += 0.2f;
            }
        }
    }

    public void getCountBurger(int count){
        count = CountBurger;
    }
    void Start()
    {
        EventManager.eventManager.BurgerCompleteEvent += onComplete;
        EventManager.eventManager.BurgerCompleteEvent += show_Ani_Failed;
        Initiate_Ani();
        show_Char();
    }

}
