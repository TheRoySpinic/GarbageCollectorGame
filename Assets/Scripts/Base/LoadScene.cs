using Balance;
using Firebase.RemouteConfig;
using GooglePlayGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Base
{
    public class LoadScene : MonoBehaviour
    {
        [SerializeField]
        private string sceneName = "MenuScene";

        [SerializeField]
        private bool waitFullLoad = false;
        [SerializeField]
        private int waitForSecond = 5;

        private void Start()
        {
            if(waitFullLoad && !LoadComplete())
            {
                StartCoroutine(WaitLoad());
            }
            else
            {
                FinishLoad();
            }
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
            for(int i = 0; i < waitForSecond; ++i)
            {
                if(LoadComplete())
                {
                    FinishLoad();
                    break;
                }

                yield return new WaitForSeconds(1);
            }

            yield return null;
        }
    }
}