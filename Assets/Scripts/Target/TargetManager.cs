using Balance;
using Store;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Target
{
    public class TargetManager : MonoBehaviour
    {
        public static TargetManager instance = null;

        public static Action<GarbageType> E_CollectGarbage;

        [SerializeField]
        private GarbageData[] garbageData = { new GarbageData(), new GarbageData() {garbageType = GarbageType.COMMON}, new GarbageData() { garbageType = GarbageType.RARE} };
        [SerializeField]
        private float grabPercent = 0;

        private int garbageCount = 0;
        private int allCount = 0;
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

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.tag.Equals("Garbage"))
            {
                Garbage garbage = other.gameObject.GetComponent<Garbage>();
                if (!garbage.isActive)
                    return;
                AddGarbage(garbage.garbageType);
                AddReward(garbage.garbageType);
                garbage.isActive = false;
                ++garbageCount;
                AddAllCount();
                garbage.HideEffect();
                E_CollectGarbage?.Invoke(garbage.garbageType);
            }
        }

        public void StartNewRun()
        {

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

        public void AddAllCount()
        {
            ++allCount;
            grabPercent = (float) garbageCount / (float) allCount;
        }

        public void AddReward(GarbageType garbageType)
        {
            int reward = 0;
            reward = Array.Find(GameBalance.GetMapBalance().garbageSpawnsConfig, (a) => { return a.garbageType == garbageType; }).baseReward;

            MasterStoreManager.instance.AddGold(reward);
        }


        [System.Serializable]
        private class GarbageData
        {
            public GarbageType garbageType = GarbageType.NONE;
            public int count = 0;
        }
    }
}