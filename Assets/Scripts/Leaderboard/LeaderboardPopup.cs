using System;
using System.Collections;
using UnityEngine;

public class LeaderboardPopup : MonoBehaviour
{
   public void OnClickButton()
    {

        Action L_Action = () => Debug.Log("On Click back Button");

        LeaderboardControll.Instance.ShowLeaderboard(L_Action);
    }
}
