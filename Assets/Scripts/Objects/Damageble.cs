using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class Damageble : MonoBehaviour
    {
        [SerializeField]
        private int damage = 10;

        private bool isActive = true;

        [SerializeField]
        private bool frezeWhileNoDamage = false;

        private Rigidbody rb = null;

        private void Awake()
        {
            if(frezeWhileNoDamage)
            {
                rb = GetComponent<Rigidbody>();
                rb.constraints = RigidbodyConstraints.FreezeAll;
                foreach(Rigidbody r in transform.GetComponentsInChildren<Rigidbody>())
                {
                    r.constraints = RigidbodyConstraints.FreezeAll;
                }
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (isActive && collision.gameObject.tag.Equals("Player"))
            {
                if (frezeWhileNoDamage)
                {
                    rb.constraints = RigidbodyConstraints.None;
                    foreach (Rigidbody r in transform.GetComponentsInChildren<Rigidbody>())
                    {
                        r.constraints = RigidbodyConstraints.None;
                    }
                }

                HealthManager.instance.TakeDamage(damage);
                isActive = false;
            }
        }
    }
}