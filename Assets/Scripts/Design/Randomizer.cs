using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Design
{
    public class Randomizer : MonoBehaviour
    {
        [SerializeField]
        private DesignObjectOneFrom[] oneFrom = null;
        [SerializeField]
        private DesignObjectBetvenTwo[] betvenTwo =  null;

        [SerializeField]
        private GameObject content = null;

        private void Awake()
        {
            Randomize();
        }

        public void Randomize()
        {
            while(content.transform.childCount > 0)
            {
                DestroyImmediate(content.transform.GetChild(0).gameObject);
            }

            if(oneFrom.Length > 0)
            foreach(DesignObjectOneFrom one in oneFrom)
            {
                int index = UnityEngine.Random.Range(0, one.objects.Length);
                    Instantiate(one.objects[index], content.transform);
            }

            if(betvenTwo.Length > 0)
            foreach(DesignObjectBetvenTwo o in betvenTwo)
            {
                float val = UnityEngine.Random.Range(0, 1f);

                if (val < o.change_A)
                    Instantiate(o.A, content.transform);
                else if (val < (o.change_A + o.change_B))
                    Instantiate(o.B, content.transform);
            }
        }

        [Serializable]
        private class DesignObjectOneFrom
        {
            public string name = "";
            public GameObject[] objects = null;
        }

        [Serializable]
        private class DesignObjectBetvenTwo
        {
            public GameObject A = null;
            [Range(0,1f)]
            public float change_A = 0;
            public GameObject B = null;
            [Range(0,1f)]
            public float change_B = 0;
        }
    }
}