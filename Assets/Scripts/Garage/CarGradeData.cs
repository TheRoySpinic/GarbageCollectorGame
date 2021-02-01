using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Garage
{
    [System.Serializable]
    public class CarGradeData
    {
        public ECarType carType = ECarType.NONE;
        int carCost = 1;
        GradeData[] grades = null;
    }
}