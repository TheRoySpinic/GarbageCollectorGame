using Base;
using Garage;
using Map;
using Player.Control;
using Player.Control.Joystic;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Player
{
    public class PlayerController : SingletonGen<PlayerController>
    {
        public static bool enableInput = false;

        private int currentLine = 1;
        private float targetPos = 0;

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
        private Transform carTransform = null;
        [SerializeField]
        private new GameObject camera = null;
        
        private CarMesh carMesh = null;

        [SerializeField]
        private float moveSpeed = 10;
        private float baseMoveSpeed = 10;

        [SerializeField]
        private ECarControlType controlType = ECarControlType.FREE_CONTROL;
        [SerializeField]
        private bool useSwipe = true;

        [SerializeField]
        private Joystick dynamicJoystic = null;
        [SerializeField]
        private Joystick floatingJoystic = null;
        [SerializeField]
        private GameObject buttonsControl = null;
        [SerializeField]
        private GameObject swipeControl = null;

        private Joystick activeJoystic = null;

#if UNITY_EDITOR
        private bool left;
        private bool right;
        private TouchPhase phase;
#endif

        override public void Init()
        {
            if(GarageManager.instance != null)
            {
                SetCarSpeed();
            }
            else
            {
                GarageManager.E_Ready -= SetCarSpeed;
                GarageManager.E_Ready += SetCarSpeed;
            }

            carTransform = car.transform;

            carMesh = carTransform.GetChild(0).GetComponent<CarMesh>();

            GarageManager.E_ActiveCarUpdate -= LoadCarPrefab;
            GarageManager.E_ActiveCarUpdate += LoadCarPrefab;

            if (GarageManager.instance)
                LoadCarPrefab(GarageManager.instance.GetActiveCarType());
            else
                GarageManager.E_GarageManagerReady += LoadCar;

            TouchController.OnSwipe -= CheckSwipe;
            TouchController.OnSwipe += CheckSwipe;

            UpdateControlers();
        }

        override public void Destroy()
        {
            GarageManager.E_ActiveCarUpdate -= LoadCarPrefab;
            GarageManager.E_GarageManagerReady -= LoadCar;
            GarageManager.E_Ready -= SetCarSpeed;
            TouchController.OnSwipe -= CheckSwipe;
        }

        private void Update()
        {
            if (!HealthManager.isAlive || !enableInput)
                return;


            switch (controlType)
            {
#if UNITY_EDITOR
                case ECarControlType.LINES:
                    if (Input.GetAxis("Horizontal") != 0 && phase == TouchPhase.Canceled)
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
                    break;
#endif
                case ECarControlType.FREE_CONTROL:
                    if (Input.touchCount > 0 || Input.GetMouseButton(0))
                    {
                        Vector2 touchPosition = Input.touchCount > 0 ? Input.GetTouch(0).position : (Vector2)Input.mousePosition;

                        if (touchPosition.y < Screen.height / 2)
                        {
                            targetPos = Mathf.Clamp(touchPosition.x.Remap(0, Screen.width, 6.0f, -6.0f), -6.0f, 6.0f); //Не забыть убрать. Указывает позицию на экране
                        }
                    }

                    if (carTransform.position.z != targetPos)
                    {
                        carTransform.position = Vector3.MoveTowards(carTransform.position,
                            new Vector3(carTransform.position.x, carTransform.position.y, targetPos),
                            Time.deltaTime * moveSpeed);
                    }
                    break;

                case ECarControlType.FLOATING_JOYSTIC:
                case ECarControlType.DYNAMIC_JOYSTIC:
                    if (activeJoystic.Horizontal == 0)
                        return;

                    float target = (carTransform.position + Vector3.forward * Time.deltaTime * -activeJoystic.Horizontal * moveSpeed).z;

                    if (target > -6.0f && target < 6.0f) //Не забыть убрать. Указывает позицию на экране
                        carTransform.Translate(Vector3.forward * Time.deltaTime * -activeJoystic.Horizontal * moveSpeed);
                    break;
            }
        }

        public void LineMoveLeft()
        {
            if (!HealthManager.isAlive || !enableInput)
                return;

            if (controlType != ECarControlType.LINES)
                return;

            StopCoroutine(MoveRight());
            StartCoroutine(MoveLeft());
        }

        public void LineMoveRight()
        {
            if (!HealthManager.isAlive || !enableInput)
                return;

            if (controlType != ECarControlType.LINES)
                return;

            StopCoroutine(MoveLeft());
            StartCoroutine(MoveRight());
        }

        public CarMesh GetCarMesh()
        {
            return carMesh;
        }

        public void SetControlType(ECarControlType type)
        {
            controlType = type;

            UpdateControlers();
        }

        public void UseSwipeLineControl(bool use)
        {
            useSwipe = use;

            UpdateControlers();
        }

        public void SetCarBasePosition()
        {
            if(controlType.Equals(ECarControlType.LINES))
            {
                currentLine = 1;
            }

            targetPos = 0;

            carTransform.localPosition = Vector3.zero;
        }

        public float GetMoveSpeed()
        {
            return moveSpeed;
        }

        public void SetMoveSpeed(float newSpeed)
        {
            moveSpeed = newSpeed;
        }

        public void ResetMoveSpeed()
        {
            moveSpeed = baseMoveSpeed;
        }


        private void CheckSwipe(Vector2Int vector)
        {
            if(vector.x > 0)
            {
                LineMoveRight();
            }
            else if(vector.x < 0)
            {
                LineMoveLeft();
            }
        }

        private void UpdateControlers()
        {
            dynamicJoystic.gameObject.SetActive(false);
            floatingJoystic.gameObject.SetActive(false);
            buttonsControl.SetActive(false);
            swipeControl.SetActive(false);

            switch (controlType)
            {
                case ECarControlType.LINES:
                    if (useSwipe)
                    {
                        swipeControl.SetActive(true);
                    }
                    else
                    {
                        buttonsControl.SetActive(true);
                    }
                    break;
                case ECarControlType.FLOATING_JOYSTIC:
                    floatingJoystic.gameObject.SetActive(true);
                    activeJoystic = floatingJoystic;
                    break;
                case ECarControlType.DYNAMIC_JOYSTIC:
                    dynamicJoystic.gameObject.SetActive(true);
                    activeJoystic = dynamicJoystic;
                    break;
            }
        }

        private void LoadCar()
        {
            LoadCarPrefab(GarageManager.instance.GetActiveCarType());
        }

        private void LoadCarPrefab(ECarType carType)
        {
            if (carType.Equals(ECarType.NONE))
            {
                Debug.LogError("Ivalide carType!!!");
                return;
            }
            GameObject o = Instantiate(GarageManager.instance.GetCarPrefab(carType), car.transform);
            o.transform.position = carMesh.transform.position;
            o.transform.eulerAngles = carMesh.transform.eulerAngles;

            Destroy(carMesh.gameObject);
            carMesh = o.GetComponent<CarMesh>();

            moveSpeed = GarageManager.instance.GetActiveCarSpeed();
        }

        private void SetCarSpeed()
        {
            moveSpeed = GarageManager.instance.GetActiveCarSpeed();
            baseMoveSpeed = moveSpeed;
        }


        private IEnumerator MoveLeft()
        {
            if(currentLine > 0 /*&& canMoveLeft*/)
            {
                canMoveRight = false;
                Vector3 target = new Vector3(carTransform.position.x, carTransform.position.y, MapManager.instance.lineShifts[currentLine]);
                --currentLine;
                while(carTransform.position.z != MapManager.instance.lineShifts[currentLine])
                {
                    camera.transform.rotation = Quaternion.Euler(camera.transform.rotation.eulerAngles.x, camera.transform.rotation.eulerAngles.y, -cameraTurnCurve.Evaluate(Vector3.Distance(carTransform.position, target)));
                    carTransform.rotation = Quaternion.Euler(-rotationCurve.Evaluate(Vector3.Distance(carTransform.position, target)), -turnCurve.Evaluate(Vector3.Distance(carTransform.position, target)), 0);
                    carTransform.position = Vector3.MoveTowards(carTransform.position, new Vector3(carTransform.position.x, carTransform.position.y, MapManager.instance.lineShifts[currentLine]), speedCurve.Evaluate(Vector3.Distance(carTransform.position, target)) * Time.deltaTime * moveSpeed);
                    yield return new WaitForEndOfFrame();
                }
                canMoveRight = true;
                carTransform.rotation = new Quaternion();
                carTransform.position = new Vector3(carTransform.position.x, carTransform.position.y, MapManager.instance.lineShifts[currentLine]);
            }
        }

        private IEnumerator MoveRight()
        {
            if (currentLine < MapManager.instance.lineShifts.Length - 1 /*&& canMoveRight*/)
            {
                canMoveLeft = false;
                Vector3 target = new Vector3(carTransform.position.x, carTransform.position.y, MapManager.instance.lineShifts[currentLine]);
                ++currentLine;
                while (carTransform.position.z != MapManager.instance.lineShifts[currentLine])
                {
                    camera.transform.rotation = Quaternion.Euler(camera.transform.rotation.eulerAngles.x, camera.transform.rotation.eulerAngles.y, cameraTurnCurve.Evaluate(Vector3.Distance(carTransform.position, target)));
                    carTransform.rotation = Quaternion.Euler(rotationCurve.Evaluate(Vector3.Distance(carTransform.position, target)), turnCurve.Evaluate(Vector3.Distance(carTransform.position, target)), 0);
                    carTransform.position = Vector3.MoveTowards(carTransform.position, new Vector3(carTransform.position.x, carTransform.position.y, MapManager.instance.lineShifts[currentLine]), speedCurve.Evaluate(Vector3.Distance(carTransform.position, target))* Time.deltaTime * moveSpeed);
                    yield return new WaitForEndOfFrame();
                }

                canMoveLeft = true;
                carTransform.rotation = new Quaternion();
                carTransform.position = new Vector3(carTransform.position.x, carTransform.position.y, MapManager.instance.lineShifts[currentLine]);
            }
        }
    }
}