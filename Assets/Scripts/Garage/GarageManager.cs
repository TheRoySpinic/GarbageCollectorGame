using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Garage
{
    public class GarageManager : MonoBehaviour
    {
        public const string PLAYERPREFS_GRADE_PLAYERDATA_FIELD = "player_grades";

        public static GarageManager instance = null;


        [SerializeField]
        private PlayerCarGradeData playerCarGrades = null;

        private void Awake()
        {
            if (instance == null)
                instance = this;

            LoadPlayerGradeData();
        }

        private void SetActiveCar(ECarType carType)
        {

        }

        private void OpenCar(ECarType carType)
        {

        }

        private void GradeLevelUp(EGradeType gradeType, ECarType currentCarType)
        {

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


        [System.Serializable]
        private class PlayerCarGradeData
        {
            public ECarType activeCar = ECarType.DEFAULT;
            public CarGradeData[] ownedCars = new CarGradeData[1];
        }

        [System.Serializable]
        private class CarGradeData
        {
            public ECarType carType = ECarType.DEFAULT;
            public GradeLevel[] grades = null;
        }

        [System.Serializable]
        private class GradeLevel
        {
            public EGradeType gradeType = EGradeType.NONE;
            public int level = 0;
        }
    }
}