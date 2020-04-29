using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Left_Ani : MonoBehaviour
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
        showChar.sprite = Resources.Load<Sprite>("Sprites/Characters/Protagonist");
        showChar.enabled = true;

    }
    //캐릭터 이름 받아와 애니메이션 재생
    public void show_Ani(){
        BurgerRecipe.burgerRecipe.currentChar(ref CharName);
        /* 캐릭터 이름에 맞춰서 재생
           그외에 감정표현 등은 추후 추가
        charAni = Ani_Object.GetComponent<Animation>();
        charAni.animation.Play("animation_Name");
        */
    }
    void Start()
    {
        Initiate_Ani();
       // show_Ani();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
