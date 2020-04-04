using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BreakPoints
{
    public abstract class BreakPoint
    {
        protected float delay = 0;
        int BPNum = -1;
        public BreakPoint(int bpn)
        {
            BPNum = bpn;
        }
        protected abstract bool BPCondition();
        public bool BPReached()
        {
            return BPCondition();
        }
        public int getBPNum()
        {
            return BPNum;
        }
        public float getBPdelay()
        {
            return delay;
        }
    }
    public class BreakPointFirstIngrEaten : BreakPoint{
        bool conditionMet = false;
        
        public BreakPointFirstIngrEaten(int bpn) : base(bpn)
        {
            EventManager.eventManager.IngrEatenEvent += OnIngrEaten;
        }
        protected override bool BPCondition()
        {
            return conditionMet;
        }
        void OnIngrEaten(string what)
        {
            conditionMet = true;
            EventManager.eventManager.IngrEatenEvent -= OnIngrEaten;
        }
    }
    public class BreakPointFirstIngrDestroyed : BreakPoint
    {
        bool conditionMet = false;

        public BreakPointFirstIngrDestroyed(int bpn) : base(bpn)
        {
            EventManager.eventManager.IngrDestroyedEvent += OnIngrDestroyed;
        }
        protected override bool BPCondition()
        {
            return conditionMet;
        }
        void OnIngrDestroyed()
        {
            conditionMet = true;
            EventManager.eventManager.IngrDestroyedEvent -= OnIngrDestroyed;
        }
    }
    public class BreakPointFirstIngrSpawned : BreakPoint
    {
        public BreakPointFirstIngrSpawned(int bpn) : base(bpn)
        {
            delay = 0.5f;
        }
        protected override bool BPCondition()
        {
            return ObjectManager.objectManager.firstSpawn;
        }
    }
}