using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class MoveMap : MonoBehaviour
    {
        [SerializeField]
        private Vector3 speed;

        private void Awake()
        {

        }

        private void Update()
        {
            transform.Translate(speed * Time.deltaTime);
        }
    }
}