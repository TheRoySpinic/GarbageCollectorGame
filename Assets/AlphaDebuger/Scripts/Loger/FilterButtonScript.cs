using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AlphaDebuger
{
    public class FilterButtonScript : MonoBehaviour
    {
        [SerializeField]
        private LogType type = LogType.Log;

        private void Awake()
        {
            SetColor();
        }

        public void ClickAction()
        {
            LogController.instance.FilterClick(type);
            SetColor();
        }

        private void SetColor()
        {
            if(LogController.instance.GetFilterState(type))
            {
                gameObject.GetComponent<Image>().color = Color.white;
            }
            else
            {
                gameObject.GetComponent<Image>().color = Color.gray;
            }
        }
    }
}
