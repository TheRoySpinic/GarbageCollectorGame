using Balance;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Tools;
using UnityEngine;
using UnityEngine.Purchasing;

namespace Monetisation.IAP.UI
{
    public class IapStoreSlot : MonoBehaviour, IStoreSlot
    {
        [Header("Icons")]
        [SerializeField]
        private GameObject[] coinsIcons = null;

        [Space]
        [Header("Profit")]
        [SerializeField]
        private GameObject profitIcon = null;
        [SerializeField]
        private TMP_Text profitText = null;

        [Space]
        [Header("Values")]
        [SerializeField]
        private TMP_Text rewardCountText = null;
        [SerializeField]
        private TMP_Text costText = null;

        private string storeId = "";

        public void SetData(string storeId)
        {
            this.storeId = storeId;
            Product productData = IAPManager.GetProductById(storeId);
            StoreProduct storeProduct = IAPManager.GetStoreProductById(storeId);

            HideAllIcons();

            if(storeProduct.reward.iconId < 0)
            {
                coinsIcons[0].SetActive(true);
            }

            rewardCountText.text = TextFormater.FormatGold(storeProduct.reward.value) + " COINS";

            costText.text = productData.metadata.localizedPriceString;

            if(storeProduct.profitValue > 0)
            {
                profitIcon.SetActive(true);
                profitText.text = TextFormater.FormatGold(storeProduct.profitValue) + "%";
            }
            else
            {
                profitIcon.SetActive(false);
            }
        }

        public void ClickAction()
        {
            IAPManager.BuyProductID(storeId);
        }


        private void HideAllIcons()
        {
            List<GameObject[]> objects = new List<GameObject[]>();

            objects.Add(coinsIcons);

            foreach (GameObject[] o in objects)
                foreach (GameObject item in o)
                    item.SetActive(false);
        }
    }
}