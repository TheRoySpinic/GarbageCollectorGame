using Map;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        public static bool enableInput;

        private int currentLine = 1;

#if UNITY_EDITOR
        [SerializeField]
        private bool left;
        [SerializeField]
        private bool right;

        private TouchPhase phase;
#endif
        private void Update()
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Vector2 touchPosition = Input.touchCount > 0 ? Input.GetTouch(0).position : (Vector2)Input.mousePosition;

                if (touchPosition.y < Screen.height / 2)
                {
                    if (touchPosition.x < Screen.width / 2)
                    {
                        MoveLeft();
                    }
                    else
                    {
                        MoveRight();
                    }
                }
            }
#if UNITY_EDITOR

            if(Input.GetAxis("Horizontal") != 0 && phase == TouchPhase.Canceled)
            {
                left = Input.GetAxis("Horizontal") < 0;
                right = Input.GetAxis("Horizontal") > 0;

                phase = TouchPhase.Began;
            }
            else if (Input.GetAxis("Horizontal") == 0)
            {
                phase = TouchPhase.Canceled;
            }

            if (phase == TouchPhase.Began)
            {
                if (left)
                {
                    MoveLeft();
                    left = false;
                }
                if (right)
                {
                    MoveRight();
                    right = false;
                }


                phase = TouchPhase.Stationary;
            }
#endif
        }

        private void MoveLeft()
        {
            if(currentLine > 0)
            {
                --currentLine;
                transform.position = new Vector3(transform.position.x, transform.position.y, MapManager.instance.lineShifts[currentLine]);
            }
        }

        private void MoveRight()
        {
            if (currentLine < MapManager.instance.lineShifts.Length - 1)
            {
                ++currentLine;
                transform.position = new Vector3(transform.position.x, transform.position.y, MapManager.instance.lineShifts[currentLine]);
            }
        }
    }
}