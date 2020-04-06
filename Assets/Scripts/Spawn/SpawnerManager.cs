using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    GameObject[] spawnerArr;

    public Vector2 bottomLeft, topRight;
    public float yPosByScreenPerc, xPosByScreenPerc;
    int numSpawner;

    void InitSpawners()
    {
        spawnerArr = new GameObject[numSpawner];
        for (int i = 0; i < numSpawner; i++)
        {
            spawnerArr[i] = new GameObject();
            spawnerArr[i].name = "Spawner_" + i;
            float sizeX = (topRight.x - bottomLeft.x) / numSpawner;
            float x = bottomLeft.x + (topRight.x - bottomLeft.x) * xPosByScreenPerc + (sizeX / 2) + (sizeX * i);
            float y = bottomLeft.y + (topRight.y - bottomLeft.y) * yPosByScreenPerc;
            spawnerArr[i].GetComponent<Transform>().position = new Vector3(x, y);
            spawnerArr[i].AddComponent<Spawner>();
        }
    }
    void RefreshSpawners()
    {
        int curDiff = Difficulty.difficulty.curDiff;

        int numActiveSpawner = Difficulty.difficulty.diffTable.stageDiffVals[curDiff].numActiveSpawner;
        for (int i = 0; i < numActiveSpawner; i++)
        {
            spawnerArr[i].GetComponent<Spawner>().active = true;
            spawnerArr[i].GetComponent<Spawner>().spawnedObjSpeed = Difficulty.difficulty.diffTable.stageDiffVals[curDiff].spawnedObjSpeed[i];
            spawnerArr[i].GetComponent<Spawner>().nextSpawnTime = Difficulty.difficulty.diffTable.stageDiffVals[curDiff].nextSpawnTime[i];
            spawnerArr[i].GetComponent<Spawner>().enabled = true;
        }
    }

    private void Start()
    {
        EventManager.eventManager.RefreshEvent += RefreshSpawners;

        numSpawner = Difficulty.difficulty.maxNumSpawner;
        spawnerArr = new GameObject[numSpawner];

        InitSpawners();
        RefreshSpawners();
    }
}
