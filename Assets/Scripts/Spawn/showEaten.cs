using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class showEaten : MonoBehaviour
{
    public string ingre = "Lettuce";//for test
    public int arrIndex = 0;
    public static showEaten ShowObtain;
    public GameObject ingr;//오브젝트 저장
    public SpriteRenderer spriteR;
    public void Awake(){
        ShowObtain = this;
    }
    //이벤트 매니저에 함수추가
   public void Start(){
       //EventManager.eventManager.IngrObtainedEvent += showEatenToUser;
       InitiateObj();
   }
   public void Update(){//for test
       if(Input.GetKeyDown(KeyCode.H)){
            showEatenToUser(ingre);
        }
   }

   public void InitiateObj(){//오브젝트 초기화
       arrIndex = 0;
       for(int i =0;i<10;i++){
           ingr = GameObject.FindGameObjectWithTag("showEat"+i);
           spriteR = ingr.GetComponent<SpriteRenderer>();
           ingr.GetComponent<Rigidbody2D>().gravityScale = 0f;
           ingr.GetComponent<Transform>().position = new Vector3(8f,5f);
           spriteR.enabled = false;
       }
   }
   //먹은 ingre_name을 받아 맞는 sprite를 호출
   public void showEatenToUser(string ingre_info){
        ingr = GameObject.FindGameObjectWithTag("showEat"+arrIndex);
        spriteR = ingr.GetComponent<SpriteRenderer>();
        spriteR.sprite= Resources.Load<Sprite>("Sprites/Ingredients/" + ingre_info);
        spriteR.enabled = true;
        ingr.GetComponent<BoxCollider2D>().size = new Vector3(2f,0.2f);
        ingr.GetComponent<Rigidbody2D>().gravityScale = 1f;
        arrIndex++;
   }
}
