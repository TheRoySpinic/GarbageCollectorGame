using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlphaDebuger
{
    public class OpenDebugerButton : MonoBehaviour
    {
        [SerializeField]
        private int index;

        public void ClickAction()
        {
            OpenDebuger.instance.SetCheck(index);
        }
    }
}
