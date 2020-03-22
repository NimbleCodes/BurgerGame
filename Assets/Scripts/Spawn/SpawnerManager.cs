using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    List<GameObject> spawners;
    int numSpawner = 3;

    void Start()
    {
        EventManager.eventManager.DiffIncEvent += OnDiffIncEvent;
        spawners = new List<GameObject>();
        initSpawners();
    }

    List<Vector3> GetSpawnerPos()
    {
        List<Vector3> spawnerPos = new List<Vector3>();

        Camera main_cam = Camera.main;
        int hnpixels = main_cam.pixelWidth, vnpixels = main_cam.pixelHeight;
        int dx = hnpixels / (numSpawner+1), y = (int)((float)vnpixels * 0.9f);
        for (int i = 0; i < numSpawner; i++)
        {
            Vector3 temp = main_cam.ScreenToWorldPoint(new Vector3((dx * (i+1)),y));
            temp.z = 0;
            spawnerPos.Add(temp);
        }
        return spawnerPos;
    }
    void initSpawners()
    {
        List<Vector3> spawnerPos = GetSpawnerPos();
        for (int i = 0; i < numSpawner; i++)
        {
            if (spawners.Count < i+1)
            {
                GameObject temp = new GameObject();
                temp.name = "Spawner" + i;
                temp.AddComponent<Spawner>();
                temp.GetComponent<Spawner>().randSeed = i;
                temp.GetComponent<Transform>().position = spawnerPos[i];
                spawners.Add(temp);
            }
            else
            {
                spawners[i].GetComponent<Transform>().position = spawnerPos[i];
            }
        }
    }

    void OnDiffIncEvent()
    {
        if (numSpawner < 6) {
            numSpawner++;
            initSpawners();
            EventManager.eventManager.Invoke_NumSpawnerIncEvent(numSpawner);
        }
        //change spawn time?

    }
}
