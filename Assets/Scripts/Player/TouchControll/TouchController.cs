using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Player.Control
{
    public class TouchController : MonoBehaviour
    {
        public static Action<Vector2Int> OnSwipe;

        [SerializeField]
        private float minSwipeDistance = 0;

        [SerializeField]
        private float xDistance = 0;
        private float yDistance = 0;

        private bool swipeDetection = false;

        public void BeginTouch()
        {
            swipeDetection = true;
        }

        public void MoveTouch()
        {
            if (!swipeDetection)
                return;

            xDistance += Input.GetTouch(0).deltaPosition.x;
            yDistance += Input.GetTouch(0).deltaPosition.y;

            if(Mathf.Abs(xDistance) > minSwipeDistance || Mathf.Abs(yDistance) > minSwipeDistance)
            {
                Vector2Int vector = new Vector2Int(
                    xDistance > 0 ? 1 : xDistance < 0 ? -1 : 0,
                    yDistance > 0 ? 1 : yDistance < 0 ? -1 : 0
                    );

                OnSwipe?.Invoke(vector);

                swipeDetection = false;

                xDistance = 0;
                yDistance = 0;
            }
        }

        public void EndTouch()
        {
            swipeDetection = false;

            xDistance = 0;
            yDistance = 0;
        }
    }
}