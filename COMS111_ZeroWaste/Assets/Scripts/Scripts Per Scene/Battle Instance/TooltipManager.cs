using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;

public class TooltipManager : MonoBehaviour, IPointerDownHandler, IPointerUpHandler{

    [Header("Tooltip Data")]
    [SerializeField]
    private AttackDetails attackInfo;

    [Space]
    public bool pointerDown;
    public float requiredHoldTime;
    private float pointerDownTime;

    [Header("Tooltip UI")]
    [SerializeField]
    private Image fillImage;
    [SerializeField]
    private GameObject tooltip;
    [SerializeField]
    private TextMeshProUGUI tooltipText;

    // Triggered when user holds a click on a button
    public void OnPointerDown(PointerEventData eventData)
    {
        pointerDown = true;
        Debug.Log("Holding down " + attackInfo.attackName + "button...");
    }

    // Triggered when user releases hold on a button
    public void OnPointerUp(PointerEventData eventData)
    {
        pointerDown = false;
    }

    void Update()
    {
        // Check if user is holding any button
        if (pointerDown)
        {
            // Get seconds that pass by
            pointerDownTime += Time.deltaTime;
            Debug.Log(pointerDownTime);

            // Check if required time is finished
            if (pointerDownTime >= requiredHoldTime)
            {
                // Animate attack button fill
                fillImage.fillAmount = pointerDownTime / requiredHoldTime;
                // Show tooltip while user's holding the button
                ShowTooltip();
            }
        }
        else
        {
            // Animate attack button fill
            pointerDownTime -= Time.deltaTime * 3;
            fillImage.fillAmount = pointerDownTime / requiredHoldTime;

            // Reset everything
            Reset();
        }
    }

    public void ShowTooltip()
    {
        // Prepare tooltip message
        tooltipText.SetText(attackInfo.tooltipMessage);
        tooltip.SetActive(pointerDown);
    }

    public void Reset()
    {
        // Reset just to make sure
        pointerDown = false;

        // Hide tooltip
        tooltip.SetActive(pointerDown);

        // If timer is negative, set to 0
        if (pointerDownTime <= -0)
            pointerDownTime = 0;
    }

    public void ButtonTest() {
        Debug.Log("Hello");
    }
}
