using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool incDiff = false;    //임시 코드
    int curStage = 0;

    private void Start() {
        EventManager.eventManager.GamePausedEvent += OnGamePausedEvent;
    }
    private void Update()
    {
        //임시 코드
        if (incDiff)
        {
            curStage++;
            EventManager.eventManager.Invoke_DiffIncEvent(curStage);
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
}
