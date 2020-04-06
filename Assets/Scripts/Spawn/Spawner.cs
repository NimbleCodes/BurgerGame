using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    string[] spawnableObjTypes;
    bool _active;
    public bool active
    {
        set {
            if (value & !_active)
                StartCoroutine("SpawnCoroutine");
            _active = value;
        }
        get { return _active; }
    }
    public float nextSpawnTime;
    public float spawnedObjSpeed;

    string ChooseObjToSpawn()
    {
        return spawnableObjTypes[GameManager.gameManager.getRandNum(spawnableObjTypes.Length)];
    }
    void SpawnObj(string objName, Vector3 position)
    {
        GameObject temp = ObjectManager.objectManager.getGameObject(objName);
        temp.transform.position = position;
        temp.SetActive(true);
    }
    IEnumerator SpawnCoroutine()
    {
        yield return new WaitForSeconds(nextSpawnTime);
        string nameObjToSpawn = ChooseObjToSpawn();
        SpawnObj(nameObjToSpawn, gameObject.GetComponent<Transform>().position);
        if (active)
            StartCoroutine("SpawnCoroutine");
    }

    private void Awake()
    {
        _active = false;
    }
    private void Start()
    {
        ObjectManager.objectManager.GetSpawnableObjTypes(ref spawnableObjTypes);
    }
}
