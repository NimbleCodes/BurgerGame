using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    bool spawnFaster = false;

    public struct spawnerVars
    {
        public float nextSpawnTime;
        public float spawnerSpeed;
        public float fasterSpawnerSpeed;
        public spawnerVars(float _nextSpawnTime, float _spawnerSpeed, float _fasterSpawnerSpeed)
        {
            nextSpawnTime = _nextSpawnTime;
            spawnerSpeed = _spawnerSpeed;
            fasterSpawnerSpeed = _fasterSpawnerSpeed;
        }
    }
    spawnerVars vars;

    private void Awake()
    {
        //디폴트 값으로 스포너 초기화
        vars = new spawnerVars(1f, 10f, 20f);
    }
    private void Start()
    {
        StartCoroutine("ObjSpawnTimer");
    }

    public void ChangeSpawnerVariables(spawnerVars vals)
    {
        vars = vals;
    }
    public void SpawnObjFasterOnce()
    {
        spawnFaster = true;
    }

    IEnumerator ObjSpawnTimer()
    {
        yield return new WaitForSeconds(vars.nextSpawnTime);
        SpawnObjectAtCurPos();
        StartCoroutine("ObjSpawnTimer");
    }
    void SpawnObjectAtCurPos()
    {
        string objToSpawnTag = ChooseObjToSpawn();
        GameObject objToSpawn = ObjectManager.objectManager.getGameObject(objToSpawnTag);
        objToSpawn.transform.position = gameObject.transform.position;
        objToSpawn.SetActive(true);
        float spawnVelocity = -(spawnFaster ? vars.fasterSpawnerSpeed : vars.spawnerSpeed);
        objToSpawn.GetComponent<Rigidbody2D>().velocity = new Vector2(0,spawnVelocity);
        objToSpawn.GetComponent<Ingredient>().OnSpawn();
    }
    string ChooseObjToSpawn()
    {
        GameManager.randMutex.WaitOne();
        int nextObjToSpawnIndex = GameManager.rand.Next(0, ObjectManager.objectManager.poolInfo.Count);
        GameManager.randMutex.ReleaseMutex();

        return ObjectManager.objectManager.poolInfo[nextObjToSpawnIndex].ingreName;
    }
}