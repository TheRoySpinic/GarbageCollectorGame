using HUD;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Store.Colorsets.UI
{
    public class FillColorsetList : MonoBehaviour
    {
        [SerializeField]
        private GameObject content = null;
        [SerializeField]
        private GameObject slotPrefab = null;
        [SerializeField]
        private GameObject spacerPrefab = null;

        private void Awake()
        {
            ScreensManager.E_ShowColorsetStore -= FillList;
            ScreensManager.E_ShowColorsetStore += FillList;
            ColorsetStoreManager.E_PlayerColorsetUpdate -= FillList;
            ColorsetStoreManager.E_PlayerColorsetUpdate += FillList;
        }

        public void FillList()
        {
            ClearList();

            Instantiate(spacerPrefab, content.transform);

            foreach (ColorsetData slot in ColorsetStoreManager.instance.GetColorsetsData())
            {
                Instantiate(slotPrefab, content.transform).GetComponent<ColorsetStoreButton>().SetData(slot);
            }
        }

        private void ClearList()
        {
            while(content.transform.childCount > 0)
            {
                DestroyImmediate(content.transform.GetChild(0).gameObject);
            }
        }
    }
}