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
        [SerializeField]
        private AnimationCurve cameraTurnCurve = null;

        [SerializeField]
        private GameObject car = null;
        [SerializeField]
        private new GameObject camera = null;

#if UNITY_EDITOR
        private bool left;
        private bool right;
        private TouchPhase phase;
#endif

        private void Update()
        {
            if (!HealthManager.isAlive)
                return;

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Vector2 touchPosition = Input.touchCount > 0 ? Input.GetTouch(0).position : (Vector2)Input.mousePosition;

                if (touchPosition.y < Screen.height / 3)
                {
                    if (touchPosition.x < Screen.width / 2)
                    {

                        StopCoroutine(MoveRight());
                        StartCoroutine(MoveLeft());
                    }
                    else
                    {
                        StopCoroutine(MoveLeft());
                        StartCoroutine(MoveRight());
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
                }
                if (right)
                {
                    StopCoroutine(MoveLeft());
                    StartCoroutine(MoveRight());
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
                Vector3 target = new Vector3(car.transform.position.x, car.transform.position.y, MapManager.instance.lineShifts[currentLine]);
                --currentLine;
                while(car.transform.position.z != MapManager.instance.lineShifts[currentLine])
                {
                    camera.transform.rotation = Quaternion.Euler(camera.transform.rotation.eulerAngles.x, camera.transform.rotation.eulerAngles.y, -cameraTurnCurve.Evaluate(Vector3.Distance(car.transform.position, target)));
                    car.transform.rotation = Quaternion.Euler(-rotationCurve.Evaluate(Vector3.Distance(car.transform.position, target)), -turnCurve.Evaluate(Vector3.Distance(car.transform.position, target)), 0);
                    car.transform.position = Vector3.MoveTowards(car.transform.position, new Vector3(car.transform.position.x, car.transform.position.y, MapManager.instance.lineShifts[currentLine]), speedCurve.Evaluate(Vector3.Distance(car.transform.position, target)));
                    yield return new WaitForEndOfFrame();
                }
                canMoveRight = true;
                car.transform.rotation = new Quaternion();
                car.transform.position = new Vector3(car.transform.position.x, car.transform.position.y, MapManager.instance.lineShifts[currentLine]);
            }
        }

        private IEnumerator MoveRight()
        {
            if (currentLine < MapManager.instance.lineShifts.Length - 1 && canMoveRight)
            {
                canMoveLeft = false;
                Vector3 target = new Vector3(car.transform.position.x, car.transform.position.y, MapManager.instance.lineShifts[currentLine]);
                ++currentLine;
                while (car.transform.position.z != MapManager.instance.lineShifts[currentLine])
                {
                    camera.transform.rotation = Quaternion.Euler(camera.transform.rotation.eulerAngles.x, camera.transform.rotation.eulerAngles.y, cameraTurnCurve.Evaluate(Vector3.Distance(car.transform.position, target)));
                    car.transform.rotation = Quaternion.Euler(rotationCurve.Evaluate(Vector3.Distance(car.transform.position, target)), turnCurve.Evaluate(Vector3.Distance(car.transform.position, target)), 0);
                    car.transform.position = Vector3.MoveTowards(car.transform.position, new Vector3(car.transform.position.x, car.transform.position.y, MapManager.instance.lineShifts[currentLine]), speedCurve.Evaluate(Vector3.Distance(car.transform.position, target)));
                    yield return new WaitForEndOfFrame();
                }

                canMoveLeft = true;
                car.transform.rotation = new Quaternion();
                car.transform.position = new Vector3(car.transform.position.x, car.transform.position.y, MapManager.instance.lineShifts[currentLine]);
            }
        }
    }
}