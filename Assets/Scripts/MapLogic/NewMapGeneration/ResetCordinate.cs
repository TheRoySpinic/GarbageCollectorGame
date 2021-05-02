using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map.Tools
{
    public class ResetCordinate : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody rb = null;

        public void ResetCord()
        {
            transform.localPosition = Vector3.zero;
            transform.eulerAngles = Vector3.zero;

            if (rb != null)
                rb.velocity = Vector3.zero;
        }

        public void ResetX()
        {
            transform.localPosition = new Vector3(0, transform.localPosition.y, transform.localPosition.z); //Replace!!!
            
            if (rb != null)
                rb.velocity = Vector3.zero;
        }

        public void ResetY()
        {
            transform.localPosition = new Vector3(transform.localPosition.x, 0, transform.localPosition.z); //Replace!!!
            
            if (rb != null)
                rb.velocity = Vector3.zero;
        }

        public void ResetZ()
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0); //Replace!!!
            
            if (rb != null)
                rb.velocity = Vector3.zero;
        }
    }
}