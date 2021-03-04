using Balance;
using Base;
using HUD;
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

        public void StartArrival()
        {
            StartCoroutine(CStart());
            E_Start?.Invoke();
        }

        public void EndArrival()
        {
            E_End?.Invoke();
        }

        private IEnumerator CStart()
        {
            ShowEffect.Blackout();
            yield return new WaitForSeconds(0.3f);

            ScreensManager.ShowGameHud();
            PlayerController.enableInput = true;
            MovePlayerCar.SetSpeed(GameBalance.GetPlayerBalance().startSpeed);
        }
    }
}