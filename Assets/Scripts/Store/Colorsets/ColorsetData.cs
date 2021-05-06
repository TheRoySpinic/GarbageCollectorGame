using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Store.Colorsets
{
    [System.Serializable]
    public class ColorsetData
    {
        public string name = "";
        public EColorsetType colorset = EColorsetType.DEFAULT;
        public Sprite iconSprite = null;
        public Texture maintTexture = null;
        public int cost = 1;
        //public bool night = false;
    }
}