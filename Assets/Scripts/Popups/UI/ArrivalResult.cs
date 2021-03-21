using System.Collections;
using System.Collections.Generic;
using Target;
using TMPro;
using UnityEngine;
using Tools;
using Arrival;

namespace Popups
{
    public class ArrivalResult : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text distance = null;
        [SerializeField]
        private GameObject newRecord = null;

        [SerializeField]
        private TMP_Text coins = null;
        [SerializeField]
        private TMP_Text containers = null;

        [SerializeField]
        private GameObject adButton = null;

        public void SetData()
        {
            distance.text = TextFormater.FormatGold((int)TargetManager.currentDistance) + "M";

            newRecord.SetActive(false);

            coins.text = "+" + TextFormater.FormatGold(TargetManager.instance.sumArrivalReward);
            containers.text = TextFormater.FormatGold(TargetManager.instance.allCount);

            adButton.SetActive(false);
        }

        public void ClickAd()
        {

        }

        public void ClickAction()
        {
            ArrivalManager.instance.EndArrival();
        }
    }
}