using Arrival;
using Balance;
using Base;
using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HUD
{
    public class MenuManager : SingletonGen<MenuManager>
    {
        override public void Init()
        {
            SceneLoader.E_LoadScene -= OpenMenu;
            SceneLoader.E_LoadScene += OpenMenu;
        }

        private void OnDestroy()
        {
            SceneLoader.E_LoadScene -= OpenMenu;
        }

        public void PlayGameAction()
        {
            ArrivalManager.instance.StartArrival();
        }

        public void OpenMenu()
        {
            PlayerController.enableInput = false;
            MovePlayerCar.SetSpeed(Vector3.zero);
            ScreensManager.ShowMenuHud();
        }
    }
}
