using Balance;
using Store;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monetisation
{
    public static class RewardManager
    {
        public static void AddReward(ERewardType rewardType, int value)
        {
            switch (rewardType)
            {
                case ERewardType.GOLD:
                    MasterStoreManager.instance.AddGold(value);
                    break;
                case ERewardType.CAR:
                    break;
                case ERewardType.CAR_COLOR:
                    break;
                case ERewardType.COLLORSET:
                    break;
                case ERewardType.TIME_BOOSTER:
                    break;
            }
        }

        public static void AddReward(StoreProduct storeProduct)
        {
            AddReward(storeProduct.reward.rewardType, storeProduct.reward.value);
        }
    }
}