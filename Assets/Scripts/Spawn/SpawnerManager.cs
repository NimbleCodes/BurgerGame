using UnityEngine;
using System.Collections;

public class SpawnerManager : MonoBehaviour
{
    GameObject[] spawnerArr;
    [HideInInspector]
    public Vector2 bottomLeft, topRight;
    int numSpawner = 6;

    private void Awake()
    {
        spawnerArr = new GameObject[numSpawner];
    }
    private void Start()
    {
        EventManager.eventManager.DiffIncEvent += OnDiffIncEvent;
        InitSpawners();
        RefreshSpawners(0);
    }

    void OnDiffIncEvent(int stageNum)
    {
        RefreshSpawners(stageNum);
    }
    void InitSpawners()
    {
        spawnerArr = new GameObject[numSpawner];
        for(int i = 0; i < numSpawner; i++)
        {
            spawnerArr[i] = new GameObject();
            spawnerArr[i].name = "Spawner" + i;
            float sizeX = (topRight.x - bottomLeft.x) / numSpawner;
            float x = bottomLeft.x + (sizeX / 2) + (sizeX * i);
            float y = bottomLeft.y + (topRight.y - bottomLeft.y) * 0.9f;
            spawnerArr[i].GetComponent<Transform>().position = new Vector3(x, y);
            spawnerArr[i].AddComponent<Spawner>();
            spawnerArr[i].GetComponent<Spawner>().enabled = false;
        }
    }
    void RefreshSpawners(int curDiff)
    {
        int numActiveSpawner = Difficulty.difficulty.DiffTable.StageDiffVals[curDiff].NumActiveSpawner;
        for(int i = 0; i < numActiveSpawner; i++)
        {
            spawnerArr[i].GetComponent<Spawner>().ChangeSpawnerVariables(
                new Spawner.spawnerVars(
                    Difficulty.difficulty.DiffTable.StageDiffVals[curDiff].SpawnRate[i],
                    Difficulty.difficulty.DiffTable.StageDiffVals[curDiff].SpawnerSpeed,
                    Difficulty.difficulty.DiffTable.StageDiffVals[curDiff].FasterSpawnerSpeed
                    )
                );
            spawnerArr[i].GetComponent<Spawner>().enabled = true;
        }
    }
}