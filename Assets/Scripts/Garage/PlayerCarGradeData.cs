using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Garage
{
    [System.Serializable]
    public class PlayerCarGradeData
    {
        public ECarType activeCar = ECarType.DEFAULT;
        public CarGradePlayerData[] ownedCars = new CarGradePlayerData[1];
    }

    [System.Serializable]
    public class CarGradePlayerData
    {
        public ECarType carType = ECarType.DEFAULT;
        public GradeLevel[] grades = 
        {
            new GradeLevel() { gradeType = EGradeType.SPEED },
            new GradeLevel() { gradeType = EGradeType.ARMOR },
            new GradeLevel() { gradeType = EGradeType.SIZE }
        };
    }

    [System.Serializable]
    public class GradeLevel
    {
        public EGradeType gradeType = EGradeType.NONE;
        public int level = 0;
    }
}