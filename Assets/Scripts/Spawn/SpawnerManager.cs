using System.Collections.Generic;
using System.Threading;
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
            spawnerArr[Difficulty.difficulty.activationOrder[i]].GetComponent<Spawner>().spawnedObjSpeed = Difficulty.difficulty.diffTable.stageDiffVals[curDiff].spawnedObjSpeed[i];
            spawnerArr[Difficulty.difficulty.activationOrder[i]].GetComponent<Spawner>().baseSpawnTime = Difficulty.difficulty.diffTable.stageDiffVals[curDiff].nextSpawnTime[i];
            spawnerArr[Difficulty.difficulty.activationOrder[i]].GetComponent<Spawner>().enabled = true;
            spawnerArr[Difficulty.difficulty.activationOrder[i]].GetComponent<Spawner>().active = true;
        }
    }
    void DisableAllSpawners()
    {
        foreach(GameObject g in spawnerArr)
        {
            g.GetComponent<Spawner>().active = false;
        }
    }

    Mutex spawnListMut;
    List<string> spawnList;
    const int spawnListMaxSize = 12;
    Roulette burgerIngrRoulette;
    int numBurgerMenu;
    public string getObjToSpawn()
    {
        spawnListMut.WaitOne();
        string ret = spawnList[0];
        spawnList.RemoveAt(0);
        spawnListMut.ReleaseMutex();
        return ret;
    }

    private void Awake()
    {
        burgerIngrRoulette = new BurgerIngrRoulette();
        spawnList = new List<string>();
        spawnListMut = new Mutex();
    }
    private void Start()
    {
        burgerIngrRoulette.createRoulette(BurgerRecipe.burgerRecipe.menu.BurgerMenu[BurgerRecipe.burgerRecipe.curBurgerOrder].BurgerRecipe.Length + 1);
        numBurgerMenu = BurgerRecipe.burgerRecipe.menu.BurgerMenu.Length;

        EventManager.eventManager.RefreshEvent += RefreshSpawners;
        EventManager.eventManager.GamePausedEvent += DisableAllSpawners;
        EventManager.eventManager.GameResumeEvent += RefreshSpawners;

        numSpawner = Difficulty.difficulty.maxNumSpawner;
        spawnerArr = new GameObject[numSpawner];

        InitSpawners();
        RefreshSpawners();
    }
    private void Update()
    {
        if(spawnList.Count < 6)
        {
            spawnListMut.WaitOne();
            while(spawnList.Count < 12)
            {
                string newObjType;
                int spawnInd = burgerIngrRoulette.Spin();
                if(spawnInd >= BurgerRecipe.burgerRecipe.menu.BurgerMenu[BurgerRecipe.burgerRecipe.curBurgerOrder].BurgerRecipe.Length)
                {
                    int randBurgerInd = GameManager.gameManager.getRandNum(numBurgerMenu);
                    int randBurgerIngr = GameManager.gameManager.getRandNum(BurgerRecipe.burgerRecipe.menu.BurgerMenu[randBurgerInd].BurgerRecipe.Length);
                    newObjType = BurgerRecipe.burgerRecipe.menu.BurgerMenu[randBurgerInd].BurgerRecipe[randBurgerIngr];
                }
                else
                {
                    newObjType = BurgerRecipe.burgerRecipe.menu.BurgerMenu[BurgerRecipe.burgerRecipe.curBurgerOrder].BurgerRecipe[spawnInd];
                }
                spawnList.Add(newObjType);
            }
            spawnListMut.ReleaseMutex();
        }
    }
}
