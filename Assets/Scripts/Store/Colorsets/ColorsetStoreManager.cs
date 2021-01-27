using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Store.Colorset
{
    public class ColorsetStoreManager : MonoBehaviour
    {
        private const string PLAYERPREFS_PLAYER_COLORSETS_FIELD = "player_colorsets";

        public static ColorsetStoreManager instance = null;

        public static Action E_PlayerColorsetUpdate;
        public static Action E_PlayerColorsetsLoaded;


        [SerializeField]
        private Colorset[] colorsets = null;

        [SerializeField]
        private PlayerColorsets playerColorsets = new PlayerColorsets();

        private void Awake()
        {
            if (instance == null)
                instance = this;
            
            if(LoadPlayerColorsets())
                E_PlayerColorsetsLoaded?.Invoke();
        }

        private void OnDestroy()
        {
            if (instance.Equals(this))
                instance = null;
        }
        
        public Texture GetColorsetMainTexture(EColorsetType colorsetType)
        {
            return Array.Find(colorsets, (c) => { return c.colorset == colorsetType; }).maintTexture;
        }
        
        public int GetColorsetCost(EColorsetType colorsetType)
        {
            return Array.Find(colorsets, (c) => { return c.colorset == colorsetType; }).cost;
        }

        public EColorsetType GetActivePlayerColorset()
        {
            return playerColorsets.activeColorset;
        }


        private void SavePlayerColorsets()
        {
            PlayerPrefs.SetString(PLAYERPREFS_PLAYER_COLORSETS_FIELD, JsonUtility.ToJson(playerColorsets));
        }

        private bool LoadPlayerColorsets()
        {
            if (PlayerPrefs.GetString(PLAYERPREFS_PLAYER_COLORSETS_FIELD).Length > 0)
            {
                playerColorsets = JsonUtility.FromJson<PlayerColorsets>(PlayerPrefs.GetString(PLAYERPREFS_PLAYER_COLORSETS_FIELD));
                return true;
            }
            return false;
        }

        [Serializable]
        private class PlayerColorsets
        {
            public EColorsetType activeColorset = EColorsetType.DEFAULT;
            public List<EColorsetType> colorsets = new List<EColorsetType>();
        }

        [Serializable]
        private class Colorset
        {
            public EColorsetType colorset = EColorsetType.DEFAULT;
            public Texture maintTexture = null;
            public int cost = 1;

        }
    }
}