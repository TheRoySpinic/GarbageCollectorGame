using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Design
{
    public class HideEffect : MonoBehaviour
    {
        [SerializeField]
        private float effectSpeed = 0.05f;

        public void Hide()
        {
            StartCoroutine(CHide());
        }

        private IEnumerator CHide()
        {
            while (transform.localScale.x - effectSpeed > 0)
            {
                transform.localScale = new Vector3(transform.localScale.x - effectSpeed, transform.localScale.y - effectSpeed, transform.localScale.z - effectSpeed);
                yield return new WaitForEndOfFrame();
            }
            Destroy(gameObject);
        }
    }
}