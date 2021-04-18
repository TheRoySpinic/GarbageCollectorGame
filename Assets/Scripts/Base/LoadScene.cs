using Balance;
using Firebase.RemouteConfig;
using GooglePlayGames;
using Popups;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Base
{
    public class LoadScene : SingletonGen<LoadScene>
    {
        [SerializeField]
        private string sceneName = "MenuScene";

        [SerializeField]
        private bool waitFullLoad = false;
        [SerializeField]
        private int waitForSecond = 5;

        private void Start()
        {
            Load();
        }


        public void Load()
        {
            if(Application.internetReachability == NetworkReachability.NotReachable)
            {
                ShowConnectionErrorPopup();
                return;
            }

            if(waitFullLoad && !LoadComplete())
            {
                StartCoroutine(WaitLoad());
            }
            else
            {
                FinishLoad();
            }
        }


        private void ShowConnectionErrorPopup()
        {
            PopupManager.ShowConnection();
        }

        private void FinishLoad()
        {
            SceneLoader.LoadScene(sceneName);
        }

        private bool LoadComplete()
        {
            return FirebaseRemouteConfigInit.ready && InitGoogleServices.ready && GameBalance.configReady;
        }


        private IEnumerator WaitLoad()
        {
            bool show = true;

            for(int i = 0; i < waitForSecond; ++i)
            {
                yield return new WaitForSeconds(1);
                if(LoadComplete())
                {
                    FinishLoad();
                    show = false;
                    break;
                }
            }
            if(show)
                ShowConnectionErrorPopup();
        }
    }
}