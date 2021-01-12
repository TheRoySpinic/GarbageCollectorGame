using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlphaDebuger
{
    public class PanelButton : MonoBehaviour
    {
        [SerializeField]
        private ECanvasType type = ECanvasType.LOGER;

        public void ClickAction()
        {
            CanvasSwitcher.instance.OpenCanvas(type);
        }
    }
}
