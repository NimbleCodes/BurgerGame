using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerManager : MonoBehaviour
{
    List<GameObject> eatTriggers;
    List<GameObject> recycleTriggers;
    int numTriggers = 3;

    string[] ETrigKeyOrder = { "q", "w", "e", "r", "t" };
    string[] RTrigKeyOrder = { "y", "u", "i", "o", "p" };

    /*----------------------------트리거 초기화 관련----------------------------*/
    List<Vector3> GetTriggerPos()
    {
        List<Vector3> triggerPos = new List<Vector3>();

        Camera main_cam = Camera.main;
        int hnpixels = main_cam.pixelWidth, vnpixels = main_cam.pixelHeight;
        int dx = hnpixels / (numTriggers + 1), y = (int)((float)vnpixels * 0.2f);
        for (int i = 0; i < numTriggers; i++)
        {
            Vector3 temp = main_cam.ScreenToWorldPoint(new Vector3((dx * (i + 1)), y));
            temp.z = 0;
            triggerPos.Add(temp);
        }
        return triggerPos;
    }
    void initTriggers()
    {
        List<Vector3> triggerPos = GetTriggerPos();
        for(int i = 0; i < numTriggers; i++)
        {
            if(eatTriggers.Count < i + 1)
            {
                //EatTrigger 초기화
                GameObject temp = new GameObject();
                temp.name = "EatTrigger" + i;
                temp.AddComponent<EatTrigger>();
                temp.GetComponent<Trigger>().size = new Vector2(1, 0.5f);
                temp.GetComponent<Trigger>().key = ETrigKeyOrder[i];
                temp.GetComponent<Trigger>().triggeredBy = LayerMask.GetMask("Ingredients");
                temp.GetComponent<Transform>().position = triggerPos[i];
                eatTriggers.Add(temp);
                //RecycleTrigger 초기화
                temp = new GameObject();
                temp.name = "RecycleTrigger" + i;
                temp.AddComponent<RecycleTrigger>();
                temp.GetComponent<Trigger>().size = new Vector2(1, 0.5f);
                temp.GetComponent<Trigger>().key = RTrigKeyOrder[i];
                temp.GetComponent<Trigger>().triggeredBy = LayerMask.GetMask("Ingredients");
                temp.GetComponent<Transform>().position = triggerPos[i];
                recycleTriggers.Add(temp);
            }
            else
            {
                eatTriggers[i].GetComponent<Transform>().position = triggerPos[i];
                recycleTriggers[i].GetComponent<Transform>().position = triggerPos[i];
                //change width of trigger
            }
        }
    }
    /*----------------------------트리거 초기화 관련----------------------------*/

    /*-------------------------------이벤트 관련--------------------------------*/
    void OnNumSpawnerIncEvent(int num)
    {
        numTriggers = num;
        initTriggers();
    }
    /*-------------------------------이벤트 관련--------------------------------*/

    private void Start()
    {
        EventManager.eventManager.NumSpawnerIncEvent += OnNumSpawnerIncEvent;
        eatTriggers = new List<GameObject>();
        recycleTriggers = new List<GameObject>();
        initTriggers();
    }
}
