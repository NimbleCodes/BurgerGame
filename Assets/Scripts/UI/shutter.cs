using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shutter : MonoBehaviour
{
    public GameObject shutt;
    public SpriteRenderer shutter_sprite;
    public Animator shutter_Ani;
    public ShowBun showBun;
    void Start(){
        EventManager.eventManager.BurgerCompleteEvent += show_shutter_Ani;
        Initiateshutter();
    }
    public void Initiateshutter(){
        shutter_sprite.enabled=false;
        shutter_Ani.enabled=false;
        showBun.InitiateBun();
        showEaten.ShowObtain.InitiateObj();
    }
    public void show_shutter_Ani(bool TF){
        shutter_Ani.enabled = true;
        shutter_sprite.enabled=true;
        shutter_Ani.Play("shutter_for_showeaten");
        //showEaten.ShowObtain.InitiateObj();
        //showBun.InitiateBun(); 
    }
}
