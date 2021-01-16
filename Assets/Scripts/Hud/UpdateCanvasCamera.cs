using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateCanvasCamera : MonoBehaviour
{
    void Start()
    {
        Base.SceneLoader.E_LoadScene += UpdateCamera;
    }

    private void UpdateCamera()
    {
        Canvas canvas = gameObject.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.worldCamera = Camera.main;
    }
}
