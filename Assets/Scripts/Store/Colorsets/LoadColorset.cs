using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Store.Colorset
{
    public class LoadColorset : MonoBehaviour
    {
        public static LoadColorset instance = null;

        [SerializeField]
        private Material material = null;

        private void Awake()
        {
            if (instance == null)
                instance = this;

            ColorsetStoreManager.E_PlayerColorsetsLoaded -= LoadActiveColorset;
            ColorsetStoreManager.E_PlayerColorsetsLoaded += LoadActiveColorset;

            ColorsetStoreManager.E_PlayerColorsetUpdate -= LoadActiveColorset;
            ColorsetStoreManager.E_PlayerColorsetUpdate += LoadActiveColorset;
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
        }

        public void SetMainTexture(Texture texture)
        {
            material.SetTexture("_MainTex", texture);
        }
    }
}