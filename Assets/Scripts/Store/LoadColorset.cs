using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadColorset : MonoBehaviour
{
    public static LoadColorset instance = null;

    [SerializeField]
    private Material material = null;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void OnDestroy()
    {
        if (instance.Equals(this))
            instance = null;
    }

    public void SetMainTexture(Texture texture)
    {
        material.SetTexture("_MainTex", texture);
    }
}
