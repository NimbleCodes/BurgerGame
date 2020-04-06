using System;
using System.Threading;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;

    System.Random rand;
    Mutex randMut;
    public int getRandNum(int max)
    {
        randMut.WaitOne();
        int ret = rand.Next(max);
        randMut.ReleaseMutex();
        return ret;
    }

    private void Awake()
    {
        gameManager = this;
        rand = new System.Random(Guid.NewGuid().GetHashCode());
        randMut = new Mutex();
    }
}
