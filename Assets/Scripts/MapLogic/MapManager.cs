using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class MapManager : MonoBehaviour
    {
        public const int LINE_COUNT = 3;

        public static MapManager instance;

        public float[] lineShifts;

        private void Awake()
        {
            if (instance == null)
                instance = this;
        }
    }
}