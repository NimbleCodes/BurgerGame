using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;
public class TriggerManager : MonoBehaviour
{
    //트리거 종류
    enum triggerType
    {
        Obtain = 0,
        Return
    }
    //트리거의 종류를 위치별로 묶은 구조체
    public struct triggerSet
    {
        public GameObject[] triggers;
    }
    triggerSet[] triggerSetArr;
    //트리거의 세로 길이
    public float ySize = 0.5f;

    const float coolDownTime = .5f;
    const float triggOnTime = .5f;

    //트리거가 생성될 영역
    public Vector2 bottomLeft, topRight;
    public float yPosByScreenPerc, xPosByScreenPerc;
    int numTriggerSet;

    public TextMeshProUGUI[] Eat_TMap;
    public TextMeshProUGUI[] Throw_TMap;

    //UI에 버튼매핑 보여주기
    void InitButtonMap(){
        for(int b = 0; b < 1; b++){
            for(int n = 0; n < 6; n++)
            {
                Eat_TMap[n].text = triggerKeys[b,n].ToUpper();
            }
        }
        for(int b = 1; b < 2; b++){
            for(int n = 0; n < 6; n++){
                Throw_TMap[n].text = triggerKeys[b,n].ToUpper();
            }
        }
    }

    //트리거 초기화
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
                triggerSetArr[Difficulty.difficulty.activationOrder[i]].triggers[j].GetComponent<Trigger>().active = true;
                triggerSetArr[Difficulty.difficulty.activationOrder[i]].triggers[j].GetComponent<Trigger>().key = triggerKeys[j, Difficulty.difficulty.activationOrder[i]];
            }
        }
    }
    void DisableAllTriggers()
    {
        foreach(triggerSet ts in triggerSetArr)
        {
            foreach(GameObject t in ts.triggers)
            {
                t.GetComponent<Trigger>().enabled = false;
            }
        }
    }

    //트리거의 키를 변화하고 싶을 때 triggerKeys를 변경하고 refresh호출
    enum patternNames
    {
        Default = 0,
        Pattern0,
        Pattern1,
        Pattern2
    };
    public string[,] triggerKeys;
    /* 트리거 변환법칙
        - 디폴트
        - 첫번째 좌우반전
        - 두번째 상하반전
        - 세번째 정상키 + 키늘림
    */
    public string[,,] keyPatterns = {
        {
            {"q", "w", "e", "i", "o", "p"},
            {"a", "s", "d", "k", "l", ";"}
        },
        {
            {"i", "o", "p", "q", "w", "e"},
            {";", "l", ";", "a", "s", "d"}
        },
        {
            {"a", "s", "d", "k", "l", ";"},
            {"q", "w", "e", "i", "o", "p"},
        },
        {
            {";", "l", ";", "a", "s", "d"},
            {"i", "o", "p", "q", "w", "e"}
        }
    };
    public void changeTriggerKeys_index(int ind)
    {
        int numActive = Difficulty.difficulty.diffTable.stageDiffVals[Difficulty.difficulty.curDiff].numActiveSpawner;
        for (int i = 0; i < Enum.GetNames(typeof(triggerType)).Length; i++)
        {
            for(int j = 0; j < numTriggerSet; j++)
            {
                bool active = false;
                for(int k = 0; k < numActive; k++)
                {
                    if(Difficulty.difficulty.activationOrder[k] == j)
                    {
                        active = true;
                    }
                }
                if (!active)
                {
                    triggerKeys[i, j] = " ";
                }
                else
                {
                    triggerKeys[i, j] = keyPatterns[ind, i, j];
                }
            }
        }
        RefreshTriggers();
        InitButtonMap();
    }
    public void changeTriggerKeys_random()
    {
        int randNum = GameManager.gameManager.getRandNum(Enum.GetNames(typeof(patternNames)).Length);
        changeTriggerKeys_index(randNum);
    }

    private void Start()
    {
        EventManager.eventManager.RefreshEvent += RefreshTriggers;
        EventManager.eventManager.GameOverEvent += DisableAllTriggers;

        numTriggerSet = Difficulty.difficulty.maxNumSpawner;
        triggerSetArr = new triggerSet[numTriggerSet];
        for (int i = 0; i < numTriggerSet; i++)
        {
            triggerSetArr[i].triggers = new GameObject[Enum.GetNames(typeof(triggerType)).Length];
        }
        triggerKeys = new string[Enum.GetNames(typeof(triggerType)).Length, Difficulty.difficulty.maxNumSpawner];
        InitTriggers();
        changeTriggerKeys_index(0);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)){
            changeTriggerKeys_random();
        }
    }
}
