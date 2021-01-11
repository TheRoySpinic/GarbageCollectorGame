using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlphaDebuger
{
    

    public class CanvasSwitcher : MonoBehaviour
    {
        public static CanvasSwitcher instance;

        [SerializeField]
        private List<Canvases> canvases = new List<Canvases>();

        private void Awake()
        {
            if (!instance)
                instance = this;
            else
            {
                Debug.Log("[CanvasSwitcher] Dublicate instance. Destroy gameobject \"" + gameObject.name + "\"");
                Destroy(gameObject);
            }
        }

        public void OpenCanvas(ECanvasType type)
        {
            HideCanvases();

            ShowCanvas(type);
        }

        private void ShowCanvas(ECanvasType type)
        {
            foreach (Canvases c in canvases)
            {
                if(c.type.Equals(type))
                {
                    c.canvas.SetActive(true);
                    return;
                }
            }
        }

        private void HideCanvases()
        {
            foreach(Canvases c in canvases)
            {
                c.canvas.SetActive(false);
            }
        }
        
        [System.Serializable]
        class Canvases
        {
            public ECanvasType type;
            public GameObject canvas;
        }
    }

}

