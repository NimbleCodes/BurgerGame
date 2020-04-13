using System;
using System.Collections;
using System.IO;
using UnityEngine;

class TutorialManager : MonoBehaviour
{
    [Serializable]
    struct DialogueTable
    {
        public DialogueGroup[] dialogueGroup;
    }
    [Serializable]
    public struct DialogueGroup
    {
        public string[] dialogue;
    }
    DialogueTable dialogueTable;
    void LoadDialogueTableFromJson()
    {
        string DialogueTableStr = File.ReadAllText(Application.dataPath + "/Resources/Json/TutorialDialogue.json");
        dialogueTable = JsonUtility.FromJson<DialogueTable>(DialogueTableStr);
    }

    public GameObject TutorialPopUp;
    bool cont = false;
    bool fin = false;
    int diaIndex = 0;
    int printing = 0;
    IEnumerator PrintDialogue(int bpNum)
    {
        TutorialPopUp.GetComponent<DialoguePopUp>().showDialoguePopUp();
        while (dialogueTable.dialogueGroup[bpNum].dialogue.Length > diaIndex)
        {
            if (!fin)
            {
                if (cont)
                {
                    TutorialPopUp.GetComponent<DialoguePopUp>().setTextField(dialogueTable.dialogueGroup[bpNum].dialogue[diaIndex]);
                    cont = false;
                    fin = true;
                }
                else
                {
                    TutorialPopUp.GetComponent<DialoguePopUp>().setTextField(dialogueTable.dialogueGroup[bpNum].dialogue[diaIndex].Substring(0, printing++));
                    if (printing >= dialogueTable.dialogueGroup[bpNum].dialogue[diaIndex].Length+1)
                        fin = true;
                }
            }
            else
            {
                if (cont)
                {
                    diaIndex++;
                    printing = 0;
                    cont = false;
                    fin = false;
                }
            }
            yield return null;
        }
        cont = false;
        fin = false;
        printing = 0;
        diaIndex = 0;
        TutorialPopUp.GetComponent<DialoguePopUp>().hideDialoguePopUp();
        EventManager.eventManager.Invoke_GameResumeEvent("TutorialManager");
    }
    void OnBpReachedEvent(int bpnum)
    {
        EventManager.eventManager.Invoke_GamePausedEvent("TutorialManager");
        StartCoroutine("PrintDialogue",bpnum);
    }

    bool click = false;
    private void Awake()
    {
        LoadDialogueTableFromJson();
    }
    private void Start()
    {
        EventManager.eventManager.BreakpointReachedEvent += OnBpReachedEvent;
    }
    private void Update()
    {
        if(!click & Input.GetMouseButtonDown(0)){
            cont = true;
            click = true;
        }
        if(click & Input.GetMouseButtonUp(0))
        {
            click = false;
        }
    }
}