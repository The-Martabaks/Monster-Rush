using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float sensitivity;

    void FixedUpdate()
    {
        float rotateHorizontal = Input.GetAxis("Horizontal");
        float rotateVertical = Input.GetAxis("Vertical");

        gameObject.transform.Rotate(0, rotateHorizontal * sensitivity, 0);
        gameObject.transform.Rotate(-rotateVertical * sensitivity, 0, 0);
    }

}