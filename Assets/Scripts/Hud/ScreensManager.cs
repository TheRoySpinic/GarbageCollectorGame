using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HUD
{
    public class ScreensManager : MonoBehaviour
    {
        public static ScreensManager instance;

        [SerializeField]
        private GameObject gameHud = null;

        [SerializeField]
        private GameObject menuHud = null;

        [SerializeField]
        private GameObject garage = null;

        [SerializeField]
        private GameObject colorsetStore = null;

        [SerializeField]
        private GameObject buyStore = null;

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
        }

        public static void HideMenuHud()
        {
            instance.menuHud.SetActive(false);
            instance.activeScreens.Remove(instance.menuHud);
        }

        public static void ShowColorsetStore()
        {
            HideAllActiveScreens();
            instance.colorsetStore.SetActive(true);
            instance.activeScreens.Add(instance.colorsetStore);
        }

        public static void HideColorsetStore()
        {
            instance.colorsetStore.SetActive(false);
            instance.activeScreens.Remove(instance.colorsetStore);
        }

        public static void ShowBuyStore()
        {
            HideAllActiveScreens();
            instance.buyStore.SetActive(true);
            instance.activeScreens.Add(instance.buyStore);
        }

        public static void HideBuyStore()
        {
            instance.buyStore.SetActive(false);
            instance.activeScreens.Remove(instance.buyStore);
        }

        public static void ShowGarage()
        {
            HideAllActiveScreens();
            instance.garage.SetActive(true);
            instance.activeScreens.Add(instance.garage);
        }

        public static void HideGarage()
        {
            instance.garage.SetActive(false);
            instance.activeScreens.Remove(instance.garage);
        }

        public static void HideAllActiveScreens()
        {
            foreach(GameObject screen in instance.activeScreens)
            {
                screen.SetActive(false);
            }
            instance.activeScreens.Clear();
        }
    }
}
