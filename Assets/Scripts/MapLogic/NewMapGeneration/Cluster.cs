using Map.Generate.ParameterLogic;
using System;
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
        public int lineIndex = -1;

        [SerializeField]
        private State[] states = null;

        [SerializeField]
        private int currentStateIndex = 0;

        [SerializeField]
        private int parameterIndex = 0;

        [SerializeField]
        private BaseParameterLogic defaultParameterComponent = null;
        [SerializeField]
        private ParameterLogic[] parameterLogics = new ParameterLogic[0];

        public bool SetActiveIndex(int nextState, int parameter = 0)
        {
            HideLast();

            if (nextState >= 0 && nextState < states.Length && states[nextState] != null && states[nextState].root != null)
            {
                if(clusterType.Equals(EClusterType.ROAD) && lineIndex == MapGenerator.instance.centerLineIndex)
                {
                    states[0].root.SetActive(true);
                }

                states[nextState].root.SetActive(true);
                states[nextState].constructEvents?.Invoke();

                currentStateIndex = nextState;

                ResetParameter();

                if(parameter != 0)
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
            if (parameter == 0) 
                return;

            BaseParameterLogic parameterComponent = Array.Find(parameterLogics, (p) => { return p.avableIndex.Contains(currentStateIndex); })?.parameterComponent;

            if (parameterComponent == null)
                parameterComponent = defaultParameterComponent;

            if (parameterComponent != null)
            {
                parameterComponent.ParameterAction(parameter);
            }

            parameterIndex = parameter;
        }

        private void ResetParameter()
        {
            if (parameterIndex == 0)
                return;

            BaseParameterLogic parameterComponent = Array.Find(parameterLogics, (p) => { return p.avableIndex.Contains(currentStateIndex); })?.parameterComponent;

            if (parameterComponent == null)
                parameterComponent = defaultParameterComponent;

            if (parameterComponent != null)
            {
                parameterComponent.ResetParameter(parameterIndex);
            }

            parameterIndex = 0;
        }

        private void HideLast()
        {
            if (currentStateIndex < 0 || currentStateIndex > states.Length - 1)
                return;

            states[currentStateIndex].destructEvents?.Invoke();

            if (clusterType.Equals(EClusterType.ROAD) && lineIndex == MapGenerator.instance.centerLineIndex)
            {
                states[0].root.SetActive(false);
            }

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

        [System.Serializable]
        private class ParameterLogic
        {
            public List<int> avableIndex = new List<int>();
            public BaseParameterLogic parameterComponent = null;
        }
    }
}