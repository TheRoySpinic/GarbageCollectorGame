using Arrival;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Boosts.UI
{
    public class ActiveBoostFillList : MonoBehaviour
    {
        [SerializeField]
        private GameObject content = null;

        [SerializeField]
        private GameObject slotPrefab = null;

        private void Awake()
        {
            ArrivalManager.E_Start -= FillList;
            ArrivalManager.E_Start += FillList;

            ActiveBoostsManager.E_BoostListUpdate -= FillList;
            ActiveBoostsManager.E_BoostListUpdate += FillList;
        }

        private void OnDestroy()
        {
            ArrivalManager.E_Start -= FillList;

            ActiveBoostsManager.E_BoostListUpdate -= FillList;
        }

        public void FillList()
        {
            ClearList();

            foreach(Boost boost in ActiveBoostsManager.instance.GetActiveBoost())
            {
                Instantiate(slotPrefab, content.transform).GetComponent<ActiveBoostSlot>().SetData(boost.boostType, boost.timeLeft);
            }
        }

        private void ClearList()
        {
            for(int i = 0; i < content.transform.childCount; ++i)
            {
                Destroy(content.transform.GetChild(i).gameObject);
            }
        }
    }
}