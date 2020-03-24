using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityTest : MonoBehaviour
{
    Rigidbody2D rb;
    float gravityVal = 15f;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;    
    }

    private void OnEnable() {
        rb.AddForce(new Vector2(0,-gravityVal),ForceMode2D.Impulse);
    }
}
