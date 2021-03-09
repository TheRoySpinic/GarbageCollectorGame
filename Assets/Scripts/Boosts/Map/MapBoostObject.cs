using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Boosts.Map
{
    public class MapBoostObject : MonoBehaviour
    {
        [SerializeField]
        private EBoostType boostType = EBoostType.NONE;

        [HideInInspector]
        public bool isActive = true;

        [SerializeField]
        private GameObject[] particles = null;

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.CompareTag("Player"))
            {
                ActiveBoostsManager.instance.AddBoost(boostType);
                HideEffect();
            }
        }

        public void HideEffect()
        {
            isActive = false;
            foreach (GameObject o in particles)
            {
                o.SetActive(true);
                o.transform.SetParent(transform.parent);
            }

            StartCoroutine(Hide());
        }

        private IEnumerator Hide()
        {
            float a = 0.05f;

            while (transform.localScale.x - a > 0)
            {
                transform.localScale = new Vector3(transform.localScale.x - a, transform.localScale.y - a, transform.localScale.z - a);
                yield return new WaitForEndOfFrame();
            }
            Destroy(gameObject);
        }
    }
}