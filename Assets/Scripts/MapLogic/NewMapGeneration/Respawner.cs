using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map.Tools
{
    public class Respawner : MonoBehaviour
    {
        [SerializeField]
        private GameObject prefab = null;

        [SerializeField]
        private Transform content = null;

        public void Spawn()
        {
            Instantiate(prefab, content);
        }

        public void Respawn()
        {
            Clear();

            Instantiate(prefab, content);
        }

        public void Clear()
        {
            while(content.childCount > 0)
            {
                DestroyImmediate(content.GetChild(0).gameObject);
            }
        }
    }
}