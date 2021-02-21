using HUD;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monetisation.IAP.UI
{
    public class PrepareStoreList : MonoBehaviour
    {
        public static PrepareStoreList instance = null;

        [SerializeField]
        private GameObject content = null;

        private void Awake()
        {
            if (instance == null)
                instance = this;

            ScreensManager.E_ShowBuyStore -= PrepareStore;
            ScreensManager.E_ShowBuyStore += PrepareStore;
        }

        private void OnDestroy()
        {
            ScreensManager.E_ShowBuyStore -= PrepareStore;
        }

        public void PrepareStore()
        {
            ClearList();
        }

        private void ClearList()
        {

        }
    }
}