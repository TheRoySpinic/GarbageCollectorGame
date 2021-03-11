using Balance;
using Base;
using Player;
using Popups;
using Store;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Garage
{
    public class GarageManager : SingletonGen<GarageManager>
    {
        public const string PLAYERPREFS_GRADE_PLAYERDATA_FIELD = "player_grades";
        
        public static Action E_GarageManagerReady;

        public static Action E_GradeUpgrade;
        public static Action E_ColorUpgrade;
        public static Action E_CarMeshUpgrade;
        public static Action E_ColorUpgrade_Fail;
        public static Action<ECarType> E_ViewCarUpdate;
        public static Action<ECarType> E_ActiveCarUpdate;

        [SerializeField]
        private CarMesh carMesh = null;
        [SerializeField]
        private GameObject carSlot = null;

        [Header("Grade parameter")]
        [SerializeField]
        private PlayerCarsData playerCarGrades = null;
        [SerializeField]
        private CarGradeConfig gradeConfig = null;

        [SerializeField]
        private GradeIcon[] gradeIcons = null;
        [SerializeField]
        private GradeText[] gradeTexts = null;

        [Header("Cars")]
        [SerializeField]
        private CarPrefab[] carPrefabs = null;

        private ECarType viewCar = ECarType.NONE;

        override public void Init()
        {
            gradeConfig = GameBalance.GetCarGradeConfig();

            LoadPlayerGradeData();

            if(carMesh == null)
            {
                Debug.Log("Attempt to find CarMesh...");
                carMesh = carSlot.transform.GetChild(0).GetComponent<CarMesh>();
                if(carMesh != null)
                {
                    Debug.Log("Find CarMesh");
                }
                else
                {
                    Debug.LogError("Find CarMesh failed!!!");
                }
            }

            viewCar = playerCarGrades.activeCar;

            E_GarageManagerReady?.Invoke();
        }

        public void SetActiveCar(ECarType carType)
        {
            if (IsOwnedCar(carType))
            {
                playerCarGrades.activeCar = carType;
                SavePlayerGradeData();
                E_ActiveCarUpdate?.Invoke(carType);
            }

            viewCar = carType;
            E_ViewCarUpdate?.Invoke(carType);
        }

        public void OpenCar(ECarType carType)
        {
            if(IsOwnedCar(carType))
            {
                E_ViewCarUpdate?.Invoke(carType);
                return;
            }

            if(MasterStoreManager.instance.SubstractGold(GetCarCost(carType)))
            {
                playerCarGrades.ownedCars.Add(new CarGradePlayerData(){ carType = carType });
                SetActiveCar(carType);
                SavePlayerGradeData();
            }
            else
            {
                //попап "недостаточно денег"
            }

            E_ViewCarUpdate?.Invoke(carType);
        }
        
        public int GetActiveCarHealth()
        {
            return GetCarGradeData().baseCarHealth + (int) GetGradeValue(EGradeType.ARMOR);
        }
        
        public float GetActiveCarSpeed()
        {
            return GetCarGradeData().baseCarSpeed + GetGradeValue(EGradeType.SPEED);
        }

        public ECarType GetActiveCarType()
        {
            return playerCarGrades.activeCar;
        }
        
        public ECarType GetViewCarType()
        {
            return viewCar;
        }

        public int GetActiveCarColorIndex()
        {
            if(GetPlayerCarGrades() != null)
                return GetPlayerCarGrades().colorIndex;
            return 0;
        }

        public void ColorClickAction(int index)
        {
            if(GetActiveCarColorIndex() != index)
                if (!SetCarColor(index))
                    OpenColor(index);
        }

        public GameObject GetCarSlot()
        {
            return carSlot;
        }

        public CarMesh GetCarMesh()
        {
            return carMesh;
        }

        public void SetCarMesh(CarMesh carMesh)
        {
            this.carMesh = carMesh;
            E_CarMeshUpgrade?.Invoke();
        }

        public CarPrefab[] GetCarPrefabs()
        {
            return carPrefabs;
        }

        public GameObject GetCarPrefab(ECarType carType)
        {
            return Array.Find(carPrefabs, (p) => { return p.carType.Equals(carType); }).prefab;
        }

        public void GradeLevelUp(EGradeType gradeType)
        {
            if (!IsOwnedCar(viewCar))
            {
                // инфо-попап "в начале необходимо приобрести машину"
                return;
            }

            if(gradeType.Equals(EGradeType.NONE))
            {
                Debug.LogError("Invalide gradeType");
                return;
            }
            
            if(GetCurentGradeLevel(gradeType) < Array.Find(GetCarGradeData().grades, (g) => { return g.gradeType.Equals(gradeType); }).gradeCost.Length)
            {
                PopupManager.ShowUpgradeConfirm(gradeType);
            }
            else
            {
                //попап "максимальный уровень"
            }
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

        public bool IsOwnedCar(ECarType carType)
        {
            return Array.Find(playerCarGrades.ownedCars.ToArray(), (o) => { return o.carType.Equals(carType); }) != null;
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

        public int GetCarCost(ECarType carType)
        {
            return gradeConfig.GetCarData(carType).carCost;
        }

        public int GetCurentGradeLevel(EGradeType gradeType)
        {
            return Array.Find(GetPlayerCarGrades().grades, (g) => { return g.gradeType.Equals(gradeType); }).level;
        }

        public CarGradePlayerData GetPlayerCarGrades()
        {
            return Array.Find(playerCarGrades.ownedCars.ToArray(), (c) => { return c.carType.Equals(viewCar); });
        }

        public CarGradeData GetCarGradeData()
        {
            return gradeConfig.GetCarData(viewCar);
        }

        public CarGradeData GetCarGradeData(ECarType carType)
        {
            return gradeConfig.GetCarData(carType);
        }

        public int GetColorCost(int index)
        {
            CarGradeData data = GetCarGradeData();
            return data.colorsCost[index];
        }

        public int GetColorCost(ECarType carType, int index)
        {
            CarGradeData data = GetCarGradeData(carType);
            return data.colorsCost[index];
        }

        public bool IsOwnedColor(int index)
        {
            CarGradePlayerData playerData = GetPlayerCarGrades();
            if (playerData == null || playerData.colors.Length <= index)
                return false;
            return playerData.colors[index];
        }

        
        private bool SetCarColor(int index)
        {
            if (index >= 0 && index < GetPlayerCarGrades().colors.Length && GetPlayerCarGrades().colors[index])
            {
                GetPlayerCarGrades().colorIndex = index;
                SavePlayerGradeData();
                E_ColorUpgrade?.Invoke();
                return true;
            }
            return false;
        }

        private void OpenColor(int index)
        {
            if (index < 0)
            {
                return;
            }

            CarGradePlayerData playerData = GetPlayerCarGrades();

            CarGradeData data = GetCarGradeData();

            if(index > playerData.colors.Length - 1)
            {
                if(index > data.colorsCost.Length - 1)
                {
                    E_ColorUpgrade_Fail?.Invoke();
                    return;
                }

                bool[] newColors = new bool[data.colorsCost.Length];

                for(int i = 0; i < playerData.colors.Length; i++)
                {
                    newColors[i] = playerData.colors[i];
                }

                playerData.colors = newColors;
            }

            if(!playerData.colors[index])
            {
                if(MasterStoreManager.instance.SubstractGold(data.colorsCost[index]))
                {
                    playerData.colors[index] = true;
                    if(!SetCarColor(index))
                        SavePlayerGradeData();
                }
                else
                {
                    //Попап неудачной покупки "недостаточно денег"
                }
            }
        }

        private void IncrementGradeLevel(EGradeType gradeType)
        {
            Array.Find(GetPlayerCarGrades().grades, (g) => { return g.gradeType.Equals(gradeType); }).level++;
        }

        private void SavePlayerGradeData()
        {
            PlayerPrefs.SetString(PLAYERPREFS_GRADE_PLAYERDATA_FIELD, JsonUtility.ToJson(playerCarGrades));
        }

        private bool LoadPlayerGradeData()
        {
            if (PlayerPrefs.GetString(PLAYERPREFS_GRADE_PLAYERDATA_FIELD).Length > 0)
            {
                playerCarGrades = JsonUtility.FromJson<PlayerCarsData>(PlayerPrefs.GetString(PLAYERPREFS_GRADE_PLAYERDATA_FIELD));
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

        [Serializable]
        public class CarPrefab
        {
            public ECarType carType = ECarType.NONE;
            public GameObject prefab = null;
        }
    }
}