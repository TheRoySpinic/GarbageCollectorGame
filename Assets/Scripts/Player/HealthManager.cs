using Arrival;
using Garage;
using Popups;
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
        public static Action E_Damage;
        public static Action E_Die;

        [SerializeField]
        private bool isTakeDamage = true;

        [SerializeField]
        private AudioSource audioSource = null;

        public static int health { get; private set; } = 100;
        public static int maxHealth { get; private set; } = 100;

        public static bool isAlive { get; private set; } = true;

        private void Awake()
        {
            instance = this;

            ArrivalManager.E_Start -= SetCarHealth;
            ArrivalManager.E_Start += SetCarHealth;
        }

        private void OnDestroy()
        {
            if (instance.Equals(this))
                instance = null;

            ArrivalManager.E_Start -= SetCarHealth;
        }


        public void TakeDamage(int damage)
        {
            if (!isTakeDamage)
            {
                Debug.Log("[HealthManager] isTakeDamage false");
                return;
            }
            health -= damage;

            if(audioSource != null)
            {
                audioSource.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
                audioSource.Play();
            }

            E_Damage?.Invoke();
            E_UpdateHealth?.Invoke(health);

            if (health <= 0)
            {
                E_Die?.Invoke();
                isAlive = false;
                PopupManager.ShowArrivalResult();
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

        public void ResetHealth()
        {
            health = maxHealth;
            isAlive = true;
            E_UpdateHealth?.Invoke(health);
        }


        private void SetCarHealth()
        {
            maxHealth = GarageManager.instance.GetActiveCarHealth();
            ResetHealth();
        }
    }
}