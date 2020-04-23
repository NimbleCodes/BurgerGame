using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectManager : MonoBehaviour
{
    //SINGLETON INSTANCE
    public static ObjectManager objectManager;

    //OBJECT POOLS
    protected Dictionary<string, Queue<GameObject>> objPools;
    protected List<string> spawnableObjNames;
    protected int poolSize = 10;
    protected abstract void InitObjPools();

    //ACTIVE OBJECT LIST
    List<GameObject> curActiveObj;
    public void AddToActiveList(GameObject g)
    {
        curActiveObj.Add(g);
    }
    public void RemoveFromActiveList(GameObject g)
    {
        curActiveObj.Remove(g);
    }
    void DisableAllActive(bool cor)
    {
        if (cor)
        {
            Debug.Log(curActiveObj.Count);
            for(int i = 0; i < curActiveObj.Count; i++)
            {
                curActiveObj[i].SetActive(false);
            }
        }
    }

    //REACT TO EVENTS
    void OnGamePaused()
    {
        foreach(GameObject g in curActiveObj)
        {
            g.GetComponent<Rigidbody2D>().gravityScale = 0;
            g.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        }
    }
    void OnGameResume()
    {
        foreach (GameObject g in curActiveObj)
        {
            g.GetComponent<Rigidbody2D>().gravityScale = 1;
        }
    }

    //USE THIS
    public GameObject GetGameObject(string objName)
    {
        if (objPools.ContainsKey(objName))
        {
            GameObject temp = objPools[objName].Dequeue();
            objPools[objName].Enqueue(temp);
            return temp;
        }
        return null;
    }
    public List<string> GetSpawnableObjNames()
    {
        return spawnableObjNames;
    }

    //MONOBEHAVIOUR
    private void Awake()
    {
        objectManager = this;
        objPools = new Dictionary<string, Queue<GameObject>>();
        spawnableObjNames = new List<string>();
        curActiveObj = new List<GameObject>();
        InitObjPools();
    }
    private void Start()
    {
        EventManager.eventManager.GamePausedEvent += OnGamePaused;
        EventManager.eventManager.GameResumeEvent += OnGameResume;
        EventManager.eventManager.BurgerCompleteEvent += DisableAllActive;
    }
}
