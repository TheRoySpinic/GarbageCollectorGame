using Balance;
using HUD;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

namespace Monetisation.IAP.UI
{
    public class PrepareStoreList : MonoBehaviour
    {
        public static PrepareStoreList instance = null;

        [SerializeField]
        private GameObject content = null;

        [SerializeField]
        private GameObject slotPrefab = null;
        
        [SerializeField]
        private GameObject spacer = null;

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

            if(spacer != null)
                Instantiate(spacer, content.transform);

            List<StoreProduct> storeProducts = IAPManager.GetStoreProducts();

            foreach(StoreProduct product in storeProducts)
            {
                Instantiate(slotPrefab, content.transform).GetComponent<IapStoreSlot>().SetData(product.storeId);
            }
        }

        private void ClearList()
        {
            while(content.transform.childCount > 0)
                DestroyImmediate(content.transform.GetChild(0).gameObject);
        }
    }
}