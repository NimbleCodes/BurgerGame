using UnityEngine;
using System;
using System.IO;
public class Difficulty : MonoBehaviour
{
    public static Difficulty difficulty;
    public int maxNumSpawner = 6;
    enum triggerType
    {
        Eat = 0,
        Recycle
    }

    [System.Serializable]
    public struct DiffRow
    {
        public int numActiveSpawner;
        //Spawner
        public float[] spawnedObjSpeed;
        public float[] nextSpawnTime;
    }
    [System.Serializable]
    public struct DiffTable
    {
        public DiffRow[] stageDiffVals;
    }
    public DiffTable diffTable;
    void LoadDiffTableFromJson()
    {
        string DiffTableStr = File.ReadAllText(Application.dataPath + "/Resources/Json/Difficulty.json");
        diffTable = JsonUtility.FromJson<DiffTable>(DiffTableStr);
    }

    public int curDiff = 0;

    private void Awake()
    {
        difficulty = this;
        LoadDiffTableFromJson();
    }
    private void Start()
    {

    }
}
