using System.Threading;
using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool incDiff = false;    //임시 코드
    int curStage = 0;
    public static GameManager gm;

    public static System.Random rand;
    public static Mutex randMutex;
    public bool pause;
    private void Awake()
    {
        gm = this;
        randMutex = new Mutex();
        rand = new System.Random(Guid.NewGuid().GetHashCode());
    }
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
        if(pause)
        {
        	pauseGame();
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
