using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Target
{
    public class Garbage : MonoBehaviour
    {
        public GarbageType garbageType;

        private void Update()
        {
            if(transform.position.x < TargetManager.instance.gameObject.transform.position.x - 10)
            {
                TargetManager.instance.AddAllCount();
                Destroy(gameObject);
            }
        }
    }
}
