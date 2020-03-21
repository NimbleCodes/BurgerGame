using System;
using UnityEngine;

/// <summary>
/// 팝업 윈도우 샘플 스크립트
/// 샘플 스크립트를 참조하세요
/// </summary>
public class PopupWindowSample : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClickSetting()
    {
        string title = "Setting";
        string message = "Sound";
        Action okAction = () => Debug.Log("On Click Ok Button");

        PopupWindowController.Instance.ShowOk(title, message, okAction);
    }
}
