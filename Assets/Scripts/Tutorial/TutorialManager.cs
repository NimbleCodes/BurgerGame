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
    bool cont = true;
    int diaIndex = 0;
    IEnumerator PrintDialogue(int bpNum)
    {
        while(dialogueTable.dialogueGroup[bpNum].dialogue.Length > diaIndex)
        {
            if (cont)
            {
                if (TutorialPopUp == null)
                    Debug.Log(dialogueTable.dialogueGroup[bpNum].dialogue[diaIndex++]);
                else
                {
                    TutorialPopUp.GetComponent<DialoguePopUp>().showDialoguePopUp();
                    TutorialPopUp.GetComponent<DialoguePopUp>().setTextField(dialogueTable.dialogueGroup[bpNum].dialogue[diaIndex++]);
                }
                cont = false;
            }
            yield return null;
        }
        cont = true;
        diaIndex = 0;
        if (TutorialPopUp != null)
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