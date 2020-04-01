using UnityEngine;

public abstract class BreakPoint : MonoBehaviour
{
    public int BPNum = -1;

    protected abstract bool BPCondition();
    protected void SetBPNum(int val)
    {
        BPNum = val;
    }

    private void Awake()
    {
        if(BPNum == -1)
        {
            Debug.Log("BPNum invalid");
            gameObject.GetComponent<BreakPoint>().enabled = false;
        }
    }
    private void Update()
    {
        if (BPCondition()) EventManager.eventManager.Invoke_BpReachedEvent(BPNum);
    }
}
