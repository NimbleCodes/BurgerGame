using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class showEaten : MonoBehaviour
{
    public string ingre = "Lettuce";//for test
    public int arrIndex = 0;
    public static showEaten ShowObtain;
    public GameObject[] ingr;//오브젝트 저장
    public SpriteRenderer spriteR;
    public string Bun = "Bun";
    public bool complete = true;
    public void Awake()
    {
        ShowObtain = this;
    }
    //이벤트 매니저에 함수추가
    public void Start()
    {
        //EventManager.eventManager.IngrObtainedEvent += showEatenToUser;
        ingr = new GameObject[10];
        InitiateObj();
    }
    public void Update()
    {//for test
        if (Input.GetKeyDown(KeyCode.H))
        {
            showEatenToUser(ingre);
        }
    }

    public void InitiateObj()
    {//오브젝트 초기화
        arrIndex = 1;
        for (int i = 0; i < 10; i++)
        {
            ingr[i] = GameObject.FindGameObjectWithTag("showEat" + i);
            spriteR = ingr[i].GetComponent<SpriteRenderer>();
            ingr[i].GetComponent<Rigidbody2D>().gravityScale = 0f;
            ingr[i].GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            ingr[i].GetComponent<Transform>().position = new Vector3(8f, 5f);
            ingr[i].GetComponent<BoxCollider2D>().enabled = false;
            spriteR.enabled = false;
        }
        ingr[0] = GameObject.FindGameObjectWithTag("showEat0");
        spriteR = ingr[0].GetComponent<SpriteRenderer>();
        spriteR.sprite = Resources.Load<Sprite>("Sprites/Ingredients/BelowBun");
        spriteR.enabled = true;
        ingr[0].GetComponent<BoxCollider2D>().size = new Vector3(2f, 0.2f);
        ingr[0].GetComponent<Transform>().position = new Vector3(8f, 2.2f);
        ingr[0].GetComponent<Rigidbody2D>().gravityScale = 1f;
        ingr[0].GetComponent<BoxCollider2D>().enabled = true;
    }
    //먹은 ingre_name을 받아 맞는 sprite를 호출
    public void showEatenToUser(string ingre_info)
    {
        if (ingre_info == "Bun" || ingre_info == "HeadCrap" || ingre_info == "FaceHugger")
        {
            EventManager.eventManager.Invoke_GamePausedEvent("InventoryUI");
            ShowBun.showBun.getIndexCount(arrIndex);
            ShowBun.showBun.SetActiveBun(ingre_info);
            arrIndex = 0;
        }
        else
        {
            //일반재료
            //ingr[arrIndex] = GameObject.FindGameObjectWithTag("showEat" + arrIndex);
            spriteR = ingr[arrIndex].GetComponent<SpriteRenderer>();
            spriteR.sprite = Resources.Load<Sprite>("Sprites/Ingredients/" + ingre_info);
            spriteR.enabled = true;
            ingr[arrIndex].GetComponent<BoxCollider2D>().size = new Vector3(2f, 0.2f);
            ingr[arrIndex].GetComponent<Rigidbody2D>().gravityScale = 1f;
            ingr[arrIndex].GetComponent<BoxCollider2D>().enabled = true;
            arrIndex++;

        }

        /*
        else if(ingre_info=="FaceHugger"){
            spriteR = ingr[arrIndex].GetComponent<SpriteRenderer>();
            spriteR.sprite = Resources.Load<Sprite>("Sprites/Ingredients/" + ingre_info);
            spriteR.enabled = true;
            ingr[arrIndex].GetComponent<BoxCollider2D>().size = new Vector3(2f, 0.1f);
            ingr[arrIndex].GetComponent<Rigidbody2D>().gravityScale = 1f;
            ingr[arrIndex].GetComponent<BoxCollider2D>().enabled = true;
            arrIndex++;
        }
        */
    }
}
