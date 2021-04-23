using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map.Generate.ParameterLogic
{
    public class GarbageSpawner : BaseParameterLogic
    {
        [SerializeField]
        private GameObject prefab_small = null;
        [SerializeField]
        private GameObject prefab_big = null;
        [SerializeField]
        private Transform[] spawnPoints = new Transform[0];

        public override void ParameterAction(int parameter)
        {
            base.ParameterAction(parameter);

            if (parameter == 0)
                return;

            //Говнокод, нет времени и сил(!!!) думать на нормальным решением, релиз я планировал месяц назад

            switch(parameter)
            {
                case 1:
                    Instantiate(prefab_small, spawnPoints[0]);
                    break;
                case 2:
                    Instantiate(prefab_small, spawnPoints[0]);
                    Instantiate(prefab_small, spawnPoints[1]);
                    break;
                case 3:
                    Instantiate(prefab_small, spawnPoints[0]);
                    Instantiate(prefab_small, spawnPoints[1]);
                    Instantiate(prefab_small, spawnPoints[2]);
                    break;
                case 4:
                    Instantiate(prefab_small, spawnPoints[0]);
                    Instantiate(prefab_small, spawnPoints[1]);
                    Instantiate(prefab_small, spawnPoints[2]);
                    Instantiate(prefab_small, spawnPoints[3]);
                    break;
                case 5:
                    Instantiate(prefab_big, spawnPoints[1]);
                    break;
                case 6:
                    Instantiate(prefab_big, spawnPoints[1]);
                    Instantiate(prefab_big, spawnPoints[3]);
                    break;
            }
        }

        public override void ResetParameter(int lastParameter)
        {
            base.ResetParameter(lastParameter);

            if (lastParameter == 0)
                return;

            foreach(Transform t in spawnPoints)
            {
                while (t.childCount > 0)
                    DestroyImmediate(t.GetChild(0).gameObject);
            }
        }
    }
}