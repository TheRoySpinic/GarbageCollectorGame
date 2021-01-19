using Balance;
using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HUD
{
    public class MenuManager : MonoBehaviour
    {
        public static MenuManager instance;

        private void Awake()
        {
            if (instance == null)
                instance = this;

            Base.SceneLoader.E_LoadScene -= OpenMenu;
            Base.SceneLoader.E_LoadScene += OpenMenu;
        }

        private void OnDestroy()
        {
            if (instance.Equals(this))
                instance = null;

            Base.SceneLoader.E_LoadScene -= OpenMenu;
        }

        public void PlayGameAction()
        {
            ScreensManager.ShowGameHud();
            PlayerController.enableInput = true;
            MovePlayerCar.SetSpeed(GameBalance.GetPlayerBalance().startSpeed);
        }

        private void OpenMenu()
        {
            PlayerController.enableInput = false;
            MovePlayerCar.SetSpeed(Vector3.zero);
            ScreensManager.ShowMenuHud();
        }
    }
}