using System;
using UnityEngine;

public class ShowPopup : MonoBehaviour
{
    public void OnClickSetting()
    {
        string title = "Setting";
        string message = "Sound";
        Action okAction = () => Debug.Log("On Click Ok Button");

        PopupWindowController.Instance.ShowOk(title, message, okAction);
    }
}
