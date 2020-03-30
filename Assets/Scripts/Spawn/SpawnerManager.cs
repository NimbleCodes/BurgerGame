using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpawnerManager : MonoBehaviour
{
    GameObject[] spawner_arr;
    //스포너가 생성될 사각 영역의 두 점(왼쪽-아래, 오른쪽-위)
    public Vector2 bottom_left, top_right;
    //스포너가 활성호하되는 순서
    int[] activation_order = { 2, 3, 1, 4, 0, 5 };
    int num_spawner = 6;
    int num_active_spawner = 3;
    //각 스포너에 할당 될 스폰 주기
    float[] spawn_rate;
    //랜덤 선택 기능
    System.Random rand;
    public float faster_spawn_time = 5f;
    bool faster_spawn_cont = true;
    
    int ChooseRandomSpawner()
    {
        int chosen_one = activation_order[rand.Next(0, num_active_spawner)];
        return chosen_one;
    }
    void SpawnObjFaster(int ind)
    {
        spawner_arr[ind].GetComponent<Spawner>().SpawnerErrorOccured = true;
    }
    IEnumerator FasterSpawnTimer()
    {
        yield return new WaitForSeconds(faster_spawn_time);
        SpawnObjFaster(ChooseRandomSpawner());
        if (faster_spawn_cont)
            StartCoroutine("FasterSpawnTimer");
    }
    void ActivateSpawners()
    {
        for (int i = 0; i < num_active_spawner; i++)
        {
            spawner_arr[activation_order[i]].GetComponent<Spawner>().enabled = true;
        }
    }
    void InitSpawners()
    {
        spawner_arr = new GameObject[num_spawner];
        for(int i = 0; i < num_spawner; i++)
        {
            spawner_arr[i] = new GameObject();
            spawner_arr[i].name = "Spawner" + i;
            float sizex = (top_right.x - bottom_left.x) / num_spawner;
            float x = bottom_left.x + (sizex / 2) + (sizex * i);
            float y = bottom_left.y + (top_right.y - bottom_left.y) * 0.9f;
            spawner_arr[i].GetComponent<Transform>().position = new Vector3(x, y);

            spawner_arr[i].AddComponent<Spawner>();
            spawner_arr[i].GetComponent<Spawner>().nextSpawnTime = spawn_rate[i];
            //재료 선택할 때 사용하는 랜덤 객체 시드 값
            spawner_arr[i].GetComponent<Spawner>().randSeed = i;
            spawner_arr[i].GetComponent<Spawner>().enabled = false;
        }
        ActivateSpawners();
    }
    void OnDiffIncEvent()
    {
        num_active_spawner++;
        ActivateSpawners();
    }
    private void Start()
    {
        rand = new System.Random();

        EventManager.eventManager.DiffIncEvent += OnDiffIncEvent;
        /*
        //트리거가 생성될 영역 지정
        //임시코드
        bottom_left = Camera.main.ScreenToWorldPoint(new Vector3(0, Camera.main.pixelHeight*0.1f));
        top_right = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight));
        */
        //spawn_rate 값 초기화
        spawn_rate = new float[num_spawner];
        //임시코드
        for(int i = 0; i < num_spawner; i++)
        {
            spawn_rate[i] = 0.25f * (i + 1);
        }
        InitSpawners();

        StartCoroutine("FasterSpawnTimer");
    }
}
