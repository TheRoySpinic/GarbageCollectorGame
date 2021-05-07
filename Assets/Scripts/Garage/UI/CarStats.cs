using HUD;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Garage.UI
{
    class CarStats : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text speedText = null;

        [SerializeField]
        private TMP_Text armorText = null;

        private void Awake()
        {
            ScreensManager.E_ShowGarage -= ShowStats;
            ScreensManager.E_ShowGarage += ShowStats;

            GarageManager.E_GradeUpgrade -= ShowStats;
            GarageManager.E_GradeUpgrade += ShowStats;

            GarageManager.E_ViewCarUpdate -= UpdateStats;
            GarageManager.E_ViewCarUpdate += UpdateStats;
        }

        private void OnDestroy()
        {
            ScreensManager.E_ShowGarage -= ShowStats;
            GarageManager.E_GradeUpgrade -= ShowStats;
            GarageManager.E_ViewCarUpdate -= UpdateStats;
        }


        private void UpdateStats(ECarType carType)
        {
            ShowStats();
        }

        private void ShowStats()
        {
            speedText.text = GarageManager.instance.GetActiveCarSpeed().ToString();
            armorText.text = GarageManager.instance.GetActiveCarHealth().ToString();
        }
    }
}
