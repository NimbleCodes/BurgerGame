using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LeaderboardControll : MonoBehaviour
{
    public static LeaderboardControll Instance;
    public string SceneName;
    public bool BackgroundEnable = true;
    public string DefaultBackName = "Back";
    [SerializeField]
    private GameObject Background;
    [SerializeField]
    private GameObject PopupLeaderboard;
    [SerializeField]
    private Button backButton;
    [SerializeField]
    private Text backText;

    private Action L_Action; //

    private void Awake()
    {
        if(Instance == null)
        Instance = this;
        else if(Instance != null)
            Destroy(gameObject);
    }
    void Start()
    {
        CloseLeaderboard();
    }

    private void CloseLeaderboard()
    {
        L_Action = null;
        backButton.gameObject.SetActive(false);

        if(BackgroundEnable)
            Background.SetActive(false);

        PopupLeaderboard.SetActive(false);
        backText.text = DefaultBackName;
    }

    private void OpenLeaderboard()
    {
        if(BackgroundEnable)
            Background.SetActive(true);
        
        PopupLeaderboard.SetActive(true);
    }

    public void ShowLeaderboard(Action L_Action = null)
    {
        this.L_Action = L_Action;

        backButton.gameObject.SetActive(true);
        backButton.Select();

        Navigation navigation = new Navigation();

        navigation.selectOnLeft = null;
        navigation.selectOnRight = null;
        backButton.navigation = navigation;

        OpenLeaderboard();
    }

    public void OnClickBackButton()
    {
        if(L_Action != null)
            L_Action();

        CloseLeaderboard();
        SceneManager.LoadScene(SceneName);//Scene 호출
    }

    public void OnClickBack2()
    {
        if(L_Action != null)
        L_Action();

        CloseLeaderboard();
    }


}
