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
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController instance = null;

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

        [SerializeField]
        private ECarControlType controlType = ECarControlType.FREE_CONTROL;

        [SerializeField]
        private Joystick dynamicJoystic = null;
        [SerializeField]
        private Joystick floatingJoystic = null;

        private Joystick activeJoystic = null;

#if UNITY_EDITOR
        private bool left;
        private bool right;
        private TouchPhase phase;
#endif

        private void Awake()
        {
            if (instance == null)
                instance = this;

            carTransform = car.transform;

            carMesh = carTransform.GetChild(0).GetComponent<CarMesh>();

            GarageManager.E_ActiveCarUpdate -= LoadCarPrefab;
            GarageManager.E_ActiveCarUpdate += LoadCarPrefab;

            if (GarageManager.instance)
                LoadCarPrefab(GarageManager.instance.GetActiveCarType());
            else
                GarageManager.E_GarageManagerReady += LoadCar;
            
            UpdateControlers();
        }

        private void OnDestroy()
        {
            GarageManager.E_ActiveCarUpdate -= LoadCarPrefab;
            GarageManager.E_GarageManagerReady -= LoadCar;
        }

        private void Update()
        {
            if (!HealthManager.isAlive || !enableInput)
                return;

            switch (controlType)
            {
                case ECarControlType.LINES:
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
#endif
                    break;

                case ECarControlType.FREE_CONTROL:
                    if (Input.touchCount > 0 || Input.GetMouseButton(0))
                    {
                        Vector2 touchPosition = Input.touchCount > 0 ? Input.GetTouch(0).position : (Vector2)Input.mousePosition;

                        if (touchPosition.y < Screen.height / 2)
                        {
                            targetPos = Mathf.Clamp(touchPosition.x.Remap(0, Screen.width, 6.0f, -6.0f), -6.0f, 6.0f); //Не забыть убрать говно
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

                    if(target > -6.0f && target < 6.0f)
                        carTransform.Translate(Vector3.forward * Time.deltaTime * -activeJoystic.Horizontal * moveSpeed);
                    break;
            }
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

        public void SetCarBasePosition()
        {
            if(controlType.Equals(ECarControlType.LINES))
            {
                currentLine = 1;
            }

            targetPos = 0;

            carTransform.localPosition = Vector3.zero;
        }


        private void UpdateControlers()
        {
            dynamicJoystic.gameObject.SetActive(false);
            floatingJoystic.gameObject.SetActive(false);

            switch (controlType)
            {
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
        }


        private IEnumerator MoveLeft()
        {
            if(currentLine > 0 && canMoveLeft)
            {
                canMoveRight = false;
                Vector3 target = new Vector3(carTransform.position.x, carTransform.position.y, MapManager.instance.lineShifts[currentLine]);
                --currentLine;
                while(carTransform.position.z != MapManager.instance.lineShifts[currentLine])
                {
                    camera.transform.rotation = Quaternion.Euler(camera.transform.rotation.eulerAngles.x, camera.transform.rotation.eulerAngles.y, -cameraTurnCurve.Evaluate(Vector3.Distance(carTransform.position, target)));
                    carTransform.rotation = Quaternion.Euler(-rotationCurve.Evaluate(Vector3.Distance(carTransform.position, target)), -turnCurve.Evaluate(Vector3.Distance(carTransform.position, target)), 0);
                    carTransform.position = Vector3.MoveTowards(carTransform.position, new Vector3(carTransform.position.x, carTransform.position.y, MapManager.instance.lineShifts[currentLine]), speedCurve.Evaluate(Vector3.Distance(carTransform.position, target)) * Time.deltaTime);
                    yield return new WaitForEndOfFrame();
                }
                canMoveRight = true;
                carTransform.rotation = new Quaternion();
                carTransform.position = new Vector3(carTransform.position.x, carTransform.position.y, MapManager.instance.lineShifts[currentLine]);
            }
        }

        private IEnumerator MoveRight()
        {
            if (currentLine < MapManager.instance.lineShifts.Length - 1 && canMoveRight)
            {
                canMoveLeft = false;
                Vector3 target = new Vector3(carTransform.position.x, carTransform.position.y, MapManager.instance.lineShifts[currentLine]);
                ++currentLine;
                while (carTransform.position.z != MapManager.instance.lineShifts[currentLine])
                {
                    camera.transform.rotation = Quaternion.Euler(camera.transform.rotation.eulerAngles.x, camera.transform.rotation.eulerAngles.y, cameraTurnCurve.Evaluate(Vector3.Distance(carTransform.position, target)));
                    carTransform.rotation = Quaternion.Euler(rotationCurve.Evaluate(Vector3.Distance(carTransform.position, target)), turnCurve.Evaluate(Vector3.Distance(carTransform.position, target)), 0);
                    carTransform.position = Vector3.MoveTowards(carTransform.position, new Vector3(carTransform.position.x, carTransform.position.y, MapManager.instance.lineShifts[currentLine]), speedCurve.Evaluate(Vector3.Distance(carTransform.position, target))* Time.deltaTime);
                    yield return new WaitForEndOfFrame();
                }

                canMoveLeft = true;
                carTransform.rotation = new Quaternion();
                carTransform.position = new Vector3(carTransform.position.x, carTransform.position.y, MapManager.instance.lineShifts[currentLine]);
            }
        }
    }
}