using System;
using UnityEngine;

public class TriggerManager : MonoBehaviour
{
    enum triggerType
    {
        Eat = 0,
        Recycle
    }
    public struct triggerSet
    {
        public GameObject[] triggers;
    }
    int numTriggerSet = 6;
    [HideInInspector]
    public Vector2 bottomLeft, topRight;
    triggerSet[] triggerSetArr;
    [Range(0, 1)]
    public float posByScreenPerc;
    public float ySize;

    private void Awake()
    {
        triggerSetArr = new triggerSet[numTriggerSet];
        for(int i = 0; i < numTriggerSet; i++)
        {
            triggerSetArr[i].triggers = new GameObject[Enum.GetNames(typeof(triggerType)).Length];
        }
    }
    private void Start()
    {
        EventManager.eventManager.DiffIncEvent += OnDiffIncEvent;
        InitTriggers();
        RefreshTriggers(0);
    }

    void OnDiffIncEvent(int stageNum)
    {
        RefreshTriggers(stageNum);
    }
    void InitTriggers()
    {
        triggerSetArr = new triggerSet[numTriggerSet];
        for (int i = 0; i < numTriggerSet; i++)
        {
            triggerSetArr[i] = new triggerSet();
            //trigger_type에 정의된 트리거 종류의 수만큼 트리거 오브젝트를 생성
            int numTriggerType = Enum.GetNames(typeof(triggerType)).Length;
            triggerSetArr[i].triggers = new GameObject[numTriggerType];
            Vector2 size = new Vector2((topRight.x - bottomLeft.x)/numTriggerSet,ySize);
            for (int j = 0; j < numTriggerType; j++)
            {
                triggerSetArr[i].triggers[j] = new GameObject();
                triggerSetArr[i].triggers[j].name = ((triggerType)j).ToString() + "Trigger" + i;
                float x = bottomLeft.x + (size.x / 2) + (size.x * i);
                float y = bottomLeft.y + (size.y / 2) + ((topRight.y - bottomLeft.y) * posByScreenPerc);
                triggerSetArr[i].triggers[j].GetComponent<Transform>().position = new Vector3(x, y);
                //초기화 중인 트리거의 종류에 따라 알맞은 트리거 컴포넌트 삽입 및 트리거 컴포넌트 초기화를 위한 변수 값 지정
                switch (j)
                {
                    case (int)triggerType.Eat:
                        triggerSetArr[i].triggers[j].AddComponent<EatTrigger>();
                        break;
                    case (int)triggerType.Recycle:
                        triggerSetArr[i].triggers[j].AddComponent<RecycleTrigger>();
                        break;
                    default:
                        Debug.LogError("Attempt to add invalid trigger type to object");
                        break;
                }
                //2로 나누는 이유: Trigger쪽에서 OverlapBox가 박스의 각 변의 길이의 반을 요구
                triggerSetArr[i].triggers[j].GetComponent<Trigger>().setTrigLayerMask("Ingredients");
                triggerSetArr[i].triggers[j].GetComponent<Trigger>().setSize(size / 2);
                triggerSetArr[i].triggers[j].GetComponent<Trigger>().enabled = false;
            }
        }
    }
    void RefreshTriggers(int curDiff)
    {
        int numActiveTrigger = Difficulty.difficulty.DiffTable.StageDiffVals[curDiff].NumActiveSpawner;
        for(int i = 0; i < numActiveTrigger; i++)
        {
            int numTriggerType = Enum.GetNames(typeof(triggerType)).Length;
            for (int j = 0; j < numTriggerType; j++)
            {
                string key;
                switch (j)
                {
                    case (int)triggerType.Eat:
                        key = Difficulty.difficulty.DiffTable.StageDiffVals[curDiff].EatKey[i];
                        break;
                    case (int)triggerType.Recycle:
                        key = Difficulty.difficulty.DiffTable.StageDiffVals[curDiff].RecycleKey[i];
                        break;
                    default:
                        Debug.Log("Attempt to add invalid key value to a trigger");
                        key = "q";
                        break;
                }
                triggerSetArr[i].triggers[j].GetComponent<Trigger>().ChangeTriggerVariables(
                        new Trigger.triggerVars(
                                key,
                                Difficulty.difficulty.DiffTable.StageDiffVals[curDiff].TriggerOnTime,
                                Difficulty.difficulty.DiffTable.StageDiffVals[curDiff].CoolDownTime
                            )
                    );
                triggerSetArr[i].triggers[j].GetComponent<Trigger>().enabled = true;
            }
        }
    }
}