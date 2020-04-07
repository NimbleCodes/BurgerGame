using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    GameObject[] spawnerArr;

    //스포너가 생성될 사각 영역을 지정하는 변수들
    //GridArea.cs에서 초기화 해준다
    public Vector2 bottomLeft, topRight;
    //bottomLeft, topRight에 지정된 영역의 어느 위치에 생성될지 조절 할 수 있는 변수
    //예) yPosByScreenPerc = 0.9이면 y = 0.9 * 영역의y길이 위치에 생성
    public float yPosByScreenPerc, xPosByScreenPerc;
    int numSpawner;

    //스포너 위치 초기화
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
    //스포너 정보를 수정하는 함수
    //난이도 상승시 사용
    //refresh 이벤트 발생 시 호출
    void RefreshSpawners()
    {
        int curDiff = Difficulty.difficulty.curDiff;

        int numActiveSpawner = Difficulty.difficulty.diffTable.stageDiffVals[curDiff].numActiveSpawner;
        for (int i = 0; i < numActiveSpawner; i++)
        {
            spawnerArr[Difficulty.difficulty.activationOrder[i]].GetComponent<Spawner>().active = true;
            spawnerArr[Difficulty.difficulty.activationOrder[i]].GetComponent<Spawner>().spawnedObjSpeed = Difficulty.difficulty.diffTable.stageDiffVals[curDiff].spawnedObjSpeed[i];
            spawnerArr[Difficulty.difficulty.activationOrder[i]].GetComponent<Spawner>().nextSpawnTime = Difficulty.difficulty.diffTable.stageDiffVals[curDiff].nextSpawnTime[i];
            spawnerArr[Difficulty.difficulty.activationOrder[i]].GetComponent<Spawner>().enabled = true;
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
