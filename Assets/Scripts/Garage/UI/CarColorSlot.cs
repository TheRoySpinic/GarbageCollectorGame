using System.Collections;
using System.Collections.Generic;
using TMPro;
using Tools;
using UnityEngine;
using UnityEngine.UI;

namespace Garage.UI
{
    public class CarColorSlot : MonoBehaviour
    {
        [SerializeField]
        private Image icon = null;
        [SerializeField]
        private TMP_Text costText = null;
        [SerializeField]
        private GameObject costPanel = null;
        [SerializeField]
        private GameObject check = null;

        private int index = -1;

        public void SetData(Color color, int index)
        {
            this.index = index;
            icon.color = color;
            check.SetActive(GarageManager.instance.GetActiveCarColorIndex() == index);

            int cost = GarageManager.instance.GetColorCost(index);
            costText.text = TextFormater.FormatGold(cost);
            costPanel.SetActive(!(GarageManager.instance.IsOwnedColor(index) || cost == 0));
        }

        public void ClickAction()
        {
            GarageManager.instance.ColorClickAction(index);
        }
    }
}