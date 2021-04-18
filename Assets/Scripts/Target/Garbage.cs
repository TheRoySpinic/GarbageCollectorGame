using Boosts;
using Map;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Target
{
    public class Garbage : MonoBehaviour
    {
        public GarbageType garbageType;
        [HideInInspector]
        public bool isActive = true;

        private BoxCollider triger = null;
        private Vector3 baseTrigerSize = Vector3.one;

        [SerializeField]
        private GameObject[] particles = null;

        private void Awake()
        {
            foreach (GameObject o in particles)
            {
                o.SetActive(false);
            }
            triger = GetComponent<BoxCollider>();
            baseTrigerSize = triger.size;

            ActiveBoostsManager.E_AddBoost -= BoostActivate;
            ActiveBoostsManager.E_AddBoost += BoostActivate;

            ActiveBoostsManager.E_BoostDeactivation -= BoostDeactivate;
            ActiveBoostsManager.E_BoostDeactivation += BoostDeactivate;
        }

        private void OnDestroy()
        {
            ActiveBoostsManager.E_AddBoost -= BoostActivate;
            ActiveBoostsManager.E_BoostDeactivation -= BoostDeactivate;
        }

        private void Update()
        {
            if(TargetManager.instance == null)
            {
                Debug.LogError("TargetManager is null!!!");
                return;
            }
            if(transform.position.x < TargetManager.instance.gameObject.transform.position.x - 15)
            {
                TargetManager.instance.AddAllCount();
                isActive = false;
                Destroy(gameObject);
            }
        }

        public void HideEffect()
        {
            isActive = false;
            foreach(GameObject o in particles)
            {
                o.SetActive(true);
                o.transform.SetParent(transform.parent);
            }

            StartCoroutine(Hide());
        }

        private void BoostActivate(EBoostType boostType)
        {
            if (boostType.Equals(EBoostType.MAGNET) && triger != null)
            {
                triger.size *= 5;
            }
        }

        private void BoostDeactivate(EBoostType boostType)
        {
            if(boostType.Equals(EBoostType.MAGNET) && triger != null)
            {
                triger.size = baseTrigerSize;
            }
        }

        private IEnumerator Hide()
        {
            float a = 0.05f;

            while(transform.localScale.x - a > 0)
            {
                transform.localScale = new Vector3(transform.localScale.x - a, transform.localScale.y - a, transform.localScale.z - a);
                yield return new WaitForEndOfFrame();
            }
            Destroy(gameObject);
        }
    }
}
