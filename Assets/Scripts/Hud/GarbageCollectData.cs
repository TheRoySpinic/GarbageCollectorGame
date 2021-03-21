using System.Collections;
using System.Collections.Generic;
using Target;
using TMPro;
using Tools;
using UnityEngine;
using UnityEngine.UI;

namespace HUD
{
    public class GarbageCollectData : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text capasityText = null;
        [SerializeField]
        private GameObject isFullText = null;
        [SerializeField]
        private GameObject dark = null;
        [SerializeField]
        private GameObject check = null;
        [SerializeField]
        private ProgressBar progressBar = null;

        private void Awake()
        {
            TargetManager.E_UpdateGarbageContains -= UpdateUI;
            TargetManager.E_UpdateGarbageContains += UpdateUI;

            UpdateUI();
        }

        private void OnDestroy()
        {
            TargetManager.E_UpdateGarbageContains -= UpdateUI;
        }

        private void UpdateUI()
        {
            dark.SetActive(!TargetManager.instance.canGrabGarbage);
            check.SetActive(!TargetManager.instance.canGrabGarbage);
            isFullText.SetActive(!TargetManager.instance.canGrabGarbage);

            capasityText.text = TargetManager.instance.garbageCount + "/" + TargetManager.instance.maxGarbages;

            progressBar.SetValue((float) TargetManager.instance.garbageCount / (float) TargetManager.instance.maxGarbages);
        }
    }
}