using Firebase.RemouteConfig;
using GooglePlayGames;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting;
using UnityEngine.UI;
using Base;
using Balance;

namespace Base
{
    public class SceneLoader : SingletonGen<SceneLoader>
    {
        public static event Action E_StartLoadScene;
        public static event Action E_LoadScene;

        [SerializeField]
        private GameObject LoadingScreen = null;

        [SerializeField]
        private TMP_Text progressText = null;

        private bool flag = false;

        public override void Init()
        {
            base.Init();

            InitGoogleServices.E_GoogleServices_AutchComplete -= CheckLoadComplete;
            FirebaseRemouteConfigInit.E_InilializeFirebaseRemouteConfig -= CheckLoadComplete;
            GameBalance.E_ConfigReady -= CheckLoadComplete;

            InitGoogleServices.E_GoogleServices_AutchComplete += CheckLoadComplete;
            FirebaseRemouteConfigInit.E_InilializeFirebaseRemouteConfig += CheckLoadComplete;
            GameBalance.E_ConfigReady += CheckLoadComplete;
        }

        public override void Destroy()
        {
            base.Destroy();

            InitGoogleServices.E_GoogleServices_AutchComplete -= CheckLoadComplete;
            FirebaseRemouteConfigInit.E_InilializeFirebaseRemouteConfig -= CheckLoadComplete;
            GameBalance.E_ConfigReady -= CheckLoadComplete;
        }

        public static void LoadScene(string name)
        {
            instance.ShowLoadingScreen(true);
            instance.StartCoroutine(instance.LoadAsyncSceneCoroutine(name));
        }

        private void ShowLoadingScreen(bool show)
        {
            LoadingScreen.SetActive(show);
        }

        private void CheckLoadComplete()
        {
            if (LoadComplete() && flag)
                FinishLoad();
        }

        private void FinishLoad()
        {
            flag = false;

            E_LoadScene?.Invoke();

            ShowLoadingScreen(false);
        }

        private bool LoadComplete()
        {
            return FirebaseRemouteConfigInit.ready && InitGoogleServices.ready && GameBalance.configReady;
        }

        private IEnumerator LoadAsyncSceneCoroutine(string sceneName)
        {
            E_StartLoadScene?.Invoke();

            if (progressText != null)
                progressText.text = "Loading... ";

            yield return new WaitForSeconds(3);

            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

            while (!asyncLoad.isDone)
            {
                if (progressText != null)
                    progressText.text = "Loading... " + Mathf.Round(asyncLoad.progress * 100) + "%";
                yield return null;
            }

            if (progressText != null)
                progressText.text = "Loading... 100%";

            if(!flag && (LoadComplete() || Application.internetReachability.Equals(NetworkReachability.NotReachable)))
            {
                FinishLoad();
            }
            else
            {
                flag = true;
                yield return new WaitForSeconds(5);
                
                if(flag)
                    FinishLoad();
            }
        }
    }
}