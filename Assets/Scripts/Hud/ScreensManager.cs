using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HUD
{
    public class ScreensManager : MonoBehaviour
    {
        public static ScreensManager instance;

        public static event Action E_ShowGameHud;
        public static event Action E_ShowMenuHud;
        public static event Action E_ShowGarage;
        public static event Action E_ShowColorsetStore;
        public static event Action E_ShowBuyStore;
        public static event Action E_ShowSettings;
        public static event Action E_UpdateActiveScreen;

        [SerializeField]
        private GameObject tint = null;

        [SerializeField]
        private GameObject gameHud = null;

        [SerializeField]
        private GameObject menuHud = null;

        [SerializeField]
        private GameObject[] garage = null;

        [SerializeField]
        private GameObject colorsetStore = null;

        [SerializeField]
        private GameObject buyStore = null;

        [SerializeField]
        private GameObject settings = null;

        private List<GameObject> activeScreens = new List<GameObject>();

        private void Awake()
        {
            if (instance == null)
                instance = this;
        }

        private void OnDestroy()
        {
            if (instance.Equals(this))
                instance = null;
        }

        public static void ShowGameHud()
        {
            HideAllActiveScreens();
            instance.gameHud.SetActive(true);
            instance.activeScreens.Add(instance.gameHud);
            E_ShowGameHud?.Invoke();
            E_UpdateActiveScreen?.Invoke();

            instance.tint.SetActive(false);
        }

        public static void HideGameHud()
        {
            instance.gameHud.SetActive(false);
            instance.activeScreens.Remove(instance.gameHud);
        }

        public static void ShowMenuHud()
        {
            HideAllActiveScreens();
            instance.menuHud.SetActive(true);
            instance.activeScreens.Add(instance.menuHud);
            E_ShowMenuHud?.Invoke();
            E_UpdateActiveScreen?.Invoke();

            instance.tint.SetActive(false);
        }

        public static void HideMenuHud()
        {
            instance.menuHud.SetActive(false);
            instance.activeScreens.Remove(instance.menuHud);

            instance.tint.SetActive(false);
        }

        public static void ShowColorsetStore()
        {
            HideAllActiveScreens();
            instance.colorsetStore.SetActive(true);
            instance.activeScreens.Add(instance.colorsetStore);
            E_ShowColorsetStore?.Invoke();
            E_UpdateActiveScreen?.Invoke();

            instance.tint.SetActive(true);
        }

        public static void HideColorsetStore()
        {
            instance.colorsetStore.SetActive(false);
            instance.activeScreens.Remove(instance.colorsetStore);

            instance.tint.SetActive(false);
        }

        public static void ShowBuyStore()
        {
            HideAllActiveScreens();
            instance.buyStore.SetActive(true);
            instance.activeScreens.Add(instance.buyStore);
            E_ShowBuyStore?.Invoke();
            E_UpdateActiveScreen?.Invoke();
        }

        public static void HideBuyStore()
        {
            instance.buyStore.SetActive(false);
            instance.activeScreens.Remove(instance.buyStore);

            instance.tint.SetActive(false);
        }

        public static void ShowSettings()
        {
            HideAllActiveScreens();
            instance.settings.SetActive(true);
            instance.activeScreens.Add(instance.settings);
            E_ShowSettings?.Invoke();
            E_UpdateActiveScreen?.Invoke();
        }

        public static void HideSettings()
        {
            instance.settings.SetActive(false);
            instance.activeScreens.Remove(instance.settings);

            instance.tint.SetActive(false);
        }

        public static void ShowGarage()
        {
            HideAllActiveScreens();
            instance.garage[0].SetActive(true);
            instance.garage[1].SetActive(true);
            instance.activeScreens.Add(instance.garage[0]);
            E_ShowGarage?.Invoke();
            E_UpdateActiveScreen?.Invoke();

            instance.tint.SetActive(false);
        }

        public static void HideGarage()
        {
            instance.garage[0].SetActive(false);
            instance.garage[1].SetActive(false);
            instance.activeScreens.Remove(instance.garage[0]);
        }

        public static void HideAllActiveScreens()
        {
            foreach(GameObject screen in instance.activeScreens)
            {
                screen.SetActive(false);
            }

            instance.tint.SetActive(false);

            instance.garage[1].SetActive(false);

            instance.activeScreens.Clear();
        }
    }
}
