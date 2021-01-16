using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Balance
{
    public class GameBalance : MonoBehaviour
    {
        public static GameBalance instance;

        [SerializeField]
        private PlayerBalance playerBalance = new PlayerBalance();

        [SerializeField]
        private MapBalance mapBalance = new MapBalance();

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

        public static PlayerBalance GetPlayerBalance()
        {
            return instance.playerBalance;
        }

        public static MapBalance GetMapBalance()
        {
            return instance.mapBalance;
        }
    }
}