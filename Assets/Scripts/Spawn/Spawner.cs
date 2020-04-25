using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    string[] spawnableObjTypes;
    bool _active;
    public bool active
    {
        //active가 false -> true가 되면 스포닝 코루틴 시작
        set {
            if (value & !_active & !coroutineRunning)
            {
                coroutineRunning = true;
                StartCoroutine("SpawnCoroutine");
            }
            _active = value;
        }
        get { return _active; }
    }
    bool coroutineRunning = false;
    //스포닝 사이 시간
    public float nextSpawnTime;
    //현재는 안쓰는 중 >> 리지드 바디 중력 기능 사용중
    public float spawnedObjSpeed;

    //스폰 할 오브젝트를 결정하는 함수 -> 추후 수정 가능
    string ChooseObjToSpawn()
    {
        int randInd = GameManager.gameManager.getRandNum(BurgerRecipe.burgerRecipe.menu.BurgerMenu[BurgerRecipe.burgerRecipe.curBurgerOrder].BurgerRecipe.Length);
        return (BurgerRecipe.burgerRecipe.menu.BurgerMenu[BurgerRecipe.burgerRecipe.curBurgerOrder].BurgerRecipe[randInd]);
    }
    //오브젝트를 현재 위치에 생성
    void SpawnObj(string objName, Vector3 position)
    {
        GameObject temp = ObjectManager.objectManager.GetGameObject(objName);
        if (temp == null)
            return;
        temp.transform.position = position;
        temp.SetActive(true);
    }
    //일정 시간마다 오브젝트를 생성하는 코루틴
    IEnumerator SpawnCoroutine()
    {
        yield return new WaitForSeconds(nextSpawnTime);
        if (active)
        {
            string nameObjToSpawn = ChooseObjToSpawn();
            SpawnObj(nameObjToSpawn, gameObject.GetComponent<Transform>().position);
            StartCoroutine("SpawnCoroutine");
        }
        else
            coroutineRunning = false;
    }

    private void Awake()
    {
        _active = false;
    }
    private void Start()
    {
        //생성 가능한 오브젝트들의 이름을 오브젝트 매니져에게서 받아온다
        //ObjectManager.objectManager.GetSpawnableObjTypes(ref spawnableObjTypes);
    }
}
