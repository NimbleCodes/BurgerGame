using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obj_Destroy : MonoBehaviour
{
  public void OnEnable(){
  	Invoke("Destroy",1.8f);
  }

  void Destroy(){
  	gameObject.SetActive(false);
  }

  public void OnDisable(){
  	CancelInvoke();
  }
}
