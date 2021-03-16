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

            List<StoreProduct> storeProducts = IAPManager.GetStoreProducts();

            foreach(StoreProduct product in storeProducts)
            {
                Product productData = IAPManager.GetProductById(product.storeId);

                //Создаём сторовский слот
            }
        }

        private void ClearList()
        {

        }
    }
}