using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Tools
{
    public class ProgressBar : MonoBehaviour
    {
        [Header("Setup")]
        [SerializeField]
        private Image bg = null;
        [SerializeField]
        private Image bar = null;
        //[SerializeField]
        private EProgressDirection direction = EProgressDirection.LEFT_RIGHT;

        [Space]
        [Header("Progress bar")]
        [SerializeField, Range(0, 1f)]
        private float value = 0.5f;


        private RectTransform bgRect = null;
        private RectTransform barRect = null;

        public void SetValue(float newValue)
        {
            value = Mathf.Clamp01(newValue);

            UpdateProgressBar();
        }


        [ContextMenu("Preview value")]
        private void PreviewValue()
        {
            bgRect = null;
            barRect = null;

            UpdateProgressBar();
        }

        private void Setup()
        {
            if (bgRect == null)
                bgRect = bg.gameObject.GetComponent<RectTransform>();
            if (barRect == null)
                barRect = bar.gameObject.GetComponent<RectTransform>();

            //UpdateBarPivot();
            //UpdateBasePosition();
        }

        private void UpdateProgressBar()
        {
            if (barRect == null || bgRect == null)
                Setup();

            switch (direction)
            {
                case EProgressDirection.LEFT_RIGHT:
                case EProgressDirection.RIGHT_LEFT:
                    barRect.sizeDelta = new Vector2(value.Remap(0, 1, 0, bgRect.rect.width), barRect.sizeDelta.y);
                    break;
                case EProgressDirection.UP_DOWN:
                case EProgressDirection.DOWN_UP:
                    barRect.sizeDelta = new Vector2(barRect.sizeDelta.x, value.Remap(0, 1, 0, bgRect.rect.width));
                    break;
            }
        }

        private void UpdateBarPivot()
        {
            switch (direction)
            {
                case EProgressDirection.LEFT_RIGHT:
                    barRect.pivot = new Vector2(0, 0.5f);
                    break;
                case EProgressDirection.RIGHT_LEFT:
                    barRect.pivot = new Vector2(1, 0.5f);
                    break;
                case EProgressDirection.UP_DOWN:
                    barRect.pivot = new Vector2(0.5f, 1);
                    break;
                case EProgressDirection.DOWN_UP:
                    barRect.pivot = new Vector2(0.5f, 0);
                    break;
            }
        }

        private void UpdateBasePosition()
        {
            switch (direction)
            {
                case EProgressDirection.LEFT_RIGHT:
                    barRect.localPosition = new Vector2(0, barRect.localPosition.y);
                    break;
                case EProgressDirection.RIGHT_LEFT:
                    barRect.localPosition = new Vector2(0, barRect.localPosition.y);
                    break;
                case EProgressDirection.UP_DOWN:
                    barRect.pivot = new Vector2(barRect.localPosition.x, 0);
                    break;
                case EProgressDirection.DOWN_UP:
                    barRect.pivot = new Vector2(barRect.localPosition.x, 0);
                    break;
            }
        }


        private enum EProgressDirection
        {
            LEFT_RIGHT,
            RIGHT_LEFT,
            UP_DOWN,
            DOWN_UP
        }
    }
}