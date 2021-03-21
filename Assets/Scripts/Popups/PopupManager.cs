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
        [SerializeField]
        private ArrivalResult arrivalResult = null;

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

            instance.tint.SetActive(true);

            instance.arrivalResult.gameObject.SetActive(true);
            instance.arrivalResult.SetData();
        }

        public static void HideArrivalResult()
        {
            instance.tint.SetActive(false);

            instance.arrivalResult.gameObject.SetActive(false);
        }
    }
}