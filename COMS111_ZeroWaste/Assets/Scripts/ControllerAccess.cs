using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerAccess : MonoBehaviour {

    [SerializeField]
    private SceneController sceneController;

    private GameObject controllerObj; // gameobject that holds all controllers
    static private string OBJ_TAG = "Controller"; // tag to find controller

    // get the controller object
    public void GetController()
    {
        // find gameobject by tag
        controllerObj = GameObject.FindWithTag(OBJ_TAG);
    }

    // access function from another controller that
    // will be used in animation event
    // note: only attached scripts to gameobjects
    // with animator can be used in animation event
    public void LoadNextScene()
    {
        // load scene after fade out
        sceneController.LoadNextScene();
    }

    // note! function seems sketchy, will have to revise this later
    // access typewriter function
    public void ShakeFinished()
    {
        // get the controller object
        GetController();
        // get typewriter class through scene controller class
        TypeWriter typeWriter = controllerObj.GetComponent<SplashScreenController>().GetTypeWriter();
        typeWriter.ShakeFinished();
        
    }

    // by sh0
}
