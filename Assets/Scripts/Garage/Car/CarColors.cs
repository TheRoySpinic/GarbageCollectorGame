using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Garage.Car
{
    public class CarColors : MonoBehaviour, ICarColors
    {
        [SerializeField]
        private CarColor[] carColors = null;

        [SerializeField]
        private Material material_brighter = null;
        [SerializeField]
        private Material material_darker = null;

        private void Awake()
        {
            GarageManager.E_ColorUpgrade -= UpdateColorEvent;
            GarageManager.E_ColorUpgrade += UpdateColorEvent;

            UpdateColorEvent();
        }

        private void OnDestroy()
        {
            GarageManager.E_ColorUpgrade -= UpdateColorEvent;
        }


        public Color[] GetCarColors()
        {
            Color[] result = new Color[carColors.Length];
            int i = 0;

            foreach(CarColor carColor in carColors)
            {
                result[i] = carColor.brighter;
                i++;
            }

            return result;
        }


        private void UpdateColorEvent()
        {
            if(GarageManager.instance)
                UpdateCarColor(GarageManager.instance.GetActiveCarColorIndex());
        }

        private void UpdateCarColor(int index)
        {
            if(index >= 0 && index < carColors.Length)
            {
                material_brighter.color = carColors[index].brighter;
                material_darker.color = carColors[index].darker;
            }
        }


        [System.Serializable]
        private class CarColor
        {
            public Color brighter = Color.white;
            public Color darker = Color.gray;
        }
    }
}
