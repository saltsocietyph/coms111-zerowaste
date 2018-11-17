using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomAndPan : MonoBehaviour {

    public Camera targetCamera;

    private Vector3 touchOrigin;

    public Vector2 panLimit;
    public float panSpeed = 0.5f;
    private float beforeCameraSize;
    private float afterCameraSize;

    public float minZoomOut = 20f;
    public float maxZoomOut = 40f;
    public float zoomSpeed = .01f;

    private bool downRight, upRight, downLeft, upLeft;
    
	void Update () {
        // If there are 2 touches, its considered that the user is pinching the screen
        if (Input.touchCount == 2)
        {
            beforeCameraSize = targetCamera.orthographicSize;

            if((targetCamera.transform.position.x < panLimit.x) && (targetCamera.transform.position.x > -panLimit.x)
                && (targetCamera.transform.position.y < panLimit.y) && (targetCamera.transform.position.y > -panLimit.y))
            {
                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);

                Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

                float diffMagnitude = currentMagnitude - prevMagnitude;

                Zoom(diffMagnitude * zoomSpeed);
            }
        }

        Vector3 cameraPosition = targetCamera.transform.position;
        if ((downRight || downLeft) && (targetCamera.transform.position.y > -panLimit.y))
        {
            cameraPosition.y -= panSpeed * Time.deltaTime;
        }

        if ((upRight || upLeft) && (targetCamera.transform.position.y < panLimit.y))
        {
            cameraPosition.y += panSpeed * Time.deltaTime;
        }

        if ((downLeft || upLeft) && (targetCamera.transform.position.x > -panLimit.x))
        {
            cameraPosition.x -= panSpeed * Time.deltaTime;
        }

        if ((downRight || upRight) && (targetCamera.transform.position.x < panLimit.x))
        {
            cameraPosition.x += panSpeed * Time.deltaTime;
        }

        targetCamera.transform.position = cameraPosition;
	}

    void Zoom(float increment)
    {
        targetCamera.orthographicSize = Mathf.Clamp(targetCamera.orthographicSize - increment, minZoomOut, maxZoomOut);
        afterCameraSize = targetCamera.orthographicSize;

        panLimit.x = panLimit.x * (beforeCameraSize / afterCameraSize);
        panLimit.y = panLimit.y * (beforeCameraSize / afterCameraSize);
    }

    public void DownRight(bool downRight)
    {
        this.downRight = downRight;
        Debug.Log("Moving down " + targetCamera.transform.position.y + "and right " + targetCamera.transform.position.x);
    }

    public void DownLeft(bool downLeft)
    {
        this.downLeft = downLeft;
        Debug.Log("Moving down " + targetCamera.transform.position.y + "and left " + targetCamera.transform.position.x);
    }

    public void UpRight(bool upRight)
    {
        this.upRight = upRight;
        Debug.Log("Moving up " + targetCamera.transform.position.y + "and right " + targetCamera.transform.position.x);
    }


    public void UpLeft(bool upLeft)
    {
        this.upLeft = upLeft;
        Debug.Log("Moving up " + targetCamera.transform.position.y + "and left " + targetCamera.transform.position.x);
    }
    
}
