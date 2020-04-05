using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialoguePopUp : MonoBehaviour
{
    private void Awake()
    {
        gameObject.SetActive(false);
    }
    
    public void setTextField(string str)
    {
        gameObject.GetComponentInChildren<TextMeshProUGUI>().SetText(str);
    }
    public void showDialoguePopUp()
    {
        gameObject.SetActive(true);
    }
    public void hideDialoguePopUp()
    {
        gameObject.SetActive(false);
    }
}
