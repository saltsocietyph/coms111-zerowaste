using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckSaveFileController : SceneController {

    // for initialization
	void Start () {
        tempDelay = delay; // store delay value to tempDelay
	}
	
	void Update () {
        if (tempDelay > 0)
        {
            tempDelay--;
            return;
        }
        // if delay is finished, load next scene
        fadeAnimator.SetTrigger("FadeOut"); // trigger fade out animation
        // instead of loading scene here, use animation event
	}

    // by sh0
}
