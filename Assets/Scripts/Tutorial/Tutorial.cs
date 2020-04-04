using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class Tutorial : MonoBehaviour
{
    public bool playTutorial = false;

    /*----------json loading----------*/
    [System.Serializable]
    public class dialogue
    {
        public string[] Dialogue;
    }
    [System.Serializable]
    public class dialogueArr
    {
        public dialogue[] DialogueArr;
    }
    string dialogueJsonPath;
    dialogueArr arr;
    void loadDialogueFromJson(string filePath)
    {
        string input = File.ReadAllText(Application.dataPath + filePath);
        arr = JsonUtility.FromJson<dialogueArr>(input);
    }
    /*----------json loading----------*/

    /*----------print dialogue----------*/
    bool cont = true;
    int diaNum = 0;
    bool printingDia = false;
    IEnumerator printDialogueCoroutine(int ind)
    {
        while (arr.DialogueArr[ind].Dialogue.Length > diaNum)
        {
            if (cont)
            {
                Debug.Log(arr.DialogueArr[ind].Dialogue[diaNum]);
                diaNum++;
                cont = false;
            }
            yield return null;
        }
        diaNum = 0;
        printingDia = false;
        EventManager.eventManager.Invoke_GamePausedEvent(false,"Tutorial");
    }
    void printDialogue(int ind)
    {
        StartCoroutine("printDialogueCoroutine",ind);
    }
    /*----------print dialogue----------*/

    /*----------breakpoints----------*/
    void OnBpReached(int bpnum)
    {
        //pause game
        EventManager.eventManager.Invoke_GamePausedEvent(true,"Tutorial");
        printingDia = true;
        //print dialogue
        printDialogue(bpnum);
    }
    /*----------breakpoints----------*/

    private void Start()
    {
        dialogueJsonPath = "/Resources/Json/TutorialDialogue.json";
        loadDialogueFromJson(dialogueJsonPath);
        EventManager.eventManager.BpReachedEvent += OnBpReached;
    }
    bool click = false;
    private void Update()
    {
        if (printingDia)
        {
            if (!click & Input.GetKeyDown(KeyCode.Mouse0))
            {
                cont = true;
                click = true;
            }
            if (click & Input.GetKeyUp(KeyCode.Mouse0))
            {
                click = false;
            }
        }
    }
}
