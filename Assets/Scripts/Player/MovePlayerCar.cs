using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class MovePlayerCar : MonoBehaviour
    {
        private static MovePlayerCar instance = null;

        [SerializeField]
        private Vector3 speed = new Vector3();

        private new Rigidbody rigidbody = null;

        private float stopSpeed = 0.02f;

        private void Awake()
        {
            instance = this;
            rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            rigidbody.MovePosition(transform.position + (speed * Time.deltaTime));
        }

        private static void UpdateSpeed(Vector3 newSpeed)
        {
            instance.speed = newSpeed;
        }
    }
}