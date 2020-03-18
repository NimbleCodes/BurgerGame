using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    bool valid = false;

    bool poolObj = false;
    ObjectManager pooler;
    public string objTag = "";

    public GameObject prefab;
    List<GameObject> spawnedObjects;

    //counter
    public float countdown = 3f;
    float timer;

    private void Start()
    {
        if(pooler == null)
        {
            pooler = FindObjectOfType<ObjectManager>();
        }
        if (pooler && !objTag.Equals(""))
        {
            valid = poolObj = true;
            timer = countdown;
        }
        else if (prefab)
        {
            valid = true;
            timer = countdown;
            spawnedObjects = new List<GameObject>();
        }
        else
        {
            Debug.Log("Spawner: Invalid spawner settings");
        }
    }
    private void Update()
    {
        if (valid)
        {
            if (timer < 0f)
            {
                if (poolObj)
                {
                    GameObject g = pooler.getGameObject(objTag);
                    g.transform.position = gameObject.GetComponent<Transform>().position;
                    g.SetActive(true);
                    g.GetComponent<ISpawnedObject>().OnSpawn();
                }
                else
                {
                    GameObject spawnedObj = Instantiate(prefab);
                    spawnedObjects.Add(spawnedObj);
                }
                timer = countdown;
            }
            else
            {
                timer -= Time.deltaTime;
            }
        }
    }
}
