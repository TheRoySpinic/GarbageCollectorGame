using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

namespace Monetisation.IAP
{
    public class IAPManager : IStoreListener
    {
        private static IStoreController controller = null;
        private static IExtensionProvider extensions = null;

        public IAPManager()
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


            return PurchaseProcessingResult.Complete;
        }

        public void OnPurchaseFailed(Product i, PurchaseFailureReason p)
        {

        }

        public static Product[] GetProducts()
        {
            return controller.products.all;
        }

        public static Product GetProductById(string id)
        {
            return controller.products.WithID(id);
        }
    }
}