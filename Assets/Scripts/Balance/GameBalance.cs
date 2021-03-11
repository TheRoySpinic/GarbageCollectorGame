using Base;
using Garage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Balance
{
    public class GameBalance : SingletonGen<GameBalance>
    {
        [SerializeField]
        private PlayerBalance playerBalance = new PlayerBalance();

        [SerializeField]
        private MapBalance mapBalance = new MapBalance();

        [SerializeField]
        private BoostBalance boostBalance = new BoostBalance();

        [SerializeField]
        private CarGradeConfig carConfig = new CarGradeConfig();

        public static PlayerBalance GetPlayerBalance()
        {
            if(instance != null)
                return instance.playerBalance;

            return null;
        }

        public static MapBalance GetMapBalance()
        {
            if (instance != null)
                return instance.mapBalance;

            return null;
        }

        public static BoostBalance GetBoostBalance()
        {
            if (instance != null)
                return instance.boostBalance;

            return null;
        }

        public static CarGradeConfig GetCarGradeConfig()
        {
            if (instance != null)
                return instance.carConfig;

            return null;
        }
    }
}