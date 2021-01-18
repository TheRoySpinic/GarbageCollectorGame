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

        private void Awake()
        {
            if(oneFrom.Length > 0)
            foreach(DesignObjectOneFrom one in oneFrom)
            {
                foreach (GameObject o in one.objects)
                {
                    o.SetActive(false);
                }

                int index = UnityEngine.Random.Range(0, one.objects.Length - 1);

                one.objects[index].SetActive(true);
            }

            if(betvenTwo.Length > 0)
            foreach(DesignObjectBetvenTwo o in betvenTwo)
            {
                o.A.SetActive(false);
                o.B.SetActive(false);

                float val = UnityEngine.Random.Range(0, 1f);

                if (val < o.change_A)
                    o.A.SetActive(true);
                else if (val < (o.change_A + o.change_B))
                    o.B.SetActive(true);
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