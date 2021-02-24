using HUD;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCar : MonoBehaviour
{
    [SerializeField]
    private GameObject slot = null;

#if UNITY_EDITOR
    Vector2 lastPos = new Vector2();
#endif

    private Vector3 defaultRotation = Vector3.zero;

    private void Awake()
    {
        ScreensManager.E_ShowGarage -= ResetPos;
        ScreensManager.E_ShowGarage += ResetPos;

        defaultRotation = slot.transform.eulerAngles;
    }

    private void OnDestroy()
    {
        ScreensManager.E_ShowGarage -= ResetPos;
    }

    private void Update()
    {
        if(Input.touchCount > 0 || Input.GetMouseButton(0))
        {
            Vector2 touchLocation = Input.touchCount > 0 ? Input.GetTouch(0).position : (Vector2)Input.mousePosition;
            
            if(touchLocation.y < Screen.height* 0.5f || touchLocation.y > Screen.height * 0.8f)
            {
                return;
            }

            float value =
#if UNITY_EDITOR 
                Input.touchCount > 0 ?
#endif
                Input.GetTouch(0).deltaPosition.x
#if UNITY_EDITOR
                : GetMouseDelta().x
#endif
            ;

            if(value != 0)
                slot.transform.Rotate(Vector3.up, -value);
        }

#if UNITY_EDITOR
        lastPos = Input.mousePosition;
#endif
    }


    private void ResetPos()
    {
        slot.transform.eulerAngles = defaultRotation;
    }

#if UNITY_EDITOR
    private Vector2 GetMouseDelta()
    {
        return ((Vector2) Input.mousePosition) - lastPos;
    }
#endif
}
