using Garage.Car;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class CarMesh : MonoBehaviour
    {
        [HideInInspector]
        public CarColors carColors = null;

        private void Awake()
        {
            carColors = GetComponent<CarColors>();
        }
    }
}