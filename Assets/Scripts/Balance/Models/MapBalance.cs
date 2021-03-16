using System.Collections;
using System.Collections.Generic;
using Target;
using UnityEngine;

namespace Balance
{
    [System.Serializable]
    public class MapBalance
    {
        public GarbageConfig[] garbageSpawnsConfig = null;

        
        [System.Serializable]
        public class GarbageConfig
        {
            public GarbageType garbageType = GarbageType.NONE;
            public float spawnChange = 0;
            [Range(1,25)]
            public int baseReward = 1;
        }
    }
}