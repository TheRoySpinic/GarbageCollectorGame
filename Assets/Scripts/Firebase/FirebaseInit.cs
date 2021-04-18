using Firebase.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Firebase
{
    public class FirebaseInit : MonoBehaviour
    {
        public delegate void D_InilializeFirebase();
        public static event D_InilializeFirebase E_InilializeFirebase;

        public static FirebaseInit instance;

        DependencyStatus dependencyStatus = DependencyStatus.UnavailableOther;

        private void Awake()
        {
            instance = this;

            Init();
        }

        public void Init()
        {
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
            {
                dependencyStatus = task.Result;
                if (dependencyStatus == DependencyStatus.Available)
                {
                //FirebaseApp.LogLevel = LogLevel.Debug;
                InitializeFirebase();
                }
                else
                {
                    Debug.LogError(
                      "[Firebase] Could not resolve all Firebase dependencies: " + dependencyStatus);
                }
            });
        }

        private void InitializeFirebase()
        {
            Debug.Log("[Firebase] Firebase ready!!!");
            E_InilializeFirebase?.Invoke();
            Analytics.FirebaseAnalytics.LogEvent(Analytics.FirebaseAnalytics.EventLogin);
        }
    }
}