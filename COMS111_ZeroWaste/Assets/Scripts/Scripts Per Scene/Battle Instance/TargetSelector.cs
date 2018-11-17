using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TargetSelector : MonoBehaviour {

    [Header("Needs for Raycast")]
    public Camera targetCamera;

    [Space]
    [Header("Target UI")]
    public GameObject pointer;
    public TextMeshProUGUI description;
    public TextMeshProUGUI guideTextUI;
    [TextArea(3, 3)] public string guideText;
    
    [Space]
    [Header("Animations")]

    private GameObject selectedTarget;
    private GameObject previousTarget;
    private bool targetSelected = false;
    private bool targetLockedIn = false;
    private Vector3 pointerOrigin;
    
    void Start()
    {
        // Get pointer original coordinates so that it can be reset
        // everytime target selection is done
        pointerOrigin = pointer.transform.position;
    }

    void Update()
    {
        // Checks if the player is currently touching the screen
        if (Input.touchCount > 0)
        {
            // To prevent selection of target more than once in one
            // click, only call functions once touch has began
            if (TouchPhase.Began == Input.GetTouch(0).phase)
            {
                // Check if there's already a target, and
                // if the player is changing the target or locking in
                if (targetSelected && !targetLockedIn)
                {
                    // To check if player is locking in a target,
                    // store target in another variable...
                    previousTarget = selectedTarget;
                    SelectTarget();

                    // ... and compare if they are the same
                    ChangeGuideText(); 
                    if (selectedTarget.name == previousTarget.name)
                    {
                        ConfirmTarget();
                    }
                    else
                        Debug.Log("Selected another target!");
                }
                // If there's no target, let the player choose one
                else if(!targetSelected && !targetLockedIn)
                    SelectTarget();
            }
        }
    }

    // Draws a ray from camera to world where palyer has touched and captures
    // object (that has a collider) colliding the ray
    void SelectTarget()
    {
        // Always get the first touch, convert to world point, and make a Vector2 variable
        // Create a RaycastHit2D to capture objects on touch
        Vector2 origin = targetCamera.ScreenToWorldPoint(Input.GetTouch(0).position);
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.zero);
        
        // Checks if Raycast have hit something and has a value
        if (hit != null && hit.collider != null)
        {
            // Store the capture game object (character)
            selectedTarget = hit.transform.gameObject;
            targetSelected = true;
            Debug.Log("Selected: " + hit.transform.gameObject.name);

            // UX Functions
            PointTarget(); 
        }
    }

    // Changes [Choose Target] to [Lock In Target]
    void ChangeGuideText()
    {
        if (targetSelected)
        {
            guideTextUI.SetText(guideText);
            description.alpha = 1;
        }
    }

    // Points to whichever player selects as target
    void PointTarget()
    {
        BoxCollider2D collider = selectedTarget.GetComponent<BoxCollider2D>();
        float midpoint = collider.bounds.center.x;
        pointer.transform.position = new Vector3(midpoint, pointer.transform.position.y);
    }

    // Emphasizes chosen target
    void HighlightTarget()
    {
        // I dunno if I will use the Custom Shader Sprite Outline
    }

    // Confirms target, means cannot change anymore, and prepares for attack
    void ConfirmTarget()
    {
        // Prevents target selection from happening again
        targetLockedIn = true;
    }

    public void ResetTarget()
    {

    }

    public void DisableTarget()
    {

    }


    // Enables the panel that emphasizes the Targets
    public void EnableTargetSelection(bool enabled)
    {
        gameObject.SetActive(enabled);
    }

    // To make it more versatile, selection panel can change its tint
    // according to Ability selected
    public void ChangePanelTint(Color color)
    {
        gameObject.GetComponent<Image>().color = color;
    }

    // Resets pointer every time selection of target is canceled
    public void ResetPointer()
    {
        pointer.transform.position = pointerOrigin;
    }
}
