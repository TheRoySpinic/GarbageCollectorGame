using Store;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace HUD
{
    public class UpdateGoldCount : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text text = null;

        private void Awake()
        {
            MasterStoreManager.E_GoldUpdate += UpdateGold;
            UpdateGold();
        }

        private void UpdateGold()
        {
            text.text = MasterStoreManager.gold.ToString();
        }
    }
}