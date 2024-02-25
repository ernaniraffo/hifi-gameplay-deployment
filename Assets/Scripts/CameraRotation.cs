using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    public GameObject otherObject;

    private float smoothTime = 0.3f;
    private float turnTime = 0.3f;

    private bool rotatingLeft = false;
    private bool rotatingRight = false;

    private bool rotating = false;

    // Update is called once per frame
    void Update()
    {
        // Set left rotation
        //if (!isRotating() && Input.GetKeyDown(KeyCode.Q))
        //{
        //    SetLeftRotation();
        //}

        // Continue rotating object left
        if (!isRotating() && Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(Rotate(transform, otherObject.transform, Vector3.up, 90, turnTime));
        }

        // Rotate right
        //if (!isRotating() && Input.GetKeyDown(KeyCode.E))
        //{
        //    SetRightRotation();
        //}

        // Continue rotating object right
        if (!isRotating() && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(Rotate(transform, otherObject.transform, Vector3.up * -1, 90, turnTime));
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
        float y = transform.eulerAngles.y;
        float ceiled = Mathf.Ceil(y);
        Debug.Log("y " + y + " ceiled " + ceiled);
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
        float y = transform.eulerAngles.y;
        float ceiled = Mathf.Ceil(y);
        Debug.Log("y " + y + " ceiled " + ceiled);
        rotatingRight = false;
    }

    bool isRotating()
    {
        return rotating;
    }

    IEnumerator Rotate(Transform thisTransform, Transform otherTransform, Vector3 rotateAxis, float degrees, float totalTime)
    {
        rotating = true;

        var startRotation = thisTransform.rotation;
        var startPosition = thisTransform.position;
        transform.RotateAround(otherTransform.position, rotateAxis, degrees);
        var endRotation = thisTransform.rotation;
        var endPosition = thisTransform.position;
        thisTransform.rotation = startRotation;
        thisTransform.position = startPosition;

        var rate = degrees / totalTime;
        for (float i = 0; i < degrees; i += Time.deltaTime * rate)
        {
            thisTransform.RotateAround(otherTransform.position, rotateAxis, Time.deltaTime * rate);
            yield return null;
        }

        thisTransform.rotation = endRotation;
        thisTransform.position = endPosition;
        rotating = false;
    }
}
