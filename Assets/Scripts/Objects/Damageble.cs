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
            Setup();
        }

        public void Setup()
        {
            if(frezeWhileNoDamage)
            {
                rb = GetComponent<Rigidbody>();
                if (rb == null)
                    return;
                rb.constraints = RigidbodyConstraints.FreezeAll;
                foreach(Rigidbody r in transform.GetComponentsInChildren<Rigidbody>())
                {
                    r.constraints = RigidbodyConstraints.FreezeAll;
                }
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (isActive && collision.gameObject.CompareTag("Player"))
            {
                if (frezeWhileNoDamage)
                {
                    if (rb != null)
                    {
                        rb.constraints = RigidbodyConstraints.None;
                        foreach (Rigidbody r in transform.GetComponentsInChildren<Rigidbody>())
                        {
                            r.constraints = RigidbodyConstraints.None;
                        }
                    }
                }

                HealthManager.instance.TakeDamage(damage);
                isActive = false;
            }
        }
    }
}