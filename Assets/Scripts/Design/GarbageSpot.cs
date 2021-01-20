using Balance;
using System;
using System.Collections;
using System.Collections.Generic;
using Target;
using UnityEngine;

namespace Design
{
    public class GarbageSpot : MonoBehaviour
    {
        [SerializeField]
        private Spot[] spots = null;

        private void Awake()
        {
            if(GameBalance.instance == null)
            {
                Debug.Log("Game balance instance is not set!");
                return;
            }

            foreach(Spot spot in spots)
            {
                if (UnityEngine.Random.Range(0, 1f) < Array.Find(GameBalance.GetMapBalance().garbageSpawnsConfig, 
                                                      (a) => { return a.garbageType == spot.garbageType; }).spawnChange)
                {
                    foreach (GameObject o in spot.garbage)
                    {
                        o.SetActive(true);
                    }
                }
                else
                {
                    foreach (GameObject o in spot.garbage)
                    {
                        o.SetActive(false);
                    }
                }
            }
        }

        [Serializable]
        private class Spot
        {
            public GarbageType garbageType = GarbageType.NONE;
            public GameObject[] garbage = null;
        }
    }
}