using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showEaten : MonoBehaviour
{
   public void start(){
       EventManager.eventManager.IngrEatenEvent += showEatenToUser;
   }
   public void showEatenToUser(string ingre_info){
       GameObject ingreToshow = ObjectManager.objectManager.getGameObject(ingre_info);
       ingreToshow.GetComponent<Transform>().position = new Vector2(8f,5f);
       ingreToshow.SetActive(true);
   }
}
