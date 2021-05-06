using Store.Colorsets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Balance
{
    [System.Serializable]
    public class ColorsetStoreBalance
    {
        public List<ColorsetStoreItem> colorsetStoreItems = new List<ColorsetStoreItem>();
    }

    [System.Serializable]
    public class ColorsetStoreItem
    {
        public string name = "";
        public EColorsetType colorset = EColorsetType.DEFAULT;
        public int cost = 1;
        //public bool night = false;
    }
}
