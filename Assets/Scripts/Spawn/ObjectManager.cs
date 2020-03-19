using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public static ObjectManager objectManager;

    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }
    public List<Pool> poolInfo;

    Dictionary<string, Queue<GameObject>> objPools;
    private void Awake()
    {
        objectManager = this;
    }
    private void Start()
    {
        objPools = new Dictionary<string, Queue<GameObject>>();
        foreach (Pool pool in poolInfo)
        {
            Queue<GameObject> objPool = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject temp = Instantiate(pool.prefab);
                temp.SetActive(false);
                objPool.Enqueue(temp);
            }
            objPools.Add(pool.tag, objPool);
        }
    }
    public GameObject getGameObject(string tag)
    {
        if (objPools.ContainsKey(tag))
        {
            GameObject temp = objPools[tag].Dequeue();
            objPools[tag].Enqueue(temp);
            return temp;
        }
        else
        {
            Debug.Log("ObjectManager: Invalid tag value from spawner");
            return null;
        }
    }
}
