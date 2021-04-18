using Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Boosts
{
    [Serializable]
    public class Boost
    {
        public EBoostType boostType = EBoostType.NONE;
        public float timeLeft = 10;
        public int value = 0;

        public Boost() { }

        public Boost(Boost other)
        {
            boostType = other.boostType;
            timeLeft = other.timeLeft;
            value = other.value;
        }

        public void Activate()
        {
            switch(boostType)
            {
                case EBoostType.IMMORTAL:
                    HealthManager.instance.SetIsTakeDamage(false);
                    PlayerController.instance.GetCarMesh().boostEffects.ShowShield();
                    break;
                case EBoostType.REPAIR:
                    HealthManager.instance.RestoreHealth(value);
                    break;
                case EBoostType.SPEED:
                    PlayerController.instance.SetMoveSpeed(PlayerController.instance.GetMoveSpeed() + value);
                    break;
            }
        }

        public void Deactivate()
        {
            switch (boostType)
            {
                case EBoostType.IMMORTAL:
                    HealthManager.instance.SetIsTakeDamage(true);
                    PlayerController.instance.GetCarMesh().boostEffects.HideShield();
                    break;
                case EBoostType.SPEED:
                    PlayerController.instance.SetMoveSpeed(PlayerController.instance.GetMoveSpeed() - value);
                    break;
            }
        }
    }
}