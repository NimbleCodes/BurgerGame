using System;
using System.Threading;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;

    //랜덤 객체 한개 생성 -> 다른 객체에서 랜덤성이 필요하면 가져다 쓴다
    System.Random rand;
    Mutex randMut;
    //System.Random은 Thread safe하지 않으므로 뮤텍스로 보호
    public int getRandNum(int max)
    {
        randMut.WaitOne();
        int ret = rand.Next(max);
        randMut.ReleaseMutex();
        return ret;
    }

    void OnGamePaused()
    {
        Time.timeScale = 0;
    }
    void OnGameResumed()
    {
        Time.timeScale = 1;
    }

    private void Awake()
    {
        gameManager = this;
        rand = new System.Random(Guid.NewGuid().GetHashCode());
        randMut = new Mutex();
    }
    private void Start()
    {
        EventManager.eventManager.GamePausedEvent += OnGamePaused;
        EventManager.eventManager.GameResumeEvent += OnGameResumed;
    }
}