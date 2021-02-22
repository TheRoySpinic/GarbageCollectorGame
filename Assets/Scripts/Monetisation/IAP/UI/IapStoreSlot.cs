using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monetisation.IAP.UI
{
    public class IapStoreSlot : MonoBehaviour, IStoreSlot
    {
        private string storeId = "";

        public void SetData(string storeId)
        {
            this.storeId = storeId;
        }

        public void ClickAction()
        {

        }
    }
}