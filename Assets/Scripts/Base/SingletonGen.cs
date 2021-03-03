using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Base
{
    public class SingletonGen<T> : MonoBehaviour where T : SingletonGen<T>
    {
        public static T instance { get; private set; } = null;

        public static event Action E_Ready;

        private void Awake()
        {

            if (instance == null)
                instance = (T)this;

            Init();

            E_Ready?.Invoke();
        }

        private void OnDestroy()
        {
            Destroy();
            
            if (instance != null && instance.Equals(this))
                instance = null;
        }

        virtual public void Init()
        {

        }

        virtual public void Destroy()
        {

        }
    }
}