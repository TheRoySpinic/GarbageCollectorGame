using Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameDebug
{
    public class PlaymodeSceneSwitcher : MonoBehaviour
    {
        [SerializeField]
        private string baseSceneName = "Title";

        void Awake()
        {
            if (!SceneLoader.instance)
            {
                SceneManager.LoadScene(baseSceneName);
            }
        }
    }
}