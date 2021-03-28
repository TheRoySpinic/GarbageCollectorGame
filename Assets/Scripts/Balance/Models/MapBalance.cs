using System;
using System.Collections;
using System.Collections.Generic;
using Target;
using UnityEngine;

namespace Balance
{
    [Serializable]
    public class MapBalance
    {
        public GarbageConfig[] garbageSpawnsConfig = null;

        public SegmentConfig[] segments = null;
        
        [Serializable]
        public class GarbageConfig
        {
            public GarbageType garbageType = GarbageType.NONE;
            public float spawnChange = 0;
            [Range(1,25)]
            public int baseReward = 1;
        }

        [Serializable]
        public class SegmentConfig
        {
            public ClusterRows[] spawnConfig = null;
            public ClusterRows[] parameters = null;
        }

        [Serializable]
        public class ClusterRows
        {
            public int[] indexses = null;
        }
    }
}