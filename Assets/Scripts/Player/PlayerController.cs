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

        private bool canMoveLeft = true;
        private bool canMoveRight = true;

        [SerializeField]
        private AnimationCurve turnCurve = null;
        [SerializeField]
        private AnimationCurve rotationCurve = null;
        [SerializeField]
        private AnimationCurve speedCurve = null;

#if UNITY_EDITOR
        private bool left;
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
                    StopCoroutine(MoveRight());
                    StartCoroutine(MoveLeft());
                    left = false;
                }
                if (right)
                {
                    StopCoroutine(MoveLeft());
                    StartCoroutine(MoveRight());
                    right = false;
                }
                
                phase = TouchPhase.Stationary;
            }
#endif
        }

        private IEnumerator MoveLeft()
        {
            if(currentLine > 0 && canMoveLeft)
            {
                canMoveRight = false;
                Vector3 target = new Vector3(transform.position.x, transform.position.y, MapManager.instance.lineShifts[currentLine]);
                --currentLine;
                while(transform.position.z != MapManager.instance.lineShifts[currentLine])
                {
                    transform.rotation = Quaternion.Euler(-rotationCurve.Evaluate(Vector3.Distance(transform.position, target)), -turnCurve.Evaluate(Vector3.Distance(transform.position, target)), 0);
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y, MapManager.instance.lineShifts[currentLine]), speedCurve.Evaluate(Vector3.Distance(transform.position, target)));
                    yield return new WaitForEndOfFrame();
                }
                canMoveRight = true;
                transform.rotation = new Quaternion();
                transform.position = new Vector3(transform.position.x, transform.position.y, MapManager.instance.lineShifts[currentLine]);
            }
        }

        private IEnumerator MoveRight()
        {
            if (currentLine < MapManager.instance.lineShifts.Length - 1 && canMoveRight)
            {
                canMoveLeft = false;
                Vector3 target = new Vector3(transform.position.x, transform.position.y, MapManager.instance.lineShifts[currentLine]);
                ++currentLine;
                while (transform.position.z != MapManager.instance.lineShifts[currentLine])
                {
                    transform.rotation = Quaternion.Euler(rotationCurve.Evaluate(Vector3.Distance(transform.position, target)), turnCurve.Evaluate(Vector3.Distance(transform.position, target)), 0);
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y, MapManager.instance.lineShifts[currentLine]), speedCurve.Evaluate(Vector3.Distance(transform.position, target)));
                    yield return new WaitForEndOfFrame();
                }

                canMoveLeft = true;
                transform.rotation = new Quaternion();
                transform.position = new Vector3(transform.position.x, transform.position.y, MapManager.instance.lineShifts[currentLine]);
            }
        }
    }
}