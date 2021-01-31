using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Store.Colorsets.UI
{
    class ColorsetStoreButton : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text colorsetName = null;
        [SerializeField]
        private GameObject costPanel = null;
        [SerializeField]
        private TMP_Text costText = null;
        [SerializeField]
        private GameObject tint = null;
        [SerializeField]
        private GameObject quesionIcon = null;
        [SerializeField]
        private GameObject checkIcon = null;
        [SerializeField]
        private Image bgImage = null;

        private EColorsetType colorset = EColorsetType.NONE;
       
        public void SetData(ColorsetData storeSlot)
        {
            bool isOwned = ColorsetStoreManager.instance.IsOpenColorset(storeSlot.colorset);

            colorset = storeSlot.colorset;

            tint.SetActive(!isOwned);
            quesionIcon.SetActive(!isOwned);
            costPanel.SetActive(!isOwned);
            checkIcon.SetActive(ColorsetStoreManager.instance.GetActivePlayerColorset() == storeSlot.colorset);

            colorsetName.text = storeSlot.name;
            costText.text = storeSlot.cost.ToString();
            bgImage.sprite = storeSlot.iconSprite;
        }

        public void ClickAction()
        {
            ColorsetStoreManager.instance.StoreButtonClickAction(colorset);
        }
    }
}
