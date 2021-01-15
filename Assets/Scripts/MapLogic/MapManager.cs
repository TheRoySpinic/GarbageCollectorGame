using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class MapManager : MonoBehaviour
    {
        public const int LINE_COUNT = 3;

        public int currentDifficulty = 0;

        public static MapManager instance = null;

        public float[] lineShifts = null;

        [Header("Segments"), Space]
        [SerializeField]
        private MapSegmentDifficulty[] segments = null;

        private Queue<GameObject> segmentQueue = new Queue<GameObject>();

        private void Awake()
        {
            if (instance == null)
                instance = this;
        }

        public GameObject GetNextSegment(out float size)
        {
            if(segmentQueue.Count == 0)
            {
                FillQueue();
            }

            size = GetSegmentSizeByDifficulty(currentDifficulty);
            return segmentQueue.Dequeue();
        }

        private void FillQueue()
        {
            MapSegmentDifficulty mapSegment = GetSegmentsByDifficulty(currentDifficulty);

            if(mapSegment.segmentPrefabs.Length < 3)
            {
                Debug.LogError("[MapManager] Fill queue. SegmentPrefab count is loss that 3!!! Please add more segment prefab. Current difficulty: " + currentDifficulty);
            }

            int segmentSize = UnityEngine.Random.Range(mapSegment.minSectorSize, mapSegment.maxSectorSize);

            int[] indexes = new int[segmentSize];

            for (int i = 0; i < indexes.Length; i++)
            {
                if(i < 2)
                {
                    indexes[i] = UnityEngine.Random.Range(0, mapSegment.segmentPrefabs.Length);
                    continue;
                }

                int step = 0;
                while(step < 100)
                {
                    indexes[i] = UnityEngine.Random.Range(0, mapSegment.segmentPrefabs.Length);
                    if(indexes[i] != indexes[i-1] && indexes[i] != indexes[i-2])
                    {
                        break;
                    }
                    step++;
                }

                segmentQueue.Enqueue(mapSegment.segmentPrefabs[indexes[i]]);
            }

            if (segments.Length - 1 > currentDifficulty)
                ++currentDifficulty;
        }

        private float GetSegmentSizeByDifficulty(int difficulty)
        {
            return GetSegmentsByDifficulty(difficulty).segmentSize;
        }

        private MapSegmentDifficulty GetSegmentsByDifficulty(int difficulty)
        {
            return Array.Find<MapSegmentDifficulty>(segments, (segment) => { return segment.segmentDifficulty == difficulty; });
        }

        [System.Serializable]
        private class MapSegmentDifficulty
        {
            public string name = "";
            public int segmentDifficulty = 0;
            public int minSectorSize = 40;
            public int maxSectorSize = 50;
            public float segmentSize = 10;
            [Header("Prefabs")]
            public GameObject[] segmentPrefabs = null;
        }
    }
}