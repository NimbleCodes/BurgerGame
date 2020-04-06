using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showEaten : MonoBehaviour
{
    public string ingre = "Lettuce";//for test
    //이벤트 매니저에 함수추가
   public void Start(){
       EventManager.eventManager.IngrObtainedEvent += showEatenToUser;
   }
   public void Update(){//for test
       if(Input.GetKeyDown(KeyCode.H)){
            showEatenToUser(ingre);
        }
   }
   //먹은 ingre_name을 받아 맞는 sprite를 호출
   public void showEatenToUser(string ingre_info){
        GameObject objToSpawn = ObjectManager.objectManager.getGameObject(ingre_info);
        objToSpawn.GetComponent<Transform>().position = new Vector3(8f,4f);
        objToSpawn.SetActive(true);
        //objToSpawn.GetComponent<Rigidbody2D>().AddForce(Vector2.down*100f);
       objToSpawn.GetComponent<Rigidbody2D>().gravityScale=1f;
   }
}
