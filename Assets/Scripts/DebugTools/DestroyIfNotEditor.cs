using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DebugTools
{
    public class DestroyIfNotEditor : MonoBehaviour
    {
        private void Awake()
        {
#if !UNITY_EDITOR
        Destroy(gameObject);
#endif
        }
    }
}