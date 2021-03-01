using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Garage
{
    [Serializable]
    public class PlayerCarsData
    {
        public ECarType activeCar = ECarType.DEFAULT;
        public List<CarGradePlayerData> ownedCars = new List<CarGradePlayerData>();
    }

    [Serializable]
    public class CarGradePlayerData
    {
        public ECarType carType = ECarType.DEFAULT;
        public int colorIndex = 0;
        public GradeLevel[] grades = 
        {
            new GradeLevel() { gradeType = EGradeType.SPEED },
            new GradeLevel() { gradeType = EGradeType.ARMOR },
            new GradeLevel() { gradeType = EGradeType.SIZE }
        };
        public bool[] colors = { true };
    }

    [Serializable]
    public class GradeLevel
    {
        public EGradeType gradeType = EGradeType.NONE;
        public int level = 0;
    }
}