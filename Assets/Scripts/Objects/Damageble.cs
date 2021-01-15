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

        private void OnCollisionEnter(Collision collision)
        {
            if (isActive && collision.gameObject.tag.Equals("Player"))
            {
                HealthManager.instance.TakeDamage(damage);
                isActive = false;
            }
        }
    }
}