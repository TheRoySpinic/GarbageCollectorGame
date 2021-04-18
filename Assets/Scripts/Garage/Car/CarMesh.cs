using Boosts;
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
        [HideInInspector]
        public BoostEffects boostEffects = null;

        private void Awake()
        {
            carColors = GetComponent<CarColors>();
            boostEffects = GetComponent<BoostEffects>();
        }
    }
}