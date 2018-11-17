using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;

public class Tooltip : MonoBehaviour {

    // Tooltip UI
    public GameObject tooltip;
    public Image tooltipIndicatorFill;
    public TextMeshProUGUI tooltipText;
    public float fillTime;

    private bool fillFinished = false;

    // Set what message to show
    public void SetTooltipData(string itemName, string message)
    {
        tooltipText.SetText("[" + itemName + "]. " + message);
    }

    // Show/hide tooltip
    public void ShowTooltip(bool show)
    {
        tooltip.SetActive(show);
    }
    
    // Get value of fillFinished bool
    public bool IsFillFinished()
    {
        return fillFinished;
    }

    // Set value of fillFinished bool
    public void SetFillFinished(bool fillFinished)
    {
        this.fillFinished = fillFinished;
    }
}
