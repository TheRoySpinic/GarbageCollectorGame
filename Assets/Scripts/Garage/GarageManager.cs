using Popups;
using Store;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Garage
{
    public class GarageManager : MonoBehaviour
    {
        public const string PLAYERPREFS_GRADE_PLAYERDATA_FIELD = "player_grades";

        public static GarageManager instance = null;

        public static Action E_GradeUpgrade;


        [SerializeField]
        private PlayerCarGradeData playerCarGrades = null;
        [SerializeField]
        private CarGradeConfig gradeConfig = null;

        [SerializeField]
        private GradeIcon[] gradeIcons = null;
        [SerializeField]
        private GradeText[] gradeTexts = null;

        private void Awake()
        {
            if (instance == null)
                instance = this;

            LoadPlayerGradeData();
        }

        public void SetActiveCar(ECarType carType)
        {

        }

        public void OpenCar(ECarType carType)
        {

        }

        public void GradeLevelUp(EGradeType gradeType)
        {
            if(gradeType.Equals(EGradeType.NONE))
            {
                Debug.LogWarning("Invalide gradeType");
                return;
            }

            PopupManager.ShowUpgradeConfirm(gradeType);
        }

        public void GradeLevelUp_Confirm(EGradeType gradeType)
        {
            int level = GetCurentGradeLevel(gradeType);
            int cost = GetGradeCost(gradeType, level);

            if (MasterStoreManager.instance.SubstractGold(cost))
            {
                PopupManager.HideUpgradeConfirm();
                IncrementGradeLevel(gradeType);
                SavePlayerGradeData();
                E_GradeUpgrade?.Invoke();
            }
            else
            {
                //попап неудачного улучшения (недостаточно денег)
            }
        }

        public Sprite GetGradeIcon(EGradeType gradeType)
        {
            return Array.Find(gradeIcons, (g) => { return g.gradeType.Equals(gradeType); })?.sprite;
        }

        public string GetGradeName(EGradeType gradeType)
        {
            return Array.Find(gradeTexts, (g) => { return g.gradeType.Equals(gradeType); }).name;
        }

        public string GetGradeModifyerName(EGradeType gradeType)
        {
            return Array.Find(gradeTexts, (g) => { return g.gradeType.Equals(gradeType); }).modifyerName;
        }

        public float GetGradeValue(EGradeType gradeType)
        {
            CarGradeData gradeConfig = GetCarGradeData();
            int level = GetCurentGradeLevel(gradeType);

            return Array.Find(gradeConfig.grades, (g) => { return g.gradeType.Equals(gradeType); }).parameterValue[level];
        }

        public float GetGradeValue(EGradeType gradeType, int level)
        {
            CarGradeData gradeConfig = GetCarGradeData();

            return Array.Find(gradeConfig.grades, (g) => { return g.gradeType.Equals(gradeType); }).parameterValue[level];
        }

        public int GetGradeCost(EGradeType gradeType, int level)
        {
            CarGradeData gradeConfig = GetCarGradeData();

            return Array.Find(gradeConfig.grades, (g) => { return g.gradeType.Equals(gradeType); }).gradeCost[level];
        }

        public int GetGradeCost(EGradeType gradeType)
        {
            int level = GetCurentGradeLevel(gradeType);
            CarGradeData gradeConfig = GetCarGradeData();

            return Array.Find(gradeConfig.grades, (g) => { return g.gradeType.Equals(gradeType); }).gradeCost[level];
        }

        public int GetCurentGradeLevel(EGradeType gradeType)
        {
            return Array.Find(GetActiveCarGrades().grades, (g) => { return g.gradeType.Equals(gradeType); }).level;
        }

        public CarGradePlayerData GetActiveCarGrades()
        {
            return Array.Find(playerCarGrades.ownedCars, (c) => { return c.carType.Equals(playerCarGrades.activeCar); });
        }

        public CarGradeData GetCarGradeData()
        {
            return Array.Find(gradeConfig.gradeData, (g) => { return g.carType.Equals(playerCarGrades.activeCar); });
        }


        private void IncrementGradeLevel(EGradeType gradeType)
        {
            Array.Find(GetActiveCarGrades().grades, (g) => { return g.gradeType.Equals(gradeType); }).level++;
        }

        private void SavePlayerGradeData()
        {
            PlayerPrefs.SetString(PLAYERPREFS_GRADE_PLAYERDATA_FIELD, JsonUtility.ToJson(playerCarGrades));
        }

        private bool LoadPlayerGradeData()
        {
            if (PlayerPrefs.GetString(PLAYERPREFS_GRADE_PLAYERDATA_FIELD).Length > 0)
            {
                playerCarGrades = JsonUtility.FromJson<PlayerCarGradeData>(PlayerPrefs.GetString(PLAYERPREFS_GRADE_PLAYERDATA_FIELD));
                return true;
            }
            return false;
        }


        [Serializable]
        private class GradeIcon
        {
            public EGradeType gradeType = EGradeType.NONE;
            public Sprite sprite = null;
        }

        [Serializable]
        private class GradeText
        {
            public EGradeType gradeType = EGradeType.NONE;
            public string name = "";
            public string modifyerName = "";
        }
    }
}