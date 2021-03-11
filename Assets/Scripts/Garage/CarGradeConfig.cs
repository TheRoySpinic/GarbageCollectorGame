using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Garage
{
    [System.Serializable]
    public class CarGradeConfig
    {
        public CarGradeData[] gradeData = null;

        public CarGradeData GetCarData(ECarType carType)
        {
            return Array.Find(gradeData, (d) => { return d.carType.Equals(carType); });
        }
    }
}
