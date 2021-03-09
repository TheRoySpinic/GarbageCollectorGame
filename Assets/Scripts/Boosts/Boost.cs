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

        public void Activate()
        {

        }

        public void Deactivate()
        {

        }
    }
}