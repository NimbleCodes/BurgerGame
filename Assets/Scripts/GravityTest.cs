using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityTest : MonoBehaviour
{
    public float speed = -10;
    void Update()
    {
        transform.Translate(0, speed * Time.deltaTime, 0);
    }
}
