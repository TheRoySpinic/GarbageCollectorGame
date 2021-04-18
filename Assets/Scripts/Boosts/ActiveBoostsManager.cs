using Balance;
using Base;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Boosts
{
    public class ActiveBoostsManager : SingletonGen<ActiveBoostsManager>
    {
        public static Action<EBoostType> E_BoostDeactivation;
        public static Action<EBoostType> E_AddBoost;
        public static Action E_BoostListUpdate;

        [SerializeField]
        private BoostIcon[] icons = null;

        [SerializeField]
        private List<Boost> activeBoosts = new List<Boost>();

        private BoostBalance balance = null;

        private List<Boost> toRemove = new List<Boost>();

        private void Update()
        {
            if(activeBoosts.Count > 0)
            {
                foreach(Boost boost in activeBoosts)
                {
                    boost.timeLeft -= Time.deltaTime;

                    if(boost.timeLeft < 0)
                    {
                        boost.Deactivate();
                        E_BoostDeactivation?.Invoke(boost.boostType);
                        toRemove.Add(boost);
                    }
                }

                if(toRemove.Count > 0)
                {
                    foreach(Boost boost in toRemove)
                    {
                        activeBoosts.Remove(boost);
                    }

                    toRemove.Clear();
                }
            }
        }

        public void AddBoost(EBoostType boostType)
        {
            if (balance == null)
                balance = GameBalance.GetBoostBalance();

            Boost contains = Array.Find(activeBoosts.ToArray(), (b) => { return b.boostType.Equals(boostType); });
            Boost boostData = Array.Find(balance.boosts.ToArray(), (b) => { return b.boostType.Equals(boostType); });

            if (contains != null)
            {
                contains.timeLeft = boostData.timeLeft;
            }
            else
            {
                Boost boost = new Boost(boostData);

                if (boost == null)
                    boost = new Boost() { boostType = boostType };

                boost.Activate();
                activeBoosts.Add(boost);

                E_AddBoost?.Invoke(boostType);
            }
            E_BoostListUpdate?.Invoke();
        }

        public List<Boost> GetActiveBoost()
        {
            return activeBoosts;
        }

        public Sprite GetBoostSprite(EBoostType boostType)
        {
            return Array.Find(icons, (i) => { return i.boostType.Equals(boostType); }).sprite;
        }

        public float GetBaseRemainingTime(EBoostType boostType)
        {
            return Array.Find(balance.boosts.ToArray(), (b) => { return b.boostType.Equals(boostType); }).timeLeft;
        }

        public void DisableAllBoost()
        {
            foreach(Boost boost in activeBoosts)
            {
                boost.timeLeft = 0;
            }
        }

    
        [Serializable]
        private class BoostIcon
        {
            public EBoostType boostType = EBoostType.NONE;
            public Sprite sprite = null;
        }
    }
}