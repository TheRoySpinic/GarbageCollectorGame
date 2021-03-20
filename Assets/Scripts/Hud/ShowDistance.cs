using TMPro;
using UnityEngine;
using Tools;
using Target;

namespace HUD
{
    public class ShowDistance : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text distanceText = null;

        private void Awake()
        {
            if(TargetManager.instance == null)
            {
                gameObject.SetActive(false);
            }
        }

        private void Update()
        {
            distanceText.text = TextFormater.FormatGold((int) TargetManager.currentDistance);
        }
    }
}