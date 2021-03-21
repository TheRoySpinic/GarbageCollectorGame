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
        private float stopSpeed = 0.02f;

        [SerializeField]
        private float accelerationSpeed = 0.01f;

        private new Rigidbody rigidbody = null;
        private Transform tr = null;

        private void Awake()
        {
            instance = this;
            tr = transform;
            rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if(!HealthManager.isAlive)
            {
                currentSpeed = Vector3.Lerp(currentSpeed, Vector3.zero, stopSpeed);
            }

            rigidbody.MovePosition(tr.position + (currentSpeed * Time.deltaTime));

            if(PlayerController.enableInput && HealthManager.isAlive)
                currentSpeed = new Vector3(currentSpeed.x + accelerationSpeed * Time.fixedDeltaTime, currentSpeed.y, currentSpeed.z);
        }

        public static void SetSpeed(Vector3 newSpeed, float newAccelerationSpeed = 0)
        {
            instance.currentSpeed = newSpeed;
            
            if(newAccelerationSpeed > 0)
            {
                instance.accelerationSpeed = newAccelerationSpeed;
            }
        }

        public static void SetZeroPosition()
        {
            instance.tr.position = Vector3.zero;
        }
    }
}
