using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Boosts
{
    public class BoostEffects : MonoBehaviour
    {
        [SerializeField]
        private GameObject shield = null;

        public void ShowShield()
        {
            if (shield != null)
                shield.SetActive(true);
        }

        public void HideShield()
        {
            if (shield != null)
                shield.SetActive(false);
        }
    }
}
