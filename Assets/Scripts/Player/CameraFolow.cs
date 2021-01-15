using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFolow : MonoBehaviour
{
    [SerializeField]
    private GameObject playerCar = null;

    [SerializeField]
    private float speed = 5f;
    
    private Vector3 newPosition = new Vector3();

    void Update()
    {
        if(transform.position.z != playerCar.transform.position.z)
        {
            newPosition.Set(transform.position.x, transform.position.y, playerCar.transform.position.z);
            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * speed);
        }
    }
}
