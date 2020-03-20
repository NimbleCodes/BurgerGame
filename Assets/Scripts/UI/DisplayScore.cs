using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayScore : MonoBehaviour
{
    void Start()
    {
        EventManager.eventManager.ScoreChangedEvent += changeDispScoreVal;
        gameObject.GetComponent<TextMeshProUGUI>().text = "Score: 0";
    }

    void changeDispScoreVal(int val)
    {
        gameObject.GetComponent<TextMeshProUGUI>().text = "Score: " + val.ToString();
    }
}
