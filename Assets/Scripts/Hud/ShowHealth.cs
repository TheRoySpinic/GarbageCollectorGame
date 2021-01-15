using Player;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace HUD
{
    public class ShowHealth : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text text = null;

        private int maxHealth = 100;

        private void Awake()
        {
            HealthManager.E_UpdateHealth -= UpdateHealth;
            HealthManager.E_UpdateHealth += UpdateHealth;

            UpdateHealth(HealthManager.health);
        }

        private void OnDestroy()
        {
            HealthManager.E_UpdateHealth -= UpdateHealth;
        }

        private void UpdateHealth(int newHealth)
        {
            text.text = newHealth + "/" + maxHealth;
        }
    }
}