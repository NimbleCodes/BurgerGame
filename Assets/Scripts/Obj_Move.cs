using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obj_Move : MonoBehaviour
{
    public float speed = 5.0f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, speed *= Time.deltaTime);

        if(transform.localPosition.y < -5.0f) ///Y축 -5.0이상 내려가면 3.5로 이동
        {
            transform.localPosition = new Vector2(0,3.5f);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("End_Line"))///endline 만나면 소멸
        {
            gameObject.SetActive(false);
        }
    }
}
