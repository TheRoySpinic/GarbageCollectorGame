using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class MoveMap : MonoBehaviour
    {
        private static MoveMap instance = null;

        [SerializeField]
        private Vector3 speed = new Vector3();

        private new Rigidbody rigidbody = null;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if(speed.x != MapManager.instance.currentSpeed)
            {
                speed.x = MapManager.instance.currentSpeed;
            }

            rigidbody.MovePosition(speed * Time.fixedTime);
        }

        private static void UpdateSpeed(Vector3 newSpeed)
        {
            instance.speed = newSpeed;
        }
    }
}