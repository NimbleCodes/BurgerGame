using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    bool gamePaused = false;
    bool click = false;
    public bool incDiff = false;    //임시 코드

    private void Update()
    {
        if (!click & Input.GetKeyDown(KeyCode.Escape))
        {
            click = true;
            if (gamePaused) resumeGame();
            else pauseGame();
        }
        if (click & Input.GetKeyUp(KeyCode.Escape))
        {
            click = false;
        }
        //임시 코드
        if (incDiff)
        {
            EventManager.eventManager.Invoke_DiffIncEvent();
            incDiff = false;
        }
    }

    void pauseGame()
    {
        gamePaused = true;
        Time.timeScale = 0;
        EventManager.eventManager.Invoke_GamePausedEvent();
    }
    void resumeGame()
    {
        gamePaused = false;
        Time.timeScale = 1;
        EventManager.eventManager.Invoke_GamePausedEvent();
    }
}
