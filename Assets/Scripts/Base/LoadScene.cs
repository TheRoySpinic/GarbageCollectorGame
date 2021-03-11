using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Base
{
    public class LoadScene : MonoBehaviour
    {
        [SerializeField]
        private string sceneName = "MenuScene";

        private void Start()
        {
            SceneLoader.LoadScene(sceneName);
        }
    }
}