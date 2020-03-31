using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class DiffInc : MonoBehaviour
{
    public static DiffInc diffInc;
    int maxNumDiff = 3;

    private void Awake()
    {
        diffInc = this;

        DiffTable = new diffTable();
        DiffTable.StageDiffVals = new diffRow[maxNumDiff];
        LoadDiffTableFromJson();

        Debug.Log(DiffTable.StageDiffVals[0].NumActiveSpawner);
    }

    [System.Serializable]
    public struct diffRow
    {
        public int NumActiveSpawner;
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
