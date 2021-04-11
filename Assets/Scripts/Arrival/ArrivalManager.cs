using Balance;
using Base;
using Boosts;
using HUD;
using Map;
using Map.Generate;
using Player;
using Popups;
using System;
using System.Collections;
using System.Collections.Generic;
using Target;
using UnityEngine;

namespace Arrival
{
    public class ArrivalManager : SingletonGen<ArrivalManager>
    {
        public static Action E_Start;
        public static Action E_End;

        [SerializeField]
        private Camera gameCamera = null;
        [SerializeField]
        private Camera menuCamera = null;

        public override void Init()
        {
            SceneLoader.E_LoadScene -= BaseState;
            SceneLoader.E_LoadScene += BaseState;
        }

        public override void Destroy()
        {
            SceneLoader.E_LoadScene -= BaseState;
        }

        public void StartArrival()
        {
            StartCoroutine(CStart());
            E_Start?.Invoke();
        }

        public void EndArrival()
        {
            StartCoroutine(CEnd());
            E_End?.Invoke();
        }


        private void BaseState()
        {
            HealthManager.instance.ResetHealth();
            ActiveBoostsManager.instance.DisableAllBoost();
            PlayerController.instance.SetCarBasePosition();
            MapGenerator.instance.ResetMap(EBiomeType.PREVIEW);
            ScreensManager.ShowMenuHud();
            PlayerController.enableInput = false;
            MovePlayerCar.SetSpeed(GameBalance.GetPlayerBalance().previewSpeed);
            MovePlayerCar.SetZeroPosition();
        }


        private IEnumerator CStart()
        {
            ShowEffect.Blackout();
            yield return new WaitForSeconds(0.3f);

            menuCamera.gameObject.SetActive(false);
            gameCamera.gameObject.SetActive(true);

            HealthManager.instance.ResetHealth();
            ActiveBoostsManager.instance.DisableAllBoost();
            TargetManager.instance.StartNewRun();
            MapGenerator.instance.ResetMap();
            ScreensManager.ShowGameHud();
            PlayerController.enableInput = true;
            MovePlayerCar.SetSpeed(GameBalance.GetPlayerBalance().startSpeed, GameBalance.GetPlayerBalance().accelerationSpeed);
            PlayerController.instance.SetCarBasePosition();
            MovePlayerCar.SetZeroPosition();
        }

        private IEnumerator CEnd()
        {
            ShowEffect.Blackout();
            yield return new WaitForSeconds(0.3f);

            gameCamera.gameObject.SetActive(false);
            menuCamera.gameObject.SetActive(true);
            PopupManager.HideArrivalResult();
            BaseState();
        }
    }
}