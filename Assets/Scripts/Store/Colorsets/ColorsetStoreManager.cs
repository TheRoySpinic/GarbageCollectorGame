using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Store.Colorsets
{
    public class ColorsetStoreManager : MonoBehaviour
    {
        private const string PLAYERPREFS_PLAYER_COLORSETS_FIELD = "player_colorsets";

        public static ColorsetStoreManager instance = null;

        public static event Action E_PlayerColorsetUpdate;
        public static event Action E_PlayerColorsetsLoaded;


        [SerializeField]
        private ColorsetData[] colorsets = null;

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
        
        public void StoreButtonClickAction(EColorsetType colorsetType)
        {
            if(IsOpenColorset(colorsetType))
            {
                SetActiveColorset(colorsetType);
                return;
            }
            else
            {
                OpenColorset(colorsetType);
            }
        }

        public Texture GetColorsetMainTexture(EColorsetType colorsetType)
        {
            return Array.Find(colorsets, (c) => { return c.colorset == colorsetType; }).maintTexture;
        }

        public EColorsetType GetActivePlayerColorset()
        {
            return playerColorsets.activeColorset;
        }

        public List<ColorsetData> GetColorsetsData()
        {
            List<ColorsetData> list = new List<ColorsetData>();
            list.AddRange(colorsets);
            return list;
        }

        public bool IsOpenColorset(EColorsetType type)
        {
            foreach(EColorsetType colorset in playerColorsets.colorsets)
            {
                if (colorset == type)
                    return true;
            }
            return false;
        }

        public bool SetActiveColorset(EColorsetType type)
        {
            if(playerColorsets.activeColorset == type)
            return false;

            if (playerColorsets.colorsets.Contains(type))
            {
                playerColorsets.activeColorset = type;
                SavePlayerColorsets();
                E_PlayerColorsetUpdate?.Invoke();
                return true;
            }
            else
                return false;
        }

        public bool OpenColorset(EColorsetType type)
        {
            if(!playerColorsets.colorsets.Contains(type))
            {
                int cost = Array.Find(colorsets, (c) => { return c.colorset == type; }).cost;

                if(MasterStoreManager.instance.SubstractGold(cost))
                {
                    playerColorsets.colorsets.Add(type);
                    SavePlayerColorsets();
                    E_PlayerColorsetUpdate?.Invoke();
                    return true;
                }
            }
            return false;
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
    }
}