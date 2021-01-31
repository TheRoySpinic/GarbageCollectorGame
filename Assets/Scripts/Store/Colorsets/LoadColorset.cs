using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Store.Colorsets
{
    public class LoadColorset : MonoBehaviour
    {
        public static LoadColorset instance = null;

        [SerializeField]
        private Material material = null;

        [Header("Light")]
        [SerializeField]
        private ColorsetLighting[] colorsets = null;

        private void Awake()
        {
            if (instance == null)
                instance = this;

            ColorsetStoreManager.E_PlayerColorsetsLoaded -= LoadActiveColorset;
            ColorsetStoreManager.E_PlayerColorsetsLoaded += LoadActiveColorset;

            ColorsetStoreManager.E_PlayerColorsetUpdate -= LoadActiveColorset;
            ColorsetStoreManager.E_PlayerColorsetUpdate += LoadActiveColorset;

            LoadActiveColorset();
        }

        private void OnDestroy()
        {
            if (instance.Equals(this))
                instance = null;

            ColorsetStoreManager.E_PlayerColorsetsLoaded -= LoadActiveColorset;
            ColorsetStoreManager.E_PlayerColorsetUpdate -= LoadActiveColorset;
        }

        private void LoadActiveColorset()
        {
            SetMainTexture(
            ColorsetStoreManager.instance.GetColorsetMainTexture(ColorsetStoreManager.instance.GetActivePlayerColorset())
            );
            SetActiveLight(ColorsetStoreManager.instance.GetActivePlayerColorset());
        }

        private void SetMainTexture(Texture texture)
        {
            material.SetTexture("_MainTex", texture);
        }

        private void SetActiveLight(EColorsetType type)
        {
            foreach(ColorsetLighting lighting in colorsets)
            {
                lighting.light.SetActive(false);
            }

            foreach(ColorsetLighting lighting in colorsets)
            {
                if(lighting.colorsetType == type)
                    lighting.light.SetActive(true);
            }
        }

        [System.Serializable]
        private class ColorsetLighting
        {
            public EColorsetType colorsetType = EColorsetType.NONE;
            public GameObject light = null;
        }
    }
}