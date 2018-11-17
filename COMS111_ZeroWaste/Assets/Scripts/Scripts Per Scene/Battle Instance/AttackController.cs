using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Sample Attack Controller
public class AttackController : MonoBehaviour{

    [Header("Attack Buttons")]
    public Button basicAttack;
    public Button skill;
    public Button charge;
    public Button ultAttack;

    [Header("Target Selector")]
    public TargetSelector targetSelector;

    private Tooltip tooltipManager;
    private Button currentBtn;

    private bool pointerDown = false;
    private float fillPercent = 0.0f;

    void Update()
    {
        if (pointerDown)
        {
            if (!tooltipManager.IsFillFinished())
            {
                SetFillAnimation();
            }

            if (fillPercent >= tooltipManager.fillTime)
            {
                tooltipManager.SetFillFinished(true);
                tooltipManager.ShowTooltip(true);
            }
        }
        else
        {
            if (tooltipManager != null)
            {
                tooltipManager.SetFillFinished(false);
                tooltipManager.ShowTooltip(false);
                DeselectFillAnimation();
            }
        }
    }

    public void SetAttack(AttackDetails attackDetails)
    {
        if (currentBtn == null)
        {
            SetAttackProperties(attackDetails);
            targetSelector.EnableTargetSelection(true);
        }
        else if (currentBtn == attackDetails.attackBtn)
        {
            pointerDown = false;
            StartCoroutine(DeselectAttack());
            targetSelector.EnableTargetSelection(false);
            targetSelector.ResetPointer();
        }
        else if (currentBtn != attackDetails.attackBtn)
        {
            pointerDown = false;
            StartCoroutine(DeselectAttack());
            StartCoroutine(ChangeAttack(attackDetails));
        }
    }

    public void LockAttack(Button attackToLock)
    {
        // Lower opacity of Attack Icon
        // Disable button
    }

    IEnumerator ChangeAttack(AttackDetails attackDetails)
    {
        yield return new WaitForSeconds(fillPercent + 0.25f);
        SetAttackProperties(attackDetails);
    }

    IEnumerator DeselectAttack()
    {
        yield return new WaitForSeconds(fillPercent);
        tooltipManager = null;
        currentBtn = null;
        fillPercent = 0f;
    }

    void SetAttackProperties(AttackDetails attackDetails)
    {
        currentBtn = attackDetails.attackBtn;
        tooltipManager = attackDetails.gameObject.GetComponent<Tooltip>();
        tooltipManager.SetTooltipData(attackDetails.attackName,
            "Put the ability description here.");

        pointerDown = true;
    }

    void SetFillAnimation()
    {
        fillPercent += Time.deltaTime;
        tooltipManager.tooltipIndicatorFill.fillAmount = fillPercent / tooltipManager.fillTime;
        Debug.Log("Fill Percent: " + fillPercent);
    }

    void DeselectFillAnimation()
    {
        fillPercent -= Time.deltaTime;
        tooltipManager.tooltipIndicatorFill.fillAmount = fillPercent / tooltipManager.fillTime;
        Debug.Log("Fill Percent: " + fillPercent);
    }

    void ShowCancelIcon(float show)
    {
        Image[] cancelIcons;
        cancelIcons = currentBtn.gameObject.GetComponentsInChildren<Image>();

        foreach (Image icon in cancelIcons)
        {
            if (currentBtn.gameObject.name.Equals("UltimateAttack")) 
            {
                if (icon.name.Equals("CancelIcon"))
                {
                    icon.color = new Color(icon.color.r, icon.color.g, icon.color.b, show);
                }
                return;
            }
            
            if (icon.name.Equals("CancelIcon"))
            {
                icon.color = new Color(icon.color.r, icon.color.g, icon.color.b, show);
            }
        }
    }

}
