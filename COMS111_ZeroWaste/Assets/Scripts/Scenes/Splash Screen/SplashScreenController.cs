using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenController : SceneController {

    [Space]
    [Header("Unique Values")]
    [SerializeField]
    private TypeWriter typeWriter; // typewriter class attached to a gameobject

    // strats before start method
    void Awake()
    {
        // application details
        // for organization, this could be in preload scene
        Debug.Log(Application.productName + " has started running..."); // logs
        Debug.Log("Identifier: " + Application.identifier);
        Debug.Log("Version: " + Application.version);
        Debug.Log("Game Folder: " + Application.dataPath);
        
        Debug.Log("Device Details");
        Debug.Log("Platform: " + Application.platform);
        Debug.Log("System Language: " + Application.systemLanguage);
    }

    // for initialization
	void Start () {
        halt = false; // for skipping
        tempDelay = delay; // to retain delay value
	}

	void Update () {
        if (halt) Halt(); // check if user skips scene

        // checks if animation is fin
        if (typeWriter.typeFinished) 
        {
            if (tempDelay > 0)
            {
                tempDelay--;
                return;
            }

            Debug.Log("Scene Transition: FadeOut");
            // if delay is finished, load next scene
            fadeAnimator.SetTrigger("FadeOut"); // trigger fade out animation
            // instead of loading scene here, use animation event
        }
	}

    // stop all what's happening in the scene
    public void Halt()
    {
        Debug.Log("Tap to Skip is on. Skipping scene..."); // logs
        tempDelay = 0; // remove delay
        typeWriter.StopTyping(); // stop typewriter
    }

    // getters and setters
    public TypeWriter GetTypeWriter()
    {
        return typeWriter;
    }

    // by sh0
}
