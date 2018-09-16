using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerAccess : MonoBehaviour {

    [SerializeField]
    private SceneController controller;

    // access function from another controller that
    // will be used in animation event
    // note: only attached scripts to gameobjects
    // with animator can be used in animation event
    public void LoadNextScene()
    {
        // load scene after fade out
        controller.LoadNextScene();
    }

    // by sh0
}
