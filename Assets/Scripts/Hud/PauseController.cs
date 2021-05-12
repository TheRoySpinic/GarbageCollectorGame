using HUD;
using System.Collections;
using System.Collections.Generic;
using Target;
using TMPro;
using Tools;
using UnityEngine;

namespace HUD
{
    public class PauseController : MonoBehaviour
    {
        [SerializeField]
        private MenuManager menuManager = null;

        [SerializeField]
        private GameObject pausePopup = null;

        [SerializeField]
        private TMP_Text distance = null;

        public void GamePause()
        {
            StopCoroutine(SmoothTimeScale());

            pausePopup.SetActive(true);
            distance.text = TextFormater.FormatGold((int)TargetManager.currentDistance);
            Time.timeScale = 0;
        }

        public void GameUnpause()
        {
            pausePopup.SetActive(false);

            StartCoroutine(SmoothTimeScale());
        }

        public void GoToMenu()
        {
            Time.timeScale = 1;
            menuManager.OpenMenu();
        }

        private IEnumerator SmoothTimeScale()
        {
            float step = 0.01f;

            while (Time.timeScale < 1)
            {
                Time.timeScale += step;

                yield return new WaitForEndOfFrame();
            }

            Time.timeScale = 1;
        }
    }
}