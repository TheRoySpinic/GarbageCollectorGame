using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class CameraFolow : MonoBehaviour
    {
        [SerializeField]
        private GameObject playerCar = null;

        [SerializeField]
        private float speed = 5f;
        [Header("Clamp camera position")]
        [SerializeField]
        private float maxLeft = 5;
        [SerializeField]
        private float maxRight = -5;

        private Vector3 newPosition = new Vector3();

        void Update()
        {
            if (transform.position.z != playerCar.transform.position.z && transform.position.z > maxRight && transform.position.z < maxLeft)
            {
                newPosition.Set(transform.position.x, transform.position.y, Mathf.Clamp(playerCar.transform.position.z, maxRight, maxLeft));
                transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * speed);
            }
        }
    }
}