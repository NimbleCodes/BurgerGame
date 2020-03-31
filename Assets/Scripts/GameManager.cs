using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{
    public bool incDiff = false;    //임시 코드
    int maxNumDiff = 3;

    private void Start() {
        EventManager.eventManager.GamePausedEvent += OnGamePausedEvent;
        DiffTable = new diffTable();
        DiffTable.StageDiffVals = new diffRow[maxNumDiff];
        LoadDiffTableFromJson();
    }
    private void Update()
    {
        if (curStage < DiffTable.StageDiffVals.Length)
            Debug.Log(DiffTable.StageDiffVals[curStage].NumActiveSpawner);
        //임시 코드
        if (incDiff)
        {
            curStage++;
            EventManager.eventManager.Invoke_DiffIncEvent();
            incDiff = false;
        }
    }

    void pauseGame()
    {
        Time.timeScale = 0;
    }
    void resumeGame()
    {
        Time.timeScale = 1;
    }
    string prevWho = "";
    bool paused = false;
    void OnGamePausedEvent(bool action, string who){
        if (paused & !action & who.Equals(prevWho))
        {
            resumeGame();
            paused = false;
        }
        else if (!paused & action)
        {
            pauseGame();
            prevWho = who;
            paused = true;
        }
    }

    int curStage = 0;
    [System.Serializable]
    struct diffRow
    {
        public int NumActiveSpawner;
    }
    [System.Serializable]
    struct diffTable
    {
        public diffRow[] StageDiffVals;
    }
    diffTable DiffTable;
    void LoadDiffTableFromJson()
    {
        string DiffTableStr = File.ReadAllText(Application.dataPath + "/Resources/Json/DiffTable.json");
        DiffTable = JsonUtility.FromJson<diffTable>(DiffTableStr);
    }
}
