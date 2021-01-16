using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScene : MonoBehaviour
{
    [SerializeField]
    private string sceneName = "MenuScene";

    private void Start()
    {
        Base.SceneLoader.LoadScene(sceneName);
    }
}
