using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveManager
{
    //перенести всё сохранение сюда
    public static void SaveAll()
    {
        PlayerPrefs.Save();
    }
}
