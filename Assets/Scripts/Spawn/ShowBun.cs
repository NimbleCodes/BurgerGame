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

    void Awake()
    {
        showBun = this;
    }
    void Start()
    {
        TopBun = GameObject.FindGameObjectWithTag("ShowBun");
        sprite = TopBun.GetComponent<SpriteRenderer>();
        InitiateBun();
    }

    public void InitiateBun()
    {
        sprite.sprite = Resources.Load<Sprite>("Sprites/Ingredients/Bun");
        sprite.enabled = false;
        TopBun.GetComponent<BoxCollider2D>().size = new Vector2(2f, 0.2f);
        TopBun.GetComponent<Rigidbody2D>().gravityScale = 0f;
        TopBun.GetComponent<Transform>().position = new Vector2(8f, 5f);
        TopBun.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        TopBun.GetComponent<BoxCollider2D>().enabled = false;


    }

    //윗빵 먹었을때 Active 함수 
    public void SetActiveBun()
    {
        sprite.sprite = Resources.Load<Sprite>("Sprites/Ingredients/Bun");
        sprite.enabled = true;
        TopBun.GetComponent<BoxCollider2D>().size = new Vector2(2f, 0.2f);
        TopBun.GetComponent<Rigidbody2D>().gravityScale = 1f;
        TopBun.GetComponent<Transform>().position = new Vector2(8f, 5f);
        TopBun.GetComponent<BoxCollider2D>().enabled = true;
    }

    //Bun이 밑에 재료의 collider와 만났을때 다시 시작 (여기에 애니매이션 넣으면 됨.)
    void OnCollisionEnter2D(Collision2D col)
    {
        showEaten.ShowObtain.InitiateObj();
        InitiateBun();
        EventManager.eventManager.Invoke_GameResumeEvent("InventoryUI");
        EventManager.eventManager.Invoke_BurgerCompleteEvent(true);
    }

    //showEat 태그의 넘버링 받아오기
    public void getIndexCount(int arrIndex)
    {
        getCount = arrIndex;
    }
}
