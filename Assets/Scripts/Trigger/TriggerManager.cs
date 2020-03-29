using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerManager : MonoBehaviour
{
    //트리거의 종류
    enum trigger_type
    {
        Eat = 0,
        Recycle
    }
    //한 위치의 트리거들을 묶어 놓은 구조체
    public struct trigger_set
    {
        public GameObject[] triggers;
    }
    //총 트리거 개수
    public int num_trigger_set = 6;
    //활성 상태인 트리거 개수
    public int num_active_trigger_set = 3;
    public trigger_set[] trigger_set_arr;
    //트리거가 생성될 사각 영역의 두 점(왼쪽-아래, 오른쪽-위)
    Vector2 bottom_left, top_right;
    //트리거의 크기
    public Vector2 size;
    //트리거가 작동하게 되는 입력의 종류 배열
    string[] eatkeyarr = { "q", "w", "e", "a", "s", "d" };
    string[] reckeyarr = { "u", "i", "o", "j", "k", "l" };
    //트리거가 활성화되는 순서를 담은 배열
    int[] activation_order = {2,3,1,4,0,5};

    void ActivateTriggers()
    {
        for(int i = 0; i < num_active_trigger_set; i++)
        {
            for (int j = 0; j < Enum.GetNames(typeof(trigger_type)).Length; j++)
            {
                trigger_set_arr[activation_order[i]].triggers[j].GetComponent<Trigger>().enabled = true;
            }
        }
    }
    //트리거 초기화
    void InitTriggers()
    {
        trigger_set_arr = new trigger_set[num_trigger_set];
        for(int i = 0; i < num_trigger_set; i++)
        {
            trigger_set_arr[i] = new trigger_set();
            //trigger_type에 정의된 트리거 종류의 수만큼 트리거 오브젝트를 생성
            int num_trigger_type = Enum.GetNames(typeof(trigger_type)).Length;
            trigger_set_arr[i].triggers = new GameObject[num_trigger_type];
            for (int j = 0; j < num_trigger_type; j++)
            {
                trigger_set_arr[i].triggers[j] = new GameObject();
                trigger_set_arr[i].triggers[j].name = ((trigger_type)j).ToString() + "Trigger" + i;
                float x = bottom_left.x + (size.x / 2) + (size.x * i);
                float y = bottom_left.y + (size.y / 2);
                trigger_set_arr[i].triggers[j].GetComponent<Transform>().position = new Vector3(x, y);
                //초기화 중인 트리거의 종류에 따라 알맞은 트리거 컴포넌트 삽입 및 트리거 컴포넌트 초기화를 위한 변수 값 지정
                switch (j)
                {
                    case (int)trigger_type.Eat:
                        trigger_set_arr[i].triggers[j].AddComponent<EatTrigger>();
                        trigger_set_arr[i].triggers[j].GetComponent<Trigger>().key = eatkeyarr[i];
                        break;
                    case (int)trigger_type.Recycle:
                        trigger_set_arr[i].triggers[j].AddComponent<RecycleTrigger>();
                        trigger_set_arr[i].triggers[j].GetComponent<Trigger>().key = reckeyarr[i];
                        break;
                    default:
                        Debug.LogError("Attempt to add invalid trigger type to object");
                        break;
                }
                //2로 나누는 이유: Trigger쪽에서 OverlapBox가 박스의 각 변의 길이의 반을 요구
                trigger_set_arr[i].triggers[j].GetComponent<Trigger>().triggeredBy = LayerMask.GetMask("Ingredients");
                trigger_set_arr[i].triggers[j].GetComponent<Trigger>().size = size/2;
                trigger_set_arr[i].triggers[j].GetComponent<Trigger>().enabled = false;
            }
        }
        ActivateTriggers();
    }
    //난이도 상승 이벤트 발생시 실행
    void OnDiffIncEvent()
    {
        num_active_trigger_set++;
        ActivateTriggers();
    }
    private void Start()
    {
        //난이도 상승 이벤트에 subscribe
        EventManager.eventManager.DiffIncEvent += OnDiffIncEvent;
        //트리거가 생성될 영역 지정
        //임시코드
        bottom_left = Camera.main.ScreenToWorldPoint(new Vector3(0, Camera.main.pixelHeight*0.1f));
        top_right = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth,Camera.main.pixelHeight));
        //트리거 생성 영역을 트리거들의 영역이 겹치지 않고 완벽하게 채우도록 하는 크기 계산
        float dx = top_right.x - bottom_left.x;
        size.x = (dx / (num_trigger_set));
        size.y = 1;
        InitTriggers();
    }
}
