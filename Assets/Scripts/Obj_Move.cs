using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obj_Move : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
       this.transform.GetComponent<Rigidbody>().velocity = new Vector2(0,-1);
    }

}
