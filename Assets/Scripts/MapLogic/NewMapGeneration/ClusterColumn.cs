using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map.Generate
{
    [System.Serializable]
    public class ClusterRows
    {
        [SerializeField]
        public List<Cluster> clusters = new List<Cluster>();
    }
}