using Garage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Popups
{
    public class PopupManager : MonoBehaviour
    {
        public static PopupManager instance = null;

        [SerializeField]
        private GameObject tint = null;
        [SerializeField]
        private CarGradeLevelUp_Confirm upgradeConfirm = null;


        private void Awake()
        {
            if (instance == null)
                instance = this;
        }

        public static void ShowUpgradeConfirm(EGradeType gradeType)
        {
            instance.tint.SetActive(true);

            instance.upgradeConfirm.gameObject.SetActive(true);
            instance.upgradeConfirm.SetData(gradeType);
        }

        public static void HideUpgradeConfirm()
        {
            instance.tint.SetActive(false);

            instance.upgradeConfirm.gameObject.SetActive(false);
        }
    }
}