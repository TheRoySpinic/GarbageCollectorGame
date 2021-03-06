using Base;
using Garage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Popups
{
    public class PopupManager : SingletonGen<PopupManager>
    {

        [SerializeField]
        private GameObject tint = null;
        [SerializeField]
        private CarGradeLevelUp_Confirm upgradeConfirm = null;

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

        public static void ShowArrivalResult()
        {

        }

        public static void HideArrivalResult()
        {

        }
    }
}