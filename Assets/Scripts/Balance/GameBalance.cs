using Base;
using Firebase.RemoteConfig;
using Firebase.RemouteConfig;
using Garage;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Balance
{
    public class GameBalance : SingletonGen<GameBalance>
    {
        public static Action E_ConfigReady;
        public static bool configReady = false;

        [SerializeField]
        private bool loadFromFirebase = true;

        [Space]
        [SerializeField]
        private StoreBalance storeBalance = new StoreBalance();

        [SerializeField]
        private PlayerBalance playerBalance = new PlayerBalance();

        [SerializeField]
        private MapBalance mapBalance = new MapBalance();

        [SerializeField]
        private BoostBalance boostBalance = new BoostBalance();

        [SerializeField]
        private CarGradeConfig carConfig = new CarGradeConfig();

        public override void Init()
        {
#if !UNITY_EDITOR
            loadFromFirebase = true;
#endif

            FirebaseRemouteConfigInit.E_FetchRemouteConfig_Success -= LoadConfigs;
            FirebaseRemouteConfigInit.E_FetchRemouteConfig_Success += LoadConfigs;
        }

        public override void Destroy()
        {
            FirebaseRemouteConfigInit.E_FetchRemouteConfig_Success -= LoadConfigs;
        }

        public static PlayerBalance GetPlayerBalance()
        {
            if(instance != null)
                return instance.playerBalance;

            return null;
        }

        public static MapBalance GetMapBalance()
        {
            if (instance != null)
                return instance.mapBalance;

            return null;
        }

        public static BoostBalance GetBoostBalance()
        {
            if (instance != null)
                return instance.boostBalance;

            return null;
        }

        public static CarGradeConfig GetCarGradeConfig()
        {
            if (instance != null)
                return instance.carConfig;

            return null;
        }

        public static StoreBalance GetStoreBalance()
        {
            if (instance != null)
                return instance.storeBalance;

            return null;
        }


        private void LoadConfigs()
        {
            if(!loadFromFirebase)
            {
                LoadBalanceFromFile<MapBalance>(out mapBalance, "Assets/Editor/Configs/mapBalance.json");
                configReady = true;
                E_ConfigReady?.Invoke();
                return;
            }

            storeBalance = JsonUtility.FromJson<StoreBalance>(FirebaseRemoteConfig.DefaultInstance.GetValue("storeConfig").StringValue);
            playerBalance = JsonUtility.FromJson<PlayerBalance>(FirebaseRemoteConfig.DefaultInstance.GetValue("playerBalance").StringValue);
            mapBalance = JsonUtility.FromJson<MapBalance>(FirebaseRemoteConfig.DefaultInstance.GetValue("mapBalance").StringValue);
            boostBalance = JsonUtility.FromJson<BoostBalance>(FirebaseRemoteConfig.DefaultInstance.GetValue("boostBalance").StringValue);
            carConfig = JsonUtility.FromJson<CarGradeConfig>(FirebaseRemoteConfig.DefaultInstance.GetValue("carConfig").StringValue);

            configReady = true;
            E_ConfigReady?.Invoke();
        }

        private void LoadBalanceFromFile<T>(out T result, string path)
        {
            result = default;
            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    result = JsonUtility.FromJson<T>(reader.ReadToEnd());
                }
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }
        }
    }
}