using Store;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Tools;
using UnityEngine;
using UnityEngine.UI;

namespace Garage.UI
{
    public class CarGradeSlot : MonoBehaviour
    {
        [SerializeField]
        private Image icon = null;

        [SerializeField]
        private TMP_Text cost = null;
        [SerializeField]
        private TMP_Text gradeValue = null;
        [SerializeField]
        private TMP_Text gradeLevel = null;

        private EGradeType gradeType = EGradeType.NONE;

        public void SetData(EGradeType gradeType, int level)
        {
            this.gradeType = gradeType;

            CarGradeData gradeConfig = GarageManager.instance.GetCarGradeData();
            GradeData data = Array.Find(gradeConfig.grades, (g) => { return g.gradeType.Equals(gradeType); });

            gradeValue.text = GarageManager.instance.GetGradeValue(gradeType, level).ToString();
            gradeLevel.text = (level + 1) + " LVL";

            icon.sprite = GarageManager.instance.GetGradeIcon(gradeType);

            if (level < data.gradeCost.Length)
            {
                cost.text = TextFormater.FormatGold(data.gradeCost[level]);
                if(data.gradeCost[level] > MasterStoreManager.gold || 
                    !GarageManager.instance.IsOwnedCar(GarageManager.instance.GetViewCarType()))
                {
                    cost.color = Color.red;
                }
                else
                {
                    cost.color = Color.white;
                }
            }
            else
            {
                cost.text = "MAX LEVEL";
            }
        }

        public void ClickAction()
        {
            GarageManager.instance.GradeLevelUp(gradeType);
        }
    }
}
