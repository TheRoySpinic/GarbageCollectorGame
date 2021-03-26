using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map.Generate
{
    [System.Serializable]
    public class ClusterColumn : MonoBehaviour
    {
        [SerializeField]
        private Cluster[] clusters = null;
    }
}