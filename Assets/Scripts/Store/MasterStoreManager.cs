using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Store
{
    public class MasterStoreManager : MonoBehaviour
    {
        public static MasterStoreManager instance = null;

        public static Action E_GoldUpdate;

        private static int _gold = 0;
        public static int gold { get { return _gold; } private set { _gold = value; PlayerPrefs.SetInt("gold", value); E_GoldUpdate?.Invoke(); } }

        private void Awake()
        {
            if (instance == null)
                instance = this;

            _gold = PlayerPrefs.GetInt("gold");
            E_GoldUpdate?.Invoke();
        }

        public void AddGold(int toAdd)
        {

        }
    }
}