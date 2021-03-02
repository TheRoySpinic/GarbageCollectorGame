using Store;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tools
{
    public static class TextFormater
    {
        public static string FormatGold(string text)
        {
            return string.Format("{0:### ### ### ### ### ### ### ### ### ### ### ### ###}", text);
        }

        public static string FormatGold(int number)
        {
            return FormatGold(number.ToString());
        }

        public static Color GetCostColor(int cost)
        {
            if (cost > MasterStoreManager.gold)
            {
                return Color.red;
            }
            else
            {
                return Color.white;
            }
        }

        public static Color GetCostColor(bool canBuy)
        {
            if (canBuy)
            {
                return Color.red;
            }
            else
            {
                return Color.white;
            }
        }
    }
}