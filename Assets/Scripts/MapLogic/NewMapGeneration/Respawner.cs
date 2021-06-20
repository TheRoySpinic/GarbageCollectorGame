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
            
            if(prefab != null)
                Instantiate(prefab, content);
        }

        public void Respawn(float change = 1f)
        {
            Clear();

            float val = UnityEngine.Random.Range(0, 1f);

            if(val < change)
                Instantiate(prefab, content);

            //Debug.Log(string.Format("Spawn req val = {0}, change = {1}", val, change));
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