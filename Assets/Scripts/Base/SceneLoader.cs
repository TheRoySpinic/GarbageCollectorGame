using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting;
using UnityEngine.UI;

namespace Base
{
    public class SceneLoader : MonoBehaviour
    {
        public static SceneLoader instance;

        public static event Action E_StartLoadScene;
        public static event Action E_LoadScene;

        [SerializeField]
        private GameObject LoadingScreen = null;

        [SerializeField]
        private TMP_Text progressText = null;

        private void Awake()
        {
            instance = this;
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

        private IEnumerator LoadAsyncSceneCoroutine(string sceneName)
        {
            E_StartLoadScene?.Invoke();

            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

            while (!asyncLoad.isDone)
            {
                if (progressText != null)
                    progressText.text = "Loading... " + Mathf.Round(asyncLoad.progress * 100) + "%";
                yield return null;
            }

            if (progressText != null)
                progressText.text = "Loading... 100%";

            try
            {
                E_LoadScene?.Invoke();
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }

            ShowLoadingScreen(false);
        }
    }
}