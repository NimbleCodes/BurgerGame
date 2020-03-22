using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerManager : MonoBehaviour
{
    List<GameObject> eatTriggers;
    List<GameObject> recycleTriggers;
    int numTriggers = 3;

    char[] keyorder = { 'q', 'w', 'e', 'r', 't' };

    private void Start()
    {
        EventManager.eventManager.NumSpawnerIncEvent += OnNumSpawnerIncEvent;
        eatTriggers = new List<GameObject>();
        initTriggers();
    }

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
                GameObject temp = new GameObject();
                temp.name = "EatTrigger" + i;
                temp.AddComponent<EatTrigger>();
                //initialize eattrigger members
                temp.GetComponent<Trigger>().size = new Vector2(1, 0.5f);
                temp.GetComponent<Trigger>().key = keyorder[i].ToString();
                temp.GetComponent<Trigger>().triggeredBy = LayerMask.GetMask("Ingredients");

                temp.GetComponent<Transform>().position = triggerPos[i];
                eatTriggers.Add(temp);
            }
            else
            {
                eatTriggers[i].GetComponent<Transform>().position = triggerPos[i];
                //change width of trigger
            }
        }
    }

    void OnNumSpawnerIncEvent(int num)
    {
        numTriggers = num;
        initTriggers();
    }
}
