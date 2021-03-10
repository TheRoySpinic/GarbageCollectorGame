using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class CameraShake : MonoBehaviour
    {
        [SerializeField]
        private Animator animator = null;

        private void Awake()
        {
            HealthManager.E_Damage -= Shake;
            HealthManager.E_Damage += Shake;
        }

        private void OnDestroy()
        {
            HealthManager.E_Damage -= Shake;
        }

        private void Shake()
        {
            animator.SetTrigger("Shake");
            StartCoroutine(CShake());
        }

        private IEnumerator CShake()
        {
            yield return new WaitForEndOfFrame();
            animator.ResetTrigger("Shake");
        }
    }
}