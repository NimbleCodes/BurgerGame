using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class NickNamePanel : MonoBehaviour
{
    public static NickNamePanel Instance;
    public bool EnableBackground = true;
    public string DefaultOK = "OK";
    [SerializeField]
    private GameObject background;
    [SerializeField]
    private GameObject PopupNickName;
    [SerializeField]
    private Button Ok;
    [SerializeField]
    private Text okText;
    
    private int score;
    private Action N_Action;
    public string username;
    public GameObject inputField;
    
    private void Awake()
    {
        if(Instance == null)
        Instance = this;
        else if(Instance != null)
        Destroy(gameObject);
    }
    void Start()
    {
        CloseNickPanel();
    }

    public void StoreName()
    {
        username = inputField.GetComponent<Text>().text;
    }

    private void CloseNickPanel()
    {
        N_Action = null;
        Ok.gameObject.SetActive(false);

        if(EnableBackground)
        background.SetActive(false);

        PopupNickName.SetActive(false);
        okText.text = DefaultOK;
    }
    
    private void OpenNickNamePanel()
    {
        if(EnableBackground)
        background.SetActive(true);

        PopupNickName.SetActive(true);
        score = DisplayScore.Instance.getScore();
    }

    public void ShowNickPanel(Action N_Action = null)
    {
        this.N_Action = N_Action;

        Ok.gameObject.SetActive(true);
        Ok.Select();

        Navigation navigation = new Navigation();

        navigation.selectOnLeft = null;
        navigation.selectOnRight = null;
        Ok.navigation = navigation;

        OpenNickNamePanel();
    }

    public void OnClickOk()
    {
        if(N_Action != null)
        N_Action();

        StoreName();
        Data_manager.AddNewHighscore(username,score);
        CloseNickPanel();
    }
}
