namespace Monetisation
{
    [System.Serializable]
    public class Reward
    {
        public ERewardType rewardType = ERewardType.NONE;
        public int iconId = -1; // -1 = default icon
        public int value = 0; // must be more that 0!!! (value>0 && value != 0)
    }
}