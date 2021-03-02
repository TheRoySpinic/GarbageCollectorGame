using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arrival
{
    public class ArrivalManager : MonoBehaviour
    {
        public static ArrivalManager instance { get; private set; } = null;

        public static Action E_Start;
        public static Action E_End;

        private void Awake()
        {
            if (instance == null)
                instance = this;
        }

        public void StartArrival()
        {

            E_Start?.Invoke();
        }

        public void EndArrival()
        {

            E_End?.Invoke();
        }
    }
}