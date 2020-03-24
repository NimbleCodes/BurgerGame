using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool incDiff = false;    //임시 코드

    private void Start() {
        EventManager.eventManager.GamePausedEvent += OnGamePausedEvent;
    }

    private void Update()
    {
        //임시 코드
        if (incDiff)
        {
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
    void OnGamePausedEvent(string str){
        if(str.Equals("Pause")){
            pauseGame();
        }
        else{
            resumeGame();
        }
    }
}
