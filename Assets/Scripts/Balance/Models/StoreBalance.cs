using Monetisation;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Balance
{
    [Serializable]
    public class StoreBalance
    {
        public NewbyeOfferConfig newbyeOfferConfig = new NewbyeOfferConfig();
        public List<StoreProduct> storeProducts = new List<StoreProduct>();
        public List<OfferConfig> offerConfigs = new List<OfferConfig>();
    }

    [Serializable]
    public class StoreProduct
    {
        public string storeId = "";
        public Reward reward = new Reward();
        public int profitValue = -1;
    }

    [Serializable]
    public class NewbyeOfferConfig
    {
        public string storeId = "";
        public OfferParameter parameter = new OfferParameter();
        public List<Reward> rewards = new List<Reward>();
    }

    [Serializable]
    public class OfferConfig
    {
        public string storeId = "";
        public OfferParameter parameter = new OfferParameter();
        public List<Reward> rewards = new List<Reward>();
    }

    [Serializable]
    public class OfferParameter
    {
        public bool showCost = true;

        public int minArrivalForShow = 3;
    }
}