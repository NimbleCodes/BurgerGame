using UnityEngine;
using System;
using System.IO;
public class Difficulty : MonoBehaviour
{
    //싱글턴
    public static Difficulty difficulty;
    //총 스포너 개수
    public int maxNumSpawner = 6;
    //Json로딩 관련 구조체 및 함수
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
    //현재 난이도
    public int curDiff = 0;

    private void Awake()
    {
        difficulty = this;
        LoadDiffTableFromJson();
    }
}
