using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class HealthManager : MonoBehaviour
    {
        public static HealthManager instance;

        public static Action<int> E_UpdateHealth;
        public static Action E_Die;

        [SerializeField]
        private bool isTakeDamage = true;

        public static int health { get; private set; } = 100;
        public static int maxHealth { get; private set; } = 100;

        public static bool isAlive { get; private set; } = true;

        private void Awake()
        {
            instance = this;
        }

        private void OnDestroy()
        {
            if (instance.Equals(this))
                instance = null;
        }

        public void TakeDamage(int damage)
        {
            if (!isTakeDamage)
            {
                Debug.Log("[HealthManager] isTakeDamage false");
                return;
            }
            health -= damage;
            E_UpdateHealth?.Invoke(health);

            if (health <= 0)
            {
                E_Die?.Invoke();
                isAlive = false;
            }
        }

        public void SetIsTakeDamage(bool takeDamage)
        {
            isTakeDamage = takeDamage;
        }

        public void RestoreHealth(int restore)
        {
            health = health + restore > maxHealth ? maxHealth : health + restore;
            E_UpdateHealth?.Invoke(health);
        }
    }
}