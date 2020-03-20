using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    void Start()
    {
        Hide();
        EventManager.eventManager.PauseButtonPressed += OnPauseButtonPressed;
    }

    void OnPauseButtonPressed(string str)
    {
        if (str.Equals("pause"))
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    void Show()
    {
        gameObject.SetActive(true);
    }

    void Hide()
    {
        gameObject.SetActive(false);
    }
}
