using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Garage
{
    [System.Serializable]
    public class GradeData
    {
        public EGradeType gradeType = EGradeType.NONE;
        public int[] gradeCost = null;
        public float[] parameterValue = null;
    }
}