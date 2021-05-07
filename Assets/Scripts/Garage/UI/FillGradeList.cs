using HUD;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Garage.UI
{
    public class FillGradeList : MonoBehaviour
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

            GarageManager.E_GradeUpgrade -= FillList;
            GarageManager.E_GradeUpgrade += FillList;

            GarageManager.E_ViewCarUpdate -= UpdateList;
            GarageManager.E_ViewCarUpdate += UpdateList;
        }

        private void OnDestroy()
        {
            ScreensManager.E_ShowGarage -= FillList;
            GarageManager.E_GradeUpgrade -= FillList;
            GarageManager.E_ViewCarUpdate -= UpdateList;
        }

        private void UpdateList(ECarType carType)
        {
            FillList();
        }

        private void FillList()
        {
            ClearList();

            Instantiate(spacer, content.transform);
            
            GradeData[] data = GarageManager.instance.GetCarGradeData(GarageManager.instance.GetViewCarType()).grades;
            
            if (GarageManager.instance.IsOwnedCar(GarageManager.instance.GetViewCarType()))
            {
                CarGradePlayerData carGrade = GarageManager.instance.GetPlayerCarGrades();
                foreach (GradeLevel level in carGrade.grades)
                {
                    if (Array.Find(data, (d) => { return d.gradeType.Equals(level.gradeType);}) != null)
                        Instantiate(slot, content.transform).GetComponent<CarGradeSlot>().SetData(level.gradeType, level.level);
                }
            }
            else
            {
                foreach (GradeData grade in data)
                {
                    Instantiate(slot, content.transform).GetComponent<CarGradeSlot>().SetData(grade.gradeType, 0);
                }
            }

            Instantiate(spacer, content.transform);
        }

        private void ClearList()
        {
            while(content.transform.childCount > 0)
            {
                DestroyImmediate(content.transform.GetChild(0).gameObject);
            }
        }
    }
}
