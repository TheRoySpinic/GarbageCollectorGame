using Store;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Tools;
using UnityEngine;

namespace Garage.UI
{
    public class BuyCar : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text costText = null;

        [SerializeField]
        private GameObject costPanel = null;

        private void Awake()
        {
            GarageManager.E_ViewCarUpdate -= UpdateCostText;
            GarageManager.E_ViewCarUpdate += UpdateCostText;

            UpdateCostText(GarageManager.instance.GetViewCarType());
        }

        private void OnDestroy()
        {
            GarageManager.E_ViewCarUpdate -= UpdateCostText;
        }

        public void ClickActioin()
        {
            GarageManager.instance.OpenCar(GarageManager.instance.GetViewCarType());
        }


        private void UpdateCostText(ECarType carType)
        {
            if(GarageManager.instance.IsOwnedCar(carType))
            {
                costPanel.SetActive(false);
                return;
            }

            int cost = GarageManager.instance.GetCarCost(GarageManager.instance.GetViewCarType());

            costPanel.SetActive(true);
            costText.text = TextFormater.FormatGold(cost);

            costText.color = TextFormater.GetCostColor(cost);
        }
    }
}
