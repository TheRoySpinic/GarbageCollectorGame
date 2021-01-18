using Balance;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Target
{
    public class TargetManager : MonoBehaviour
    {
        public static TargetManager instance = null;

        [SerializeField]
        private GarbageData[] garbageData = { new GarbageData(), new GarbageData() {garbageType = GarbageType.COMMON}, new GarbageData() { garbageType = GarbageType.RARE} };
        [SerializeField]
        private float grabPercent = 0;


        private void Awake()
        {
            if (instance == null)
                instance = this;
        }

        private void OnDestroy()
        {
            if (instance.Equals(this))
                instance = null;
        }

        public void AddGarbage(GarbageType type)
        {
            foreach(GarbageData data in garbageData)
            {
                if(data.garbageType == type)
                {
                    ++data.count;
                    break;
                }
            }
        }

        public void CalculateReward()
        {
            float rewardSum = 0;

            foreach (GarbageData data in garbageData)
            {
                if (data.garbageType != GarbageType.NONE)
                {

                    int reward = Array.Find(GameBalance.GetMapBalance().garbageSpawnsConfig, (a) => { return a.garbageType == data.garbageType; }).baseReward;
                    rewardSum += data.count * reward;
                }
            }


        }


        [System.Serializable]
        private class GarbageData
        {
            public GarbageType garbageType = GarbageType.NONE;
            public int count = 0;
        }
    }
}