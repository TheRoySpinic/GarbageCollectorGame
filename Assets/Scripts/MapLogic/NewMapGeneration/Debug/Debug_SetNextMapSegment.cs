using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map.Generate.Debug
{
    public class Debug_SetNextMapSegment : MonoBehaviour
    {
        [SerializeField]
        private MapSegment segment = null;

        [SerializeField]
        private int index = -1;

        void Start()
        {
            segment.NextSegment(index);
        }
    }
}