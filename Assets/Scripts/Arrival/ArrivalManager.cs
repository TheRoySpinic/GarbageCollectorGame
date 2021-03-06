using Balance;
using Base;
using HUD;
using Map;
using Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arrival
{
    public class ArrivalManager : SingletonGen<ArrivalManager>
    {
        public static Action E_Start;
        public static Action E_End;

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
            MapFiller.instance.ReloadMap(true);
            ScreensManager.ShowMenuHud();
            PlayerController.enableInput = false;
            PlayerController.instance.SetCarBasePosition();
            MovePlayerCar.SetSpeed(GameBalance.GetPlayerBalance().previewSpeed);
            MovePlayerCar.SetZeroPosition();
        }


        private IEnumerator CStart()
        {
            ShowEffect.Blackout();
            yield return new WaitForSeconds(0.3f);

            MapFiller.instance.ReloadMap(false);
            ScreensManager.ShowGameHud();
            PlayerController.enableInput = true;
            MovePlayerCar.SetSpeed(GameBalance.GetPlayerBalance().startSpeed);
        }

        private IEnumerator CEnd()
        {
            ShowEffect.Blackout();
            yield return new WaitForSeconds(0.3f);

            BaseState();
        }
    }
}