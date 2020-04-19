using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string SceneToLoad;//호출할 Scene의 이름

    public void LoadGame()
    {
    	SceneManager.LoadScene(SceneToLoad);//Scene 호출
    }
}
