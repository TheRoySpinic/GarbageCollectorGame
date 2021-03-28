using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Map.Generate
{
    public class Cluster: MonoBehaviour
    {
        [SerializeField]
        public EClusterType clusterType = EClusterType.NONE;
        
        [SerializeField]
        private State[] states = null;

        [SerializeField]
        private int currentStateIndex = 0;

        public bool SetActiveIndex(int nextState, int parameter = -1)
        {
            HideLast();

            if (nextState >= 0 && nextState < states.Length && states[nextState] != null && states[nextState].root != null)
            {
                states[nextState].root.SetActive(true);
                states[nextState].constructEvents?.Invoke();

                currentStateIndex = nextState;

                if(parameter >= 0)
                {
                    SetupParameter(parameter);
                }

                return true;
            }
            else
            {
                return false;
            }
        }


        private void SetupParameter(int parameter)
        {

        }

        private void HideLast()
        {
            if (currentStateIndex < 0 || currentStateIndex > states.Length - 1)
                return;

            states[currentStateIndex].destructEvents?.Invoke();
            states[currentStateIndex].root.SetActive(false);
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