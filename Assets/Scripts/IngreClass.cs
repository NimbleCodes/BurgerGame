using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngreClass : MonoBehaviour
{
    ingredient ingrVar;
    Rigidbody2D rigid;

    private void Start() {
        rigid = GetComponent<Rigidbody2D>();
    }

    //자신의 정보 초기화.
    public void setIngrValue(ingredient value){
        ingrVar = value;
    }
    
    //rigidbody simulating을 켜거나 끈다. 그러므로 중력이 적용되지 않으며 불필요한 연산을 방지한다.
    public void turnGravity(bool value){
        rigid.simulated = value;
    }

    
}
