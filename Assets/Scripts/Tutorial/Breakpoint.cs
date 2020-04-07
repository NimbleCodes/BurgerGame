abstract class Breakpoint
{
    public bool active;
    int bpNum;
    float delay;

    public Breakpoint(int _bpNum, float _delay)
    {
        active = false;
        bpNum = _bpNum;
        delay = _delay;
    }
    public Breakpoint(int _bpNum)
    {
        active = false;
        bpNum = _bpNum;
        delay = 0;
    }
    protected abstract bool BpReached();
    public bool BpQuery()
    {
        if (active)
        {
            active = false;
            return BpReached();
        }
        return false;
    }
    public float getDelay()
    {
        return delay;
    }
    public int getBpNum()
    {
        return bpNum; 
    }
}

//테스트용
class Breakpoint_WaitOneSecond : Breakpoint
{
    public Breakpoint_WaitOneSecond(int _bpNum) : base(_bpNum, 1f)
    {

    }
    protected override bool BpReached()
    {
        return true;
    }
}