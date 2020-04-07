using UnityEngine;

class TutorialManager : MonoBehaviour
{

    private void Awake()
    {
        
    }
    private void Start()
    {
        EventManager.eventManager.BreakpointReachedEvent += OnBpReachedEvent;
    }
    void OnBpReachedEvent(int bpnum)
    {
        Debug.Log(bpnum + " breakpoint reached!");
    }
}