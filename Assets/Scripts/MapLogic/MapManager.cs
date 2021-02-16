using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class MapManager : MonoBehaviour
    {
        public const int LINE_COUNT = 3;

        [SerializeField]
        public EBiomeType currentBiome = EBiomeType.DEFAULT;
        private EBiomeType nextBiome = EBiomeType.DEFAULT;
        [SerializeField]
        public int currentDifficulty = 0;

        public static MapManager instance = null;

        public float[] lineShifts = null;

        [Header("Segments"), Space]
        [SerializeField]
        private MapBiome[] biomes = null;

        //[SerializeField]
        //private int segmentToNextDifficulty = 2;

        private Queue<GameObject> segmentQueue = new Queue<GameObject>();


        //private int segmentCoplete = 0;

        private void Awake()
        {
            if (instance == null)
                instance = this;


            nextBiome = currentBiome;
        }

        public void ClearData()
        {

        }

        public GameObject GetNextSegment(out float size)
        {
            if(segmentQueue.Count == 0)
            {
                FillQueue();
            }

            size = segmentQueue.Peek().GetComponent<MapSegment>().prefabSize;
            return segmentQueue.Dequeue();
        }

        private void FillQueue()
        {
            currentBiome = nextBiome;

            MapBiomeSegment mapSegment = null;

            mapSegment = GetRandomeBiomeSegment(currentBiome);

            if (mapSegment.segmentPrefabs.Length < 3)
            {
                Debug.LogError("[MapManager] Fill queue. SegmentPrefab count is loss that 3!!! Please add more segment prefab. Current biome: " + 
                    currentBiome.ToString() + ", difficulty: " + currentDifficulty);
            }

            int segmentSize = UnityEngine.Random.Range(mapSegment.minBiomeSize, mapSegment.maxBiomeSize);

            int[] indexes = new int[segmentSize];

            if(mapSegment.before.Length > 0)
            {
                foreach(GameObject o in mapSegment.before)
                {
                    segmentQueue.Enqueue(o);
                }
            }
            if (mapSegment.scenarioMode)
            {
                foreach(GameObject o in mapSegment.segmentPrefabs)
                {
                    segmentQueue.Enqueue(o);
                }
            }
            else
            {
                for (int i = 0; i < indexes.Length; i++)
                {
                    if (i < 2)
                    {
                        indexes[i] = UnityEngine.Random.Range(0, mapSegment.segmentPrefabs.Length);
                        continue;
                    }

                    int step = 0;
                    while (step < 100)
                    {
                        indexes[i] = UnityEngine.Random.Range(0, mapSegment.segmentPrefabs.Length);
                        if (indexes[i] != indexes[i - 1] && indexes[i] != indexes[i - 2])
                        {
                            break;
                        }
                        step++;
                    }

                    segmentQueue.Enqueue(mapSegment.segmentPrefabs[indexes[i]]);
                }
            }

            if (mapSegment.after.Length > 0)
            {
                foreach (GameObject o in mapSegment.after)
                {
                    segmentQueue.Enqueue(o);
                }
            }

            if (biomes.Length > 1)
            {
                int step = 0;
                while (currentBiome == nextBiome && step < 100)
                {
                    nextBiome = biomes[UnityEngine.Random.Range(0, biomes.Length)].biomeType;
                    ++step;
                }
            }
        }

        private float[] GetBiomeLineShifts(EBiomeType biomeType)
        {
            return Array.Find(biomes, (b) => { return b.biomeType.Equals(biomeType); }).lineShifts;
        }

        private MapBiome GetBiomeSegments(EBiomeType biomeType)
        {
            return Array.Find(biomes, (b) => { return b.biomeType.Equals(biomeType); });
        }

        private MapBiomeSegment GetBiomeSegmentByDifficulty(EBiomeType biomeType, int difficulty)
        {
            return Array.Find(GetBiomeSegments(biomeType).segmentPrefabs, (s) => { return s.segmentDifficulty == difficulty; });
        }

        private MapBiomeSegment GetRandomeBiomeSegment(EBiomeType biomeType)
        {
            MapBiome biome = GetBiomeSegments(biomeType);

            return biome.segmentPrefabs[UnityEngine.Random.Range(0, biome.segmentPrefabs.Length)];
        }


        [System.Serializable]
        private class MapBiome
        {
            public EBiomeType biomeType = EBiomeType.DEFAULT;
            [Header("Segments")]
            public MapBiomeSegment[] segmentPrefabs = null;

            public float[] lineShifts = null;
        }

        [System.Serializable]
        private class MapBiomeSegment
        {
            public string name = "";
            public bool scenarioMode = false;
            public int segmentDifficulty = 0;
            public int minBiomeSize = 40;
            public int maxBiomeSize = 50;

            [Header("Transfers")]
            public GameObject[] before = null;
            public GameObject[] after = null;
            [Header("Prefabs")]
            public GameObject[] segmentPrefabs = null;
        }
    }
}