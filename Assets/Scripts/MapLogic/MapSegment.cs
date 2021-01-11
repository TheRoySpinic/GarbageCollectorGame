using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class MapSegment : MonoBehaviour
    {
        public bool[] haveBaricade = new bool[MapManager.LINE_COUNT];

        [SerializeField]
        private GameObject[] baricade;

        private void Awake()
        {
            UpdateBaricade();
        }

        public void UpdateBaricade()
        {
            for (int i = 0; i < haveBaricade.Length; i++)
            {
                if(baricade.Length > i)
                    baricade[i].SetActive(haveBaricade[i]);
            }
        }
    }
}