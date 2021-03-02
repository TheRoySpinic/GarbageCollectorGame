using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class MoveCar : MonoBehaviour
    {
        [SerializeField]
        private Vector3 speed = new Vector3();

        private new Rigidbody rigidbody = null;
        private Transform tr = null;

        private void Awake()
        {
            tr = transform;
            rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            rigidbody.MovePosition(tr.position + (speed * Time.deltaTime));
        }
    }
}