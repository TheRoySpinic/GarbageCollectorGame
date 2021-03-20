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

        [SerializeField]
        private GarbageData[] garbageData = { new GarbageData(), new GarbageData() {garbageType = GarbageType.COMMON}, new GarbageData() { garbageType = GarbageType.RARE} };

        private int maxGarbages = 100;

        private int garbageCount = 0;
        private int allCount = 0;

        private int sumArrivalReward = 0;

        private bool canGrabGarbage = true;

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