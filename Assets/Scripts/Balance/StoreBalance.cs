using Monetisation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Balance
{
    public class StoreBalance
    {

    }

    public class StoreProduct
    {
        public string storeId = "";
        public List<Reward> rewards = new List<Reward>();
    }

    public class NewbyeOfferConfig
    {
        public string storeId = "";
        public OfferParameter parameter = new OfferParameter();
        public List<Reward> rewards = new List<Reward>();
    }

    public class OfferParameter
    {
        public bool showCost = true;

        public int minArrivalForShow = 3;
    }
}