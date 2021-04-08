using Map;
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
        public EBiomeType startBiome = EBiomeType.DEFAULT;
        public EBiomeType[] avableToRandom = { EBiomeType.DEFAULT, EBiomeType.FOREST };
        public int segmentCounts = 20;

        public BiomeConfig[] biomes = null;
        public GarbageConfig[] garbageSpawnsConfig = null;

        

        [Serializable]
        public class GarbageConfig
        {
            public GarbageType garbageType = GarbageType.NONE;
            public float spawnChange = 0;
            [Range(1,25)]
            public int baseReward = 1;
        }

        [Serializable]
        public class BiomeConfig
        {
            public EBiomeType biomeType = EBiomeType.NONE;
            public float setNextSegmentDistance = 100;
            public int segmentSaveDublicateIndexes = 2;
            public int biomeSaveDublicateIndexes = 2;
            public int minBiomeSegments = 10;
            public int maxBiomeSegments = 20;
            public float biomeSpawnChange = 0.5f;
            public EBiomeType nextBiome = EBiomeType.NONE; //NONE = random
            public SegmentConfig[] segments = null;
            public TransitionConfig[] transitions = null;
        }

        [Serializable]
        public class TransitionConfig
        {
            public EBiomeType previousBiome = EBiomeType.NONE;
            public bool useTransition = false;
            public int copyFromIndex = -1;
            public SegmentConfig segment = null;
        }

        [Serializable]
        public class SegmentConfig
        {
            public ClusterRow[] spawnConfig = null;
            public ClusterRow[] parameters = null;
        }

        [Serializable]
        public class ClusterRow
        {
            public int[] indexses = null;
        }
    }
}