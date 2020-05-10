using System;
using System.Collections.Generic;

public abstract class Roulette
{
    int LOD;
    bool ready = false;
    public List<int> probs;

    public virtual void createRoulette(int numEntries)
    {
        LOD = 100;
        if (probs == null)
            probs = new List<int>();
        else
            probs.Clear();

        while (LOD < numEntries)
            LOD *= 5;
        int quotient = LOD / numEntries;
        int remainer = LOD % numEntries;
        for(int i = 0; i < numEntries; i++)
        {
            int temp = quotient + (i >= (numEntries - remainer) ? 1 : 0);
            probs.Add(temp);
        }
        ready = true;
    }
    public int Spin()
    {
        if (!ready)
            return -1;
        int rand = GameManager.gameManager.getRandNum(LOD-1) + 1;
        int temp = 0;
        for(int i = 0; i < probs.Count; i++)
        {
            temp += probs[i];
            if (temp > rand)
            {
                balancing(i);
                return i;
            }
        }
        //POE SHOULD NOT BE HERE
        return -1;
    }
    public override string ToString()
    {
        string ret = "";
        for(int i = 0; i < probs.Count; i++)
        {
            ret += (probs[i] + ", ");
        }
        return ret;
    }
    protected abstract void balancing(int ind);
}

public class BurgerIngrRoulette : Roulette
{
    protected override void balancing(int ind)
    {
        int numEntries = probs.Count;
        int distribute = probs[ind];
        int q = distribute / (numEntries-1);
        int r = distribute % (numEntries-1);
        for(int i = 0; i < numEntries; i++)
        {
            if (i == ind)
                continue;
            if (r > 0)
            {
                probs[i]++;
                r--;
            }
            probs[i] += q;
        }
        probs[ind] = 0;
    }
}