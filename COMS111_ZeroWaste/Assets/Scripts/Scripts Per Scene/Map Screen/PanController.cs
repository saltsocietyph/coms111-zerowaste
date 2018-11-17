using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PanController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

    public ZoomAndPan zoomAndPan;
    public string buttonPosition;
    private bool moveMap = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        moveMap = true;

        if(buttonPosition.Equals("downRight")) {
            zoomAndPan.DownRight(moveMap);
        }

        if (buttonPosition.Equals("upRight"))
        {
            zoomAndPan.UpRight(moveMap);
        }

        if (buttonPosition.Equals("downLeft"))
        {
            zoomAndPan.DownLeft(moveMap);
        }

        if (buttonPosition.Equals("upLeft"))
        {
            zoomAndPan.UpLeft(moveMap);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        moveMap = false;

        if (buttonPosition.Equals("downRight"))
        {
            zoomAndPan.DownRight(moveMap);
        }

        if (buttonPosition.Equals("upRight"))
        {
            zoomAndPan.UpRight(moveMap);
        }

        if (buttonPosition.Equals("downLeft"))
        {
            zoomAndPan.DownLeft(moveMap);
        }

        if (buttonPosition.Equals("upLeft"))
        {
            zoomAndPan.UpLeft(moveMap);
        }

    }
}
