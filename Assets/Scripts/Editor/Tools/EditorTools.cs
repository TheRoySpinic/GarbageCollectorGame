using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorTools
{
    [UnityEditor.MenuItem("Tools/Clear PlayerPrefs")]
    public static void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("Clear playerPrefs complete!");
    }
}
