using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class Difficulty : MonoBehaviour
{
    public static Difficulty difficulty;
    int maxNumDiff = 4;

    private void Awake()
    {
        difficulty = this;

        DiffTable = new diffTable();
        DiffTable.StageDiffVals = new diffRow[maxNumDiff];
        LoadDiffTableFromJson();
    }

    [System.Serializable]
    public struct diffRow
    {
        public int NumActiveSpawner;
        //Spawner
        public float SpawnerSpeed;
        public float FasterSpawnerSpeed;
        public float[] SpawnRate;
        //Trigger
        public float TriggerOnTime;
        public float CoolDownTime;
        public string[] EatKey;
        public string[] RecycleKey;
    }
    [System.Serializable]
    public struct diffTable
    {
        public diffRow[] StageDiffVals;
    }
    public diffTable DiffTable;
    void LoadDiffTableFromJson()
    {
        string DiffTableStr = File.ReadAllText(Application.dataPath + "/Resources/Json/DiffTable.json");
        DiffTable = JsonUtility.FromJson<diffTable>(DiffTableStr);
    }
}
