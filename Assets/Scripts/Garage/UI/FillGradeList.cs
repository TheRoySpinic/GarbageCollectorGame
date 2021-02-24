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
        }

        private void OnDestroy()
        {
            ScreensManager.E_ShowGarage -= FillList;
            GarageManager.E_GradeUpgrade -= FillList;
        }

        private void FillList()
        {
            ClearList();

            Instantiate(spacer, content.transform);

            CarGradePlayerData carGrade = GarageManager.instance.GetActiveCarGrades();
            foreach(GradeLevel level in carGrade.grades)
            {
                Instantiate(slot, content.transform).GetComponent<CarGradeSlot>().SetData(level.gradeType, level.level);
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
