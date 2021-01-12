using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AlphaDebuger
{
    public class PinCodeChecker : MonoBehaviour
    {
        [SerializeField]
        private string pincode = "1001";
        private string enterCode = "";

        [SerializeField]
        private Text text = null;

        [SerializeField]
        private GameObject canvas = null;

        private void Awake()
        {
            /*if (!(Application.genuine && Application.genuineCheckAvailable))
            {
                Debug.Log("genuineCheck false. destroy");
                Destroy(gameObject);
            }*/
            SetText();
        }

        public void AddItem(string s)
        {
            enterCode = enterCode + s;
            SetText();
        }

        public void ClearPin()
        {
            enterCode = "";
            SetText();
        }

        public void CheckPinCode()
        {
            /*if (Application.genuine && Application.genuineCheckAvailable)
            {*/
                if (enterCode.Equals(pincode))
                {
                    canvas.SetActive(true);
                    gameObject.SetActive(false);
                }
            //}
        }

        private void SetText()
        {
            string s = "";
            for (int i = 0; i < enterCode.Length; i++)
            {
                s += "X";
            }
            text.text = s;
        }
    }
}