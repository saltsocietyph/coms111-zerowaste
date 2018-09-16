using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenController : SceneController {

    [Space]
    [Header("Unique Values")]
    [SerializeField]
    private TypeWriter typeWriter; // typewriter class attached to a gameobject

	void Start () {
        tempDelay = delay; // store delay value to tempDelay
	}

	void Update () {
        if (typeWriter.typeFinished) // checks if animation is fin
        {
            if (tempDelay > 0)
            {
                tempDelay--;
                return;
            }
            // if delay is finished, load next scene
            fadeAnimator.SetTrigger("FadeOut"); // trigger fade out animation
            // instead of loading scene here, use animation event
        }
	}

    // by sh0
}
