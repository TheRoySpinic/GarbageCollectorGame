using Store;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheatManager : MonoBehaviour
{
    public void AddGold(int value)
    {
        MasterStoreManager.instance.AddGold(value);
    }
}
