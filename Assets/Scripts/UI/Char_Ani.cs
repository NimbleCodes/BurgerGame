using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Char_Ani : MonoBehaviour
{  
    //캐릭터 오브젝트(이미지 오브젝트는 애니메이션 적용시 지우면 됨)
    public GameObject Ani_Object;
    public Image showChar;
    string CharName;
    Animation charAni;
    //배경 오브젝트
    public Image B_Panel;
    
    public void Initiate_Ani(){
        showChar = Ani_Object.GetComponent<Image>();
        /* 이미지로 배경을 넣어도되고, 색상만 넣어줘도 되고(원하는 대로)
        B_Panel.color = UnityEngine.Color.Black;
        B_Panel.Sprite = Resources.Load<Sprite>("");
        */
        showChar.enabled = false;

    }
    //캐릭터 이름 받아와 애니메이션 재생
    public void show_Ani(){
        BurgerRecipe.burgerRecipe.currentChar(ref CharName);
        Debug.Log(CharName);
        /*
        charAni = Ani_Object.GetComponent<Animation>();
        charAni.animation.Play("animation_Name");
        */
        showChar.sprite = Resources.Load<Sprite>("Sprites/Characters/"+ CharName);
        showChar.enabled = true;
    }

    public void onComplete(bool TF){
        Initiate_Ani();
        show_Ani();
    }
    void Start()
    {
        EventManager.eventManager.BurgerCompleteEvent += onComplete;
        Initiate_Ani();
        show_Ani();
    }

}
