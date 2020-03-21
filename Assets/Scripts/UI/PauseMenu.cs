using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    bool hidden;

    void Start()
    {
        Hide();
        EventManager.eventManager.GamePausedEvent += OnGamePausedEvent;
    }

    void OnGamePausedEvent()
    {
        if (hidden) Show();
        else Hide();
    }

    void Show()
    {
        hidden = false;
        gameObject.SetActive(true);
    }
    void Hide()
    {
        hidden = true;
        gameObject.SetActive(false);
    }
}
