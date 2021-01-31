using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsController : MonoBehaviour
{
    private void Awake()
    {
        QualitySettings.vSyncCount = 1;
    }
}
