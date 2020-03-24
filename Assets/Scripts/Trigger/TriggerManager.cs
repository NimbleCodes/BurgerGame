using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerManager : MonoBehaviour
{
    enum trigger_type{
        Eat = 0,
        Recycle
    }
    struct trigger_set{
        public GameObject[] triggers;
    }
    List<trigger_set> trigger_set_list;

    Vector3 bottom_left, top_right; //트리거가 생성될 영역
    int num_trigger_sets;           //트리거의 개수

    /*----------------------------트리거 초기화 관련----------------------------*/
    bool ValidateInput_GetTriggerPos(Vector3 bottom_left, Vector3 top_right){
        Vector3 mcam_bottom_left = Camera.main.ScreenToWorldPoint(new Vector3(0,0));
        Vector3 mcam_top_right = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight));

        if(bottom_left.x < top_right.x && bottom_left.x >= mcam_bottom_left.x && top_right.x <= mcam_top_right.x){
            if(bottom_left.y < top_right.y && bottom_left.y >= mcam_bottom_left.y && top_right.y <= mcam_top_right.y)
                return true;
        }
        return false;
    }
    List<Vector3> GetTriggerPos(Vector3 bottom_left, Vector3 top_right)
    {
        //입력 타당성 검사
        if(!ValidateInput_GetTriggerPos(bottom_left, top_right)){
            Debug.LogError("TriggerManager: invalid input for function GetTriggerPos");
            return null;
        }
        //스포너 위치 계산 및 반환
        List<Vector3> output = new List<Vector3>();
        for(int i = 0; i < num_trigger_sets; i++){
            float deltaX = top_right.x - bottom_left.x;
            float dx = deltaX / (num_trigger_sets+1);
            float y = bottom_left.y + (top_right.y - bottom_left.y) * 0.2f;  //선택된 영역의 90%에 해당되는 y값
            output.Add(new Vector3(bottom_left.x + dx * (i+1), y));
        }
        return output;
    }
    void initTriggers()
    {
        List<Vector3> trigger_pos = GetTriggerPos(bottom_left, top_right);
        if(trigger_pos == null){
            //disable all spawners
            foreach(trigger_set ts in trigger_set_list){
                foreach(GameObject g in ts.triggers){
                    g.SetActive(false);
                }
            }
            num_trigger_sets = 0;
            return;
        }
        //트리거 초기화
        for(int i = 0; i < num_trigger_sets; i++){
            if(trigger_set_list.Count >= i+1){
                //이미 생성된 트리거 위치 변경
                foreach(GameObject g in trigger_set_list[i].triggers){
                    g.GetComponent<Transform>().position = trigger_pos[i];
                }
            }
            else{
                //새 트리거 오브젝트 생성
                trigger_set ts = new trigger_set();
                ts.triggers = new GameObject[Enum.GetNames(typeof(trigger_type)).Length];
                ts.triggers[(int)trigger_type.Eat] = new GameObject();
                ts.triggers[(int)trigger_type.Eat].AddComponent<EatTrigger>();
                ts.triggers[(int)trigger_type.Eat].name = "EatTrigger" + i;

                ts.triggers[(int)trigger_type.Recycle] = new GameObject();
                ts.triggers[(int)trigger_type.Recycle].AddComponent<RecycleTrigger>();
                ts.triggers[(int)trigger_type.Recycle].name = "RecycleTrigger" + i;

                //공통 변수 초기화
                foreach(GameObject g in ts.triggers){
                    g.GetComponent<Transform>().position = trigger_pos[i];
                    g.GetComponent<Trigger>().triggeredBy = LayerMask.GetMask("Ingredients");
                    g.GetComponent<Trigger>().size = new Vector2(1,1);
                }
                trigger_set_list.Add(ts);
            }
        }
    }
    /*----------------------------트리거 초기화 관련----------------------------*/

    /*-------------------------------이벤트 관련--------------------------------*/
    void OnDiffIncEvent()
    {
        num_trigger_sets++;
        initTriggers();
    }
    /*-------------------------------이벤트 관련--------------------------------*/

    private void Start()
    {
        EventManager.eventManager.DiffIncEvent += OnDiffIncEvent;
        trigger_set_list = new List<trigger_set>();

        //임시 코드
        bottom_left = Camera.main.ScreenToWorldPoint(new Vector3(0,0,0));
        top_right = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth,Camera.main.pixelHeight,0));
        num_trigger_sets = 3;

        initTriggers();
    }
}
