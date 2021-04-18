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
        private ColorsetParameters[] colorsets = null;

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
            foreach(ColorsetParameters colorset in colorsets)
            {
                colorset.light.SetActive(false);
                foreach(GameObject o in colorset.enviroment)
                {
                    o.SetActive(false);
                }
            }

            foreach(ColorsetParameters colorset in colorsets)
            {
                if(colorset.colorsetType == type)
                {
                    colorset.light.SetActive(true);
                    foreach (GameObject o in colorset.enviroment)
                    {
                        o.SetActive(true);
                    }
                }
            }
        }

        [System.Serializable]
        private class ColorsetParameters
        {
            public EColorsetType colorsetType = EColorsetType.NONE;
            public GameObject light = null;
            public List<GameObject> enviroment = new List<GameObject>();
        }
    }
}