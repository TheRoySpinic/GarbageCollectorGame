using Balance;
using Base;
using Store;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

namespace Monetisation.IAP
{
    public class IAPManager : SingletonGen<IAPManager>, IStoreListener
    {
        private static IStoreController controller = null;
        private static IExtensionProvider extensions = null;

        public override void Init()
        {
            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
            builder.AddProduct("1_gold_coins", ProductType.Consumable, new IDs
            {
                {"1_gold_coins_google", GooglePlay.Name}
            });

            UnityPurchasing.Initialize(this, builder);
        }

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            IAPManager.controller = controller;
            IAPManager.extensions = extensions;
        }

        public void OnInitializeFailed(InitializationFailureReason error)
        {

        }

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e)
        {
            StoreProduct storeProduct = GetStoreProductById(e.purchasedProduct.definition.id);

            RewardManager.AddReward(storeProduct);

            SaveManager.SaveAll();

            return PurchaseProcessingResult.Complete;
        }

        public void OnPurchaseFailed(Product i, PurchaseFailureReason p)
        {

        }

        public static void BuyProductID(string productId)
        {
            // If Purchasing has been initialized ...
            if (IsInitialized())
            {
                // ... look up the Product reference with the general product identifier and the Purchasing 
                // system's products collection.
                Product product = controller.products.WithID(productId);

                // If the look up found a product for this device's store and that product is ready to be sold ... 
                if (product != null && product.availableToPurchase && ProductDataIsValide(product))
                {
                    Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                    // ... buy the product. Expect a response either through ProcessPurchase or OnPurchaseFailed 
                    // asynchronously.
                    controller.InitiatePurchase(product);
                }
                // Otherwise ...
                else
                {
                    // ... report the product look-up failure situation  
                    Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
                }
            }
            // Otherwise ...
            else
            {
                // ... report the fact Purchasing has not succeeded initializing yet. Consider waiting longer or 
                // retrying initiailization.
                Debug.Log("BuyProductID FAIL. Not initialized.");
            }
        }

        public static Product[] GetProducts()
        {
            return controller.products.all;
        }

        public static Product GetProductById(string id)
        {
            return controller.products.WithID(id);
        }

        public static StoreProduct GetStoreProductById(string id)
        {
            return Array.Find(GetStoreProducts().ToArray(), (p) => { return p.storeId.Equals(id); });
        }

        public static List<StoreProduct> GetStoreProducts()
        {
            return GameBalance.GetStoreBalance().storeProducts;
        }


        private static bool ProductDataIsValide(Product product)
        {
            StoreProduct storeProduct = GetStoreProductById(product.definition.id);
            return storeProduct != null && storeProduct.reward != null && storeProduct.reward.rewardType != ERewardType.NONE && storeProduct.reward.value > 0;
        }

        private static bool IsInitialized()
        {
            return controller != null && extensions != null;
        }
    }
}
