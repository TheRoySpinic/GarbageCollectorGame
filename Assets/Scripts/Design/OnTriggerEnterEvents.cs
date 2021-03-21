using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Design
{
    public class OnTriggerEnterEvents : MonoBehaviour
    {
        [Header("Check tag other object (if clear use all tags)")]
        [SerializeField]
        private string onlyTag = "";

        [SerializeField]
        private UnityEvent unityEvent = new UnityEvent();

        private void OnTriggerEnter(Collider other)
        {
            if(onlyTag.Equals("") || other.gameObject.tag.Equals(onlyTag))
            {
                unityEvent?.Invoke();
            }
        }
    }
}