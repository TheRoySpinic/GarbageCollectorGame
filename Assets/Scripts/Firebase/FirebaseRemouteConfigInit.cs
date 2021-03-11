using Balance;
using Firebase.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Firebase.RemouteConfig
{
    public class FirebaseRemouteConfigInit : MonoBehaviour
    {
        public static event Action E_InilializeFirebaseRemouteConfig;
        public static event Action E_FetchRemouteConfig_Success;

        public static FirebaseRemouteConfigInit instance;

        //DependencyStatus dependencyStatus = DependencyStatus.UnavailableOther;


        void Awake()
        {
            instance = this;
            SetDefault();

            FirebaseInit.E_InilializeFirebase += FirebaseItinAction;
        }

        private void FirebaseItinAction()
        {
            FetchDataAsync();
            E_InilializeFirebaseRemouteConfig?.Invoke();
        }

        public Task FetchDataAsync()
        {
            Debug.Log("[F_RC] Fetching data...");
            Task fetchTask = RemoteConfig.FirebaseRemoteConfig.DefaultInstance.FetchAsync(TimeSpan.Zero);
            return fetchTask.ContinueWithOnMainThread(FetchComplete);
        }

        private void FetchComplete(Task fetchTask)
        {
            if (fetchTask.IsCanceled)
            {
                Debug.Log("[F_RC] Fetch canceled.");
            }
            else if (fetchTask.IsFaulted)
            {
                Debug.Log("[F_RC] Fetch encountered an error.");
            }
            else if (fetchTask.IsCompleted)
            {
                Debug.Log("[F_RC] Fetch completed successfully!");
            }

            RemoteConfig.ConfigInfo info = RemoteConfig.FirebaseRemoteConfig.DefaultInstance.Info;
            switch (info.LastFetchStatus)
            {
                case RemoteConfig.LastFetchStatus.Success:
                    RemoteConfig.FirebaseRemoteConfig.DefaultInstance.ActivateAsync();
                    Debug.Log(String.Format("[F_RC] Remote data loaded and ready (last fetch time {0}).", info.FetchTime));
                    Debug.Log("remouteConfig_debug: " + RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue("remouteConfig_debug").StringValue);
                    E_FetchRemouteConfig_Success?.Invoke();

                    break;
                case Firebase.RemoteConfig.LastFetchStatus.Failure:
                    switch (info.LastFetchFailureReason)
                    {
                        case RemoteConfig.FetchFailureReason.Error:
                            Debug.Log("[F_RC] Fetch failed for unknown reason");
                            break;
                        case RemoteConfig.FetchFailureReason.Throttled:
                            Debug.Log("[F_RC] Fetch throttled until " + info.ThrottledEndTime);
                            break;
                    }
                    break;
                case RemoteConfig.LastFetchStatus.Pending:
                    Debug.Log("[F_RC] Latest Fetch call still pending.");
                    break;
            }
        }

        private void SetDefault()
        {
            Dictionary<string, object> defaults = new Dictionary<string, object>();

            defaults.Add("remouteConfig_debug", "default local string");
            defaults.Add("carConfig", JsonUtility.ToJson(GameBalance.GetCarGradeConfig()));
            defaults.Add("boostBalance", JsonUtility.ToJson(GameBalance.GetBoostBalance()));
            defaults.Add("mapBalance", JsonUtility.ToJson(GameBalance.GetMapBalance()));
            defaults.Add("playerBalance", JsonUtility.ToJson(GameBalance.GetPlayerBalance()));
            defaults.Add("storeConfig", JsonUtility.ToJson(GameBalance.GetStoreBalance()));

            RemoteConfig.FirebaseRemoteConfig.DefaultInstance.SetDefaultsAsync(defaults);
        }
    }
}
