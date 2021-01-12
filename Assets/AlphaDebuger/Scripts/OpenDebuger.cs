using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlphaDebuger
{
    public class OpenDebuger : MonoBehaviour
    {
        public static OpenDebuger instance;

        [SerializeField]
        private List<bool> checks = new List<bool>();

        [SerializeField]
        private GameObject passwordCanvas = null;

        private void Awake()
        {
            if(!instance)
                instance = this;
            else
            {
                Debug.Log("[OpenDebuger] Instance is set. destroy " + gameObject.name);
                Destroy(gameObject);
            }
        }

        public void SetCheck(int index)
        {
            if (checks[index])
            {
                ClearChecks();
            }

            if (index == 0)
            {
                checks[index] = true;
            }
            else if (!checks[index - 1])
            {
                ClearChecks();
            }
            else
            {
                checks[index] = true;
            }
            Validate();
        }

        private void Validate()
        {
            if (Check())
            {
                passwordCanvas.SetActive(true);
                ClearChecks();
            }
        }

        private bool Check()
        {
            foreach (bool b in checks)
            {
                if (!b) return false;
            }
            return true;
        }

        private void ClearChecks()
        {
            for (int i = 0; i < checks.Count; i++)
            {
                checks[i] = false;
            }
        }
    }
}
