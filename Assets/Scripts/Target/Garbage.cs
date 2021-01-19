using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Target
{
    public class Garbage : MonoBehaviour
    {
        public GarbageType garbageType;

        public bool isActive = true;

        private void Update()
        {
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
            StartCoroutine(Hide());
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
