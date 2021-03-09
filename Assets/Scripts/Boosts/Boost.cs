using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Boosts
{
    [Serializable]
    public class Boost
    {
        public EBoostType boostType = EBoostType.NONE;
        public float timeLeft = 10;

        public Boost() { }

        public Boost(Boost other)
        {
            boostType = other.boostType;
            timeLeft = other.timeLeft;
        }

        public void Activate()
        {

        }

        public void Deactivate()
        {

        }
    }
}