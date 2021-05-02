using Balance;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map.Generate
{
    public class MapSegment : MonoBehaviour
    {
        public float size { get; private set; } = 0;

        [SerializeField]
        private int currentIndex = -1;

        [SerializeField]
        private LineParameters[] lines = null;

        private List<ClusterRows> rows = new List<ClusterRows>();

        private Transform tr = null;

        private MapBalance mapBalance = null;

        private float clusterSize = 10;

        private void Awake()
        {
            tr = transform;
            mapBalance = GameBalance.GetMapBalance();
        }

        public void NextSegment(int segmentIndex)
        {
            SpawnClusters(segmentIndex);
            PrepareClusters(segmentIndex);

            currentIndex = segmentIndex;
        }

        public void NextSegment(MapBalance.SegmentConfig segmentConfig)
        {
            SpawnClusters(segmentConfig);
            PrepareClusters(segmentConfig);
        }

        public void SetSegmentPosition(Vector3 newPosition)
        {
            tr.localPosition = newPosition;
        }


        private void SpawnClusters(int segmentIndex)
        {
            SpawnClusters(MapGenerator.GetCurrentBiomeConfig().segments[segmentIndex]);
        }

        private void PrepareClusters(int segmentIndex)
        {
            PrepareClusters(MapGenerator.GetCurrentBiomeConfig().segments[segmentIndex]);
        }

        private void SpawnClusters(MapBalance.SegmentConfig segmentConfig)
        {
            for (int r = rows.Count > 0 ? rows.Count - 1 : 0;
                r < segmentConfig.spawnConfig.Length;
                r++)
            {
                LineParameters parameter = Array.Find(lines, (l) => { return r == l.line; });

                if(rows.Count <= r)
                {
                    rows.Add(new ClusterRows());
                }

                for (int i = rows[r].clusters.Count > 0 ? rows[r].clusters.Count : 0;
                    i < segmentConfig.spawnConfig[r].indexses.Length;
                    i++)
                {
                    Cluster cluster = Instantiate(parameter.clusterPrefab, transform).GetComponent<Cluster>();
                    cluster.transform.localPosition = new Vector3(i * 10, 0, parameter.position);
                    if (cluster.transform.localPosition.z > 0.1)
                    {
                        cluster.transform.localScale = new Vector3(cluster.transform.localScale.x, cluster.transform.localScale.y, -cluster.transform.localScale.z);
                    }
                    cluster.clusterType = parameter.clusterType;
                    cluster.lineIndex = r;

                    rows[r].clusters.Add(cluster);
                }
            }
        }

        private void PrepareClusters(MapBalance.SegmentConfig segmentConfig)
        {
            for(int r = 0; r < segmentConfig.spawnConfig.Length; ++r)
            {
                for (int i = 0; i < segmentConfig.spawnConfig[r].indexses.Length; ++i)
                {
                    rows[r].clusters[i].SetActiveIndex(segmentConfig.spawnConfig[r].indexses[i], segmentConfig.parameters[r].indexses[i]);
                }
            }

            if (segmentConfig.spawnConfig.Length > 0)
                size = segmentConfig.spawnConfig[0].indexses.Length * clusterSize;
            else
                size = 0;
        }

        private void HideAll()
        {
            foreach (ClusterRows r in rows)
            {
                foreach (Cluster cluster in r.clusters)
                {
                    cluster.SetActiveIndex(-1);
                }
            }
        }

        [System.Serializable]
        private class LineParameters
        {
            public int line = -1;
            public EClusterType clusterType = EClusterType.NONE;
            public float position = 0;
            public GameObject clusterPrefab = null;
        }
    }
}