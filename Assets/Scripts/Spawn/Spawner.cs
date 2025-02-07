﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    SpawnerManager ref2manager;

    bool _active = false;
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
    public float baseSpawnTime;

    public float spawnedObjGravScale;

    public string spawnObjType;
    /*
    //Roulette burgerIngrRoulette;
    int RandomIngr = 1;
    //스폰 할 오브젝트를 결정하는 함수 -> 추후 수정 가능

    void OnBurgerComplete(bool cor)
    {
        int numNeededIngrTypes = BurgerRecipe.burgerRecipe.menu.BurgerMenu[BurgerRecipe.burgerRecipe.curBurgerOrder].BurgerRecipe.Length;
        burgerIngrRoulette.createRoulette(numNeededIngrTypes + RandomIngr);
    }
    */
    List<string> spawnableObjTypes;
    string ChooseObjToSpawn()
    {
        /*
        int chosenIngrInd = burgerIngrRoulette.Spin();
        int numNeededIngrTypes = BurgerRecipe.burgerRecipe.menu.BurgerMenu[BurgerRecipe.burgerRecipe.curBurgerOrder].BurgerRecipe.Length;
        Debug.Log(burgerIngrRoulette.ToString());
        if (chosenIngrInd >= numNeededIngrTypes)
        {
            return spawnableObjTypes[GameManager.gameManager.getRandNum(spawnableObjTypes.Count)];
        }
        return BurgerRecipe.burgerRecipe.menu.BurgerMenu[BurgerRecipe.burgerRecipe.curBurgerOrder].BurgerRecipe[chosenIngrInd];
        */
        return ref2manager.getObjToSpawn();
    }
    //오브젝트를 현재 위치에 생성
    void SpawnObj(string objName, Vector3 position)
    {
        GameObject temp = ObjectManager.objectManager.GetGameObject(objName);
        ObjectManager.objectManager.AddToActiveList(temp);
        if (temp == null)
            return;
        temp.transform.position = position;
        temp.GetComponent<Rigidbody2D>().gravityScale = spawnedObjGravScale;
        temp.SetActive(true);

        int rand = GameManager.gameManager.getRandNum(99);
        nextSpawnTime = baseSpawnTime + ((1f + rand) / 50);
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
        int rand = GameManager.gameManager.getRandNum(99);
        nextSpawnTime = baseSpawnTime + ((1f + rand) / 25);
        //burgerIngrRoulette = new BurgerIngrRoulette();
    }
    private void Start()
    {
        //EventManager.eventManager.BurgerCompleteEvent += OnBurgerComplete;
        //int numNeededIngrTypes = BurgerRecipe.burgerRecipe.menu.BurgerMenu[BurgerRecipe.burgerRecipe.curBurgerOrder].BurgerRecipe.Length;
        //burgerIngrRoulette.createRoulette(numNeededIngrTypes + RandomIngr);
        spawnableObjTypes = new List<string>();
        spawnableObjTypes = ObjectManager.objectManager.GetSpawnableObjNames();
        ref2manager = FindObjectOfType<SpawnerManager>();
    }
}
