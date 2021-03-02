using Garage;
using Store;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Tools;
using UnityEngine;
using UnityEngine.UI;

namespace Popups
{
    public class CarGradeLevelUp_Confirm : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text textName = null;
        [SerializeField]
        private TMP_Text modifierName = null;
        [SerializeField]
        private TMP_Text modifierCount = null;
        [SerializeField]
        private TMP_Text modifierCountAdd = null;
        [SerializeField]
        private TMP_Text needGold = null;
        [SerializeField]
        private TMP_Text costText = null;

        [SerializeField]
        private Image icon = null;

        private EGradeType gradeType = EGradeType.NONE;

        public void SetData(EGradeType gradeType)
        {
            this.gradeType = gradeType;

            costText.text = TextFormater.FormatGold(GarageManager.instance.GetGradeCost(gradeType));

            textName.text = GarageManager.instance.GetGradeName(gradeType);
            modifierName.text = GarageManager.instance.GetGradeModifyerName(gradeType);

            modifierCount.text = GarageManager.instance.GetGradeValue(gradeType).ToString();
            modifierCountAdd.text = "+" + (GarageManager.instance.GetGradeValue(gradeType, GarageManager.instance.GetCurentGradeLevel(gradeType) + 1) - GarageManager.instance.GetGradeValue(gradeType));

            icon.sprite = GarageManager.instance.GetGradeIcon(gradeType);

            int cost = GarageManager.instance.GetGradeCost(gradeType);

            costText.color = TextFormater.GetCostColor(cost);

            if(cost > MasterStoreManager.gold)
            {
                needGold.gameObject.SetActive(true);
                needGold.text = "Need another: " + TextFormater.FormatGold(GarageManager.instance.GetGradeCost(gradeType) - MasterStoreManager.gold);

            }
            else
            {
                needGold.gameObject.SetActive(false);
            }
        }

        public void ClickAction()
        {
            GarageManager.instance.GradeLevelUp_Confirm(gradeType);
        }

        public void HidePopup()
        {
            PopupManager.HideUpgradeConfirm();
        }
    }
}