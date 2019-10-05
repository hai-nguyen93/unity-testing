using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject targetToFocus;
    public float offsetDistance = 2f;

    void Start()
    {

    }


    void LateUpdate()
    {
        float distance = Mathf.Abs(transform.position.x - targetToFocus.transform.position.x);       
        if (distance >= offsetDistance)
        {
            Vector3 dest = new Vector3(targetToFocus.transform.position.x, transform.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, dest, Time.deltaTime);
        }     
    }
}
