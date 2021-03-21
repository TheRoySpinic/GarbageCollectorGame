using Balance;
using Base;
using Player;
using Store;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Target
{
    public class TargetManager : SingletonGen<TargetManager>
    {
        public static Action<GarbageType> E_CollectGarbage;
        public static Action E_UpdateGarbageContains;
        public static Action E_CarIsFull;


        public static double currentDistance { get; private set; } = 0;

        public int maxGarbages { get; private set; } = 100;
        public int garbageCount { get; private set; } = 0;
        public bool canGrabGarbage { get; private set; } = true;


        [SerializeField]
        private GarbageData[] garbageData = { new GarbageData(), new GarbageData() {garbageType = GarbageType.COMMON}, new GarbageData() { garbageType = GarbageType.RARE} };

        public int allCount { get; private set; } = 0;

        public int sumArrivalReward { get; private set; } = 0;

        private Rigidbody rb = null;

        public override void Init()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if(HealthManager.isAlive)
            {
                currentDistance += rb.velocity.x / 2 * Time.deltaTime;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if(HealthManager.isAlive && canGrabGarbage && other.gameObject.tag.Equals("Garbage"))
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
                E_UpdateGarbageContains?.Invoke();
            }

            if(garbageCount >= maxGarbages)
            {
                canGrabGarbage = false;
                E_CarIsFull?.Invoke();
                StartClear();
            }
        }

        public void StartNewRun()
        {
            garbageCount = 0;
            allCount = 0;
            sumArrivalReward = 0;
            canGrabGarbage = true;
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
        }

        public void AddReward(GarbageType garbageType)
        {
            int reward = 0;
            reward = Array.Find(GameBalance.GetMapBalance().garbageSpawnsConfig, (a) => { return a.garbageType == garbageType; }).baseReward;

            sumArrivalReward += reward;

            MasterStoreManager.instance.AddGold(reward);
        }

        public void SetMaxSize(int max)
        {
            maxGarbages = max;
        }

        public void StartClear()
        {
            StartCoroutine(CClear());
        }


        private void FinishClear()
        {
            canGrabGarbage = true;
            garbageCount = 0;
            E_UpdateGarbageContains?.Invoke();
        }


        private IEnumerator CClear()
        {
            yield return new WaitForSeconds(3);

            FinishClear();
        }


        [System.Serializable]
        private class GarbageData
        {
            public GarbageType garbageType = GarbageType.NONE;
            public int count = 0;
        }
    }
}