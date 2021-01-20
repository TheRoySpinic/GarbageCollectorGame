using Design;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Store
{
    public class StoreManager : MonoBehaviour
    {
        public static StoreManager instance = null;

        public static Action E_GoldUpdate;

        private static int _gold = 0;
        public static int gold { get { return _gold; } private set { _gold = value; PlayerPrefs.SetInt("gold", value); E_GoldUpdate?.Invoke(); } }

        //[SerializeField]
        //private Colorset[] colorsets = null;

        private void Awake()
        {
            if (instance == null)
                instance = this;

            _gold = PlayerPrefs.GetInt("gold");
            E_GoldUpdate?.Invoke();
        }

        private void OnDestroy()
        {
            if (instance.Equals(this))
                instance = null;
        }


        [Serializable]
        private class Colorset
        {
            public EColorsetType colorset = EColorsetType.DEFAULT;
            public Sprite albedo = null;

        }
    }
}