using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShowBun : MonoBehaviour
{

    public GameObject TopBun;
    public SpriteRenderer sprite;
    public static ShowBun showBun;//싱글톤
    //윗빵 이전의 index 받아오기용
    public int getCount;
    
    void Awake(){
        showBun = this;
    }
    void Start()
    {
        InitiateBun();
        
    }

    public void InitiateBun(){
        TopBun = GameObject.FindGameObjectWithTag("ShowBun");
        sprite = TopBun.GetComponent<SpriteRenderer>();
        sprite.sprite = Resources.Load<Sprite>("Sprites/Ingredients/Bun");
        sprite.enabled = false;
        TopBun.GetComponent<BoxCollider2D>().size = new Vector2(2f,0.2f);
        TopBun.GetComponent<Rigidbody2D>().gravityScale = 0f;
        TopBun.GetComponent<Transform>().position = new Vector2(8f,5f);
        TopBun.GetComponent<BoxCollider2D>().enabled = false;
        
    }

    public void SetActiveBun(bool complete){
        sprite.sprite = Resources.Load<Sprite>("Sprites/Ingredients/Bun");
        sprite.enabled = complete;
        TopBun.GetComponent<BoxCollider2D>().size = new Vector2(2f,0.2f);
        TopBun.GetComponent<Rigidbody2D>().gravityScale = 1f;
        TopBun.GetComponent<Transform>().position = new Vector2(8f,5f);
        TopBun.GetComponent<BoxCollider2D>().enabled = complete;
        //ObjectManager.objectManager.disableAllActive(complete);
    }

    //Bun이 밑에 재료의 collider와 만났을때 다시 시작
    void OnCollisionEnter2D(Collision2D col){
        if(col.gameObject.tag == ("showEat" + getCount)){
            showEaten.ShowObtain.InitiateObj();
        }
    }

    //showEat 태그의 넘버링 받아오기
    public void getIndexCount(int arrIndex){
        getCount = arrIndex;
    }
}
