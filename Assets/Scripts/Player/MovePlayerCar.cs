using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class MovePlayerCar : MonoBehaviour
    {
        private static MovePlayerCar instance = null;

        [SerializeField]
        private Vector3 currentSpeed = new Vector3();

        [SerializeField]
        private Vector3 targetSpeed = new Vector3();

        [SerializeField]
        private float stopSpeed = 0.02f;

        [SerializeField]
        private float fromZeroAccelerationSpeed = 0.01f;

        [SerializeField]
        private float accelerationSpeed = 0.001f;

        private new Rigidbody rigidbody = null;

        private void Awake()
        {
            instance = this;
            rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if(!HealthManager.isAlive)
            {
                currentSpeed = Vector3.Lerp(currentSpeed, Vector3.zero, stopSpeed);
            }
            else if (currentSpeed != targetSpeed)
            {
                currentSpeed = Vector3.Lerp(currentSpeed, targetSpeed, fromZeroAccelerationSpeed);
            }

            rigidbody.MovePosition(transform.position + (currentSpeed * Time.deltaTime));

            if(PlayerController.enableInput)
                currentSpeed.Set(currentSpeed.x + accelerationSpeed, currentSpeed.y, currentSpeed.z);
        }

        public static void SetSpeed(Vector3 newSpeed)
        {
            instance.targetSpeed = newSpeed;
        }
    }
}
