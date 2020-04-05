using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showEaten : MonoBehaviour
{
    public string ingre = "Lettuce 1";//for test
    //이벤트 매니저에 함수추가
   public void start(){
       EventManager.eventManager.IngrEatenEvent += showEatenToUser;
   }
   public void update(){//for test
       if(Input.GetKeyDown(KeyCode.H)){
            showEatenToUser(ingre);
        }
   }
   public void showEatenToUser(string ingre_info){
        GameObject objToSpawn = ObjectManager.objectManager.getGameObject(ingre_info);
        objToSpawn.GetComponent<Transform>().position = new Vector3(8f,5f);
        objToSpawn.SetActive(true);
   }
}
