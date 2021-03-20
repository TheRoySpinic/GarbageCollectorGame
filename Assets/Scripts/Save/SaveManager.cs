using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveManager
{
    public static void SaveAll()
    {
        PlayerPrefs.Save();
    }
}
