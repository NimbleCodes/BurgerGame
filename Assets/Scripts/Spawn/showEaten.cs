using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class showEaten : MonoBehaviour
{
    public string ingre = "Lettuce";//for test
    public int arrIndex = 0;
    public static showEaten ShowObtain;
    public GameObject ingr;//생성 오브젝트
    public GameObject[] ingr_Arr;//
    public SpriteRenderer spriteR;
    public SpriteRenderer[] sprite_Arr;
    public int TagCount=0;
    public string Bun = "Bun";
    public bool complete = true;
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
       arrIndex = 1;
       for(int i =0;i<10;i++){
           ingr = GameObject.FindGameObjectWithTag("showEat"+i);
           ingr_Arr[i] = ingr;
           spriteR = ingr_Arr[i].GetComponent<SpriteRenderer>();
           sprite_Arr[i] = spriteR;
           ingr_Arr[i].GetComponent<Rigidbody2D>().gravityScale = 0f;
           ingr_Arr[i].GetComponent<Rigidbody2D>().velocity = Vector3.zero;
           ingr_Arr[i].GetComponent<Transform>().position = new Vector3(8f,5f);
           ingr_Arr[i].GetComponent<BoxCollider2D>().enabled = false;
           sprite_Arr[i].enabled = false;
           
           
       }
        //ingr = GameObject.FindGameObjectWithTag("showEat0");
        //spriteR = ingr.GetComponent<SpriteRenderer>();
        //spriteR.sprite= Resources.Load<Sprite>("Sprites/Ingredients/BelowBun");
        //spriteR.enabled = true;
        sprite_Arr[0].sprite = Resources.Load<Sprite>("Sprites/Ingredients/BelowBun");
        sprite_Arr[0].enabled = true;
        ingr_Arr[0].GetComponent<BoxCollider2D>().size = new Vector3(2f,0.2f);
        ingr_Arr[0].GetComponent<Transform>().position = new Vector3(8f,2.2f);
        ingr_Arr[0].GetComponent<Rigidbody2D>().gravityScale = 1f;
        ingr_Arr[0].GetComponent<BoxCollider2D>().enabled = true;

   }
   //먹은 ingre_name을 받아 맞는 sprite를 호출
   public void showEatenToUser(string ingre_info){
       if(ingre_info == Bun){
            ShowBun.showBun.getIndexCount(arrIndex);
            ShowBun.showBun.SetActiveBun(complete);
            arrIndex = 0;
       }else{
            
            //ingr = GameObject.FindGameObjectWithTag("showEat"+arrIndex);
            //spriteR = ingr.GetComponent<SpriteRenderer>();
            sprite_Arr[arrIndex].sprite= Resources.Load<Sprite>("Sprites/Ingredients/" + ingre_info);
            sprite_Arr[arrIndex].enabled = true;
            ingr_Arr[arrIndex].GetComponent<BoxCollider2D>().size = new Vector3(2f,0.2f);
            ingr_Arr[arrIndex].GetComponent<Rigidbody2D>().gravityScale = 1f;
            ingr_Arr[arrIndex].GetComponent<BoxCollider2D>().enabled = true;
            arrIndex++;
            
       }
   }
}
