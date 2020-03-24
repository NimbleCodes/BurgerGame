using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    bool paused = false;
    bool click = false;

    void Show(){
        GetComponent<Canvas>().enabled = true;
    }
    void Hide(){
        GetComponent<Canvas>().enabled = false;
    }

    private void Update() {
        if (!click & Input.GetKeyDown(KeyCode.Escape))
        {
            string output = paused ? "Resume" : "Pause";
            EventManager.eventManager.Invoke_GamePausedEvent(output);
            paused = !paused;
            
            if(paused) Show();
            else Hide();
            click = true;
        }
        if (click & Input.GetKeyUp(KeyCode.Escape))
        {
            click = false;
        }
    }
}
