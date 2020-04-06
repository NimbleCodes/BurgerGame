using System;
using UnityEngine;

public class TriggerManager : MonoBehaviour
{
    enum triggerType
    {
        Obtain = 0,
        Return
    }
    public struct triggerSet
    {
        public GameObject[] triggers;
    }
    triggerSet[] triggerSetArr;
    public float ySize = 0.5f;

    const float coolDownTime = .5f;
    const float triggOnTime = .5f;

    public Vector2 bottomLeft, topRight;
    public float yPosByScreenPerc, xPosByScreenPerc;
    int numTriggerSet;

    public string[,] triggerKeys;
    public string[,] defaultTriggerKeys = { 
        {"q", "w", "e", "a", "s", "d"},
        {"i", "o", "p", "k", "l", ";"}
    };

    void InitTriggers()
    {
        triggerSetArr = new triggerSet[numTriggerSet];
        for (int i = 0; i < numTriggerSet; i++)
        {
            triggerSetArr[i] = new triggerSet();
            int numTriggerType = Enum.GetNames(typeof(triggerType)).Length;
            triggerSetArr[i].triggers = new GameObject[numTriggerType];
            
            Vector2 size = new Vector2((topRight.x - bottomLeft.x) / numTriggerSet, ySize);
            float x = bottomLeft.x + ((topRight.x - bottomLeft.x) * xPosByScreenPerc) + (size.x / 2) + (size.x * i);
            float y = bottomLeft.y + ((topRight.y - bottomLeft.y) * yPosByScreenPerc) + (size.y / 2);
            GameObject tempParentRef = new GameObject("TriggerSet_" + i);
            tempParentRef.transform.position = new Vector3(x, y);

            for (int j = 0; j < numTriggerType; j++)
            {
                triggerSetArr[i].triggers[j] = new GameObject();
                triggerSetArr[i].triggers[j].name = ((triggerType)j).ToString() + "Trigger" + i;
                triggerSetArr[i].triggers[j].GetComponent<Transform>().position = new Vector3(x, y);
                triggerSetArr[i].triggers[j].transform.parent = tempParentRef.transform;
                switch (j)
                {
                    case (int)triggerType.Obtain:
                        triggerSetArr[i].triggers[j].AddComponent<ObtainTrigger>();
                        break;
                    case (int)triggerType.Return:
                        triggerSetArr[i].triggers[j].AddComponent<ReturnTrigger>();
                        break;
                    default:
                        Debug.LogError("Attempt to add invalid trigger type to object");
                        break;
                }
                //2로 나누는 이유: Trigger쪽에서 OverlapBox가 박스의 각 변의 길이의 반을 요구
                triggerSetArr[i].triggers[j].GetComponent<Trigger>().triggeredBy = LayerMask.GetMask("Ingredients");
                triggerSetArr[i].triggers[j].GetComponent<Trigger>().size = (size / 2);
                triggerSetArr[i].triggers[j].GetComponent<Trigger>().active = false;
            }
        }
    }
    void RefreshTriggers()
    {
        int curDiff = Difficulty.difficulty.curDiff;

        int numActiveSpawner = Difficulty.difficulty.diffTable.stageDiffVals[curDiff].numActiveSpawner;
        for (int i = 0; i < numActiveSpawner; i++)
        {
            for(int j = 0; j < Enum.GetNames(typeof(triggerType)).Length; j++)
            {
                triggerSetArr[i].triggers[j].GetComponent<Trigger>().active = true;
                triggerSetArr[i].triggers[j].GetComponent<Trigger>().key = triggerKeys[j, i];
            }
        }
    }

    private void Start()
    {
        EventManager.eventManager.RefreshEvent += RefreshTriggers;

        numTriggerSet = Difficulty.difficulty.maxNumSpawner;
        triggerSetArr = new triggerSet[numTriggerSet];
        for (int i = 0; i < numTriggerSet; i++)
        {
            triggerSetArr[i].triggers = new GameObject[Enum.GetNames(typeof(triggerType)).Length];
        }
        triggerKeys = new string[Enum.GetNames(typeof(triggerType)).Length, Difficulty.difficulty.maxNumSpawner];
        for (int i = 0; i < Enum.GetNames(typeof(triggerType)).Length; i++)
        {
            for (int j = 0; j < Difficulty.difficulty.maxNumSpawner; j++)
            {
                triggerKeys[i, j] = defaultTriggerKeys[i, j];
            }
        }

        InitTriggers();
        RefreshTriggers();
    }
}
