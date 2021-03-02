using Player;
using Player.Control;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCarControlMode : MonoBehaviour
{
    [SerializeField]
    private ECarControlType controlType = ECarControlType.LINES;

    public void ClickAction()
    {
        PlayerController.instance.SetControlType(controlType);
    }
}
