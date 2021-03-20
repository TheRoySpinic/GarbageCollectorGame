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
        /*
        [SerializeField]
        private float fromZeroAccelerationSpeed = 0.01f;
        */
        [SerializeField]
        private float accelerationSpeed = 0.001f;

        private new Rigidbody rigidbody = null;
        private Transform tr = null;

        private void Awake()
        {
            instance = this;
            tr = transform;
            rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if(!HealthManager.isAlive)
            {
                currentSpeed = Vector3.Lerp(currentSpeed, Vector3.zero, stopSpeed);
            }
            /*
            if (currentSpeed != targetSpeed)
            {
                currentSpeed = Vector3.Lerp(currentSpeed, targetSpeed, fromZeroAccelerationSpeed);
            }*/

            rigidbody.MovePosition(tr.position + (currentSpeed * Time.deltaTime));

            if(PlayerController.enableInput)
                targetSpeed.Set(targetSpeed.x + accelerationSpeed, targetSpeed.y, targetSpeed.z);
        }

        public static void SetSpeed(Vector3 newSpeed)
        {
            //instance.targetSpeed = newSpeed;
            instance.currentSpeed = newSpeed;
        }

        public static void SetZeroPosition()
        {
            instance.tr.position = Vector3.zero;
        }
    }
}
