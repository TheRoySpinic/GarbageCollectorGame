using Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Garage
{
    public class SwitchCar : MonoBehaviour
    {
        [SerializeField]
        private GameObject previusButton = null;
        [SerializeField]
        private GameObject nextButton = null;

        private void Awake()
        {
            GarageManager.E_ActiveCarUpdate -= LoadCarPrefab;
            GarageManager.E_ActiveCarUpdate += LoadCarPrefab;

            LoadCarPrefab(GarageManager.instance.GetActiveCarType());
        }

        private void OnDestroy()
        {
            GarageManager.E_ActiveCarUpdate -= LoadCarPrefab;
        }


        public void PreviusClickAction()
        {
            ECarType currentCar = GarageManager.instance.GetActiveCarType();
            GarageManager.CarPrefab[] carPrefabs = GarageManager.instance.GetCarPrefabs();

            if(carPrefabs[0].carType.Equals(currentCar))
            {
                return;
            }

            int currentIndex = Array.FindIndex(carPrefabs, (p) => { return p.carType.Equals(currentCar); });
            GarageManager.instance.SetActiveCar(carPrefabs[currentIndex - 1].carType);
        }

        public void NextClickAction()
        {
            ECarType currentCar = GarageManager.instance.GetActiveCarType();
            GarageManager.CarPrefab[] carPrefabs = GarageManager.instance.GetCarPrefabs();

            if (carPrefabs[carPrefabs.Length - 1].carType.Equals(currentCar))
            {
                return;
            }

            int currentIndex = Array.FindIndex(carPrefabs, (p) => { return p.carType.Equals(currentCar); });
            GarageManager.instance.SetActiveCar(carPrefabs[currentIndex + 1].carType);
        }


        private void UpdateButtons()
        {
            ECarType currentCar = GarageManager.instance.GetActiveCarType();
            GarageManager.CarPrefab[] carPrefabs = GarageManager.instance.GetCarPrefabs();

            previusButton.SetActive(!carPrefabs[0].carType.Equals(currentCar));
            nextButton.SetActive(!carPrefabs[carPrefabs.Length - 1].carType.Equals(currentCar));
        }

        private void LoadCarPrefab(ECarType carType)
        {
            if(!carType.Equals(ECarType.NONE))
            {
                Debug.LogError("Ivalide carType!!!");
                return;
            }
            GameObject o = Instantiate(GarageManager.instance.GetCarPrefab(carType),
                GarageManager.instance.GetCarSlot().transform);
            o.transform.position = GarageManager.instance.GetCarMesh().transform.position;
            o.transform.eulerAngles = GarageManager.instance.GetCarMesh().transform.eulerAngles;
            Destroy(GarageManager.instance.GetCarMesh());
            GarageManager.instance.SetCarMesh(o.GetComponent<CarMesh>());

            UpdateButtons();
        }
    }
}
