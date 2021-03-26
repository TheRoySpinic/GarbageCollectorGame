using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Map.Generate
{
    public class Cluster: MonoBehaviour
    {
        [SerializeField]
        private EClusterType clusterType = EClusterType.NONE;
        
        [SerializeField]
        private State[] states = null;

        private int lastStateIndex = -1;

        public bool SetActiveIndex(int nextState)
        {
            if (nextState >= 0 && nextState < states.Length)
            {
                //Activate states
                lastStateIndex = nextState;
                return true;
            }
            else
            {
                return false;
            }
        }


        [System.Serializable]
        private class State
        {
            [SerializeField]
            public GameObject root = null;

            [SerializeField]
            public UnityEvent constructEvents = new UnityEvent();
            [SerializeField]
            public UnityEvent destructEvents = new UnityEvent();
        }
    }
}