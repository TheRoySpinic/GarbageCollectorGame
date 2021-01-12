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
        
        private void Update()
        {
            if(speed.x != -MapManager.instance.currentSpeed)
            {
                speed.x = -MapManager.instance.currentSpeed;
            }

            transform.Translate(speed * Time.deltaTime);
        }

        private static void UpdateSpeed(Vector3 newSpeed)
        {
            instance.speed = newSpeed;
        }
    }
}