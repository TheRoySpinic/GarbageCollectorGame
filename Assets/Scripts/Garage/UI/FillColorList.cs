using HUD;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Garage.UI
{
    public class FillColorList : MonoBehaviour
    {
        [SerializeField]
        private GameObject content = null;

        [SerializeField]
        private GameObject slot = null;

        [SerializeField]
        private GameObject spacer = null;

        private void Awake()
        {
            ScreensManager.E_ShowGarage -= FillList;
            ScreensManager.E_ShowGarage += FillList;

            GarageManager.E_ColorUpgrade -= FillList;
            GarageManager.E_ColorUpgrade += FillList;
        }

        private void OnDestroy()
        {
            ScreensManager.E_ShowGarage -= FillList;
            GarageManager.E_ColorUpgrade -= FillList;
        }

        private void FillList()
        {
            throw new NotImplementedException();
        }
    }
}