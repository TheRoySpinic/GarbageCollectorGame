using Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Popups
{
    public class ConnectInfo : MonoBehaviour
    {
        public void ClickAction()
        {
            if(Application.internetReachability == NetworkReachability.NotReachable)
            {
                return;
            }

            PopupManager.HideConnection();
            if(LoadScene.instance != null)
                LoadScene.instance.Load();
        }
    }
}