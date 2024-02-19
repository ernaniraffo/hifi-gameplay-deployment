using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    private float smoothTime = 0.3f;

    private bool rotatingLeft = false;
    private bool rotatingRight = false;

    // Update is called once per frame
    void Update()
    {
        // Set left rotation
        if (!isRotating() && Input.GetKeyDown(KeyCode.Q))
        {
            SetLeftRotation();
        }

        // Continue rotating object left
        if (rotatingLeft)
        {
            RotateLeft();
        }

        // Rotate right
        if (!isRotating() && Input.GetKeyDown(KeyCode.E))
        {
            SetRightRotation();
        }

        // Continue rotating object right
        if (rotatingRight)
        {
            RotateRight();
        }
    }

    void SetRightRotation()
    {
        StartCoroutine(RotateRight());
        rotatingRight = true;
    }

    void SetLeftRotation()
    {
        StartCoroutine(RotateLeft());
        rotatingLeft = true;
    }

    IEnumerator RotateLeft()
    {
        rotatingLeft = true;
        Vector3 byAngles = Vector3.up * 90;
        var fromAngle = transform.rotation;
        var toAngle = Quaternion.Euler(transform.eulerAngles + byAngles);
        for (var t = 0f; t < 1; t += Time.deltaTime / smoothTime)
        {
            transform.rotation = Quaternion.Slerp(fromAngle, toAngle, t);
            yield return null;
        }
        rotatingLeft = false;
    }

    IEnumerator RotateRight()
    {
        rotatingRight = true;
        Vector3 byAngles = Vector3.up * -90;
        var fromAngle = transform.rotation;
        var toAngle = Quaternion.Euler(transform.eulerAngles + byAngles);
        for (var t = 0f; t < 1; t += Time.deltaTime / smoothTime)
        {
            transform.rotation = Quaternion.Slerp(fromAngle, toAngle, t);
            yield return null;
        }
        rotatingRight = false;
    }

    bool isRotating()
    {
        return rotatingLeft || rotatingRight;
    }
}
