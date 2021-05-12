using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Settings
{
    public class QualityLevel : MonoBehaviour
    {
        [SerializeField]
        private Button normal_Button = null;
        [SerializeField]
        private Button hight_Button = null;

        private void Awake()
        {
            if(QualitySettings.GetQualityLevel() == 2)
            {
                normal_Button.interactable = false;
                hight_Button.interactable = true;
            }
            else if (QualitySettings.GetQualityLevel() == 3)
            {
                normal_Button.interactable = true;
                hight_Button.interactable = false;
            }
        }

        public void SetNormalQualityLevel()
        {
            QualitySettings.SetQualityLevel(2);
            normal_Button.interactable = false;
            hight_Button.interactable = true;
        }

        public void SetHightlQualityLevel()
        {
            QualitySettings.SetQualityLevel(3);
            normal_Button.interactable = true;
            hight_Button.interactable = false;
        }
    }
}