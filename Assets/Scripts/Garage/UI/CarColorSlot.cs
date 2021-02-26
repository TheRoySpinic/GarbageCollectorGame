using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Garage.UI
{
    public class CarColorSlot : MonoBehaviour
    {
        [SerializeField]
        private Image icon = null;
        [SerializeField]
        private GameObject check = null;

        private int index = -1;

        public void SetData(Color color, int index)
        {
            this.index = index;
            icon.color = color;
            check.SetActive(GarageManager.instance.GetActiveCarColorIndex() == index);
        }

        public void ClickAction()
        {
            GarageManager.instance.ColorClickAction(index);
        }
    }
}