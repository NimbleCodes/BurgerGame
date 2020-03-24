using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    List<GameObject> spawners;
    Vector3 bottom_left, top_right; //스포너가 생성될 영역
    int num_spawner;                //생성될 스포너의 개수

    /*-----------------------------스포너 오류 관련-----------------------------*/
    bool SpawnerError = false;
    float SpawnerErrorTime;
    IEnumerator SpawnerErrorTimer(){
        yield return new WaitForSeconds(SpawnerErrorTime);
        SpawnerError = true;
    }
    System.Random rand;
    int ChooseRandomSpawner(){
        return rand.Next(0,spawners.Count);
    }
    void FasterSpawnedObject(){
        int chosen_one = ChooseRandomSpawner();
        spawners[chosen_one].GetComponent<Spawner>().SpawnerErrorOccured = true;
    }
    /*-----------------------------스포너 오류 관련-----------------------------*/

    /*----------------------------스포너 초기화 관련----------------------------*/
    bool ValidateInput_GetSpawnerPos(Vector3 bottom_left, Vector3 top_right){
        Vector3 mcam_bottom_left = Camera.main.ScreenToWorldPoint(new Vector3(0,0));
        Vector3 mcam_top_right = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight));

        if(bottom_left.x < top_right.x && bottom_left.x >= mcam_bottom_left.x && top_right.x <= mcam_top_right.x){
            if(bottom_left.y < top_right.y && bottom_left.y >= mcam_bottom_left.y && top_right.y <= mcam_top_right.y)
                return true;
        }
        return false;
    }
    List<Vector3> GetSpawnerPos(Vector3 bottom_left, Vector3 top_right)
    {
        //입력 타당성 검사
        if(!ValidateInput_GetSpawnerPos(bottom_left, top_right)){
            Debug.LogError("SpawnerManager: invalid input for function GetSpawnerPos");
            return null;
        }
        //스포너 위치 계산 및 반환
        List<Vector3> output = new List<Vector3>();
        for(int i = 0; i < num_spawner; i++){
            float deltaX = top_right.x - bottom_left.x;
            float dx = deltaX / (num_spawner+1);
            float y = bottom_left.y + (top_right.y - bottom_left.y) * 0.9f;  //선택된 영역의 90%에 해당되는 y값
            output.Add(new Vector3(bottom_left.x + dx * (i+1), y));
        }
        return output;
    }
    void initSpawners()
    {
        List<Vector3> spawner_pos = GetSpawnerPos(bottom_left, top_right);
        if(spawner_pos == null){
            //disable all spawners
            foreach(GameObject g in spawners){
                g.SetActive(false);
            }
            num_spawner = 0;
            return;
        }
        //스포너 초기화
        for(int i = 0; i < num_spawner; i++){
            if(spawners.Count >= i+1){
                //이미 생성된 스포너의 위치 변경
                spawners[i].GetComponent<Transform>().position = spawner_pos[i];
            }
            else{
                //새 스포너 오브젝트를 생성 
                GameObject g = new GameObject();
                g.GetComponent<Transform>().position = spawner_pos[i];
                g.name = "Spawner" + i;
                
                g.AddComponent<Spawner>();
                g.GetComponent<Spawner>().nextSpawnTime = 1f;
                g.GetComponent<Spawner>().randSeed = i;
                
                spawners.Add(g);
            }
        }
    }
    /*----------------------------스포너 초기화 관련----------------------------*/

    /*-------------------------------이벤트 관련--------------------------------*/
    void OnDiffIncEvent()
    {
        num_spawner++;
        initSpawners();
    }
    /*-------------------------------이벤트 관련--------------------------------*/

    private void Start()
    {
        EventManager.eventManager.DiffIncEvent += OnDiffIncEvent;
        spawners = new List<GameObject>();
        rand = new System.Random();

        //임시 코드
        bottom_left = Camera.main.ScreenToWorldPoint(new Vector3(0,0));
        top_right = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth,Camera.main.pixelHeight));
        num_spawner = 3;
        SpawnerErrorTime = 5f;

        initSpawners();
        StartCoroutine("SpawnerErrorTimer");
    }

    private void Update() {
        if(SpawnerError){
            FasterSpawnedObject();
            SpawnerError = false;
            StartCoroutine("SpawnerErrorTimer");
        }
    }
}
