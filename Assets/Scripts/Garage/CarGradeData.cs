using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Garage
{
    [System.Serializable]
    public class CarGradeData
    {
        public ECarType carType = ECarType.NONE;
        public int carCost = 1;
        public GradeData[] grades = null;
        public int[] colorsCost = { 0 };
        public int baseCarHealth = 100;
        public float baseCarSpeed = 10;
        public float baseCarSize = 10;
    }
}