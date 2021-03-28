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
        private LineParameters[] lines = null;

        [SerializeField]
        private List<ClusterRows> rows = null;

        private Transform tr = null;

        private MapBalance mapBalance = null;

        private float clusterSize = 10;

        private void Awake()
        {
            tr = transform;
            mapBalance = GameBalance.GetMapBalance();
        }


        private void SpawnClusters(int segmentIndex)
        {
            for (int r = rows.Count > 0 ? rows.Count - 1 : 0;
                r < mapBalance.segments[segmentIndex].spawnConfig.Length;
                r++)
            {
                LineParameters parameter = Array.Find(lines, (l) => { return r == l.line; });

                for (int i = rows[r].clusters.Count > 0 ? rows[r].clusters.Count : 0; 
                    i < mapBalance.segments[segmentIndex].spawnConfig[r].indexses.Length; 
                    i++)
                {
                    Cluster cluster = Instantiate(parameter.clusterPrefab, transform).GetComponent<Cluster>();
                    cluster.transform.localPosition = new Vector3(i * 10, 0, parameter.position);
                    cluster.clusterType = parameter.clusterType;

                    if(rows.Count < r)
                    {
                        rows.Add(new ClusterRows());
                    }

                    rows[r].clusters.Add(cluster);
                }
            }
        }

        private void PrepareClusters(int segmentIndex)
        {
            MapBalance.SegmentConfig segmentConfig = mapBalance.segments[segmentIndex];

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