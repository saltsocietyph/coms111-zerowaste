using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

	// common values
    [Header("Common Values")]
    [SerializeField]
    public int delay; // delay in loading next scene
    [SerializeField]
    public int sceneToLoad; // build number of scene
    [Space]
    [SerializeField]
    public Animator fadeAnimator; // animator for fading of scenes

    [HideInInspector] // hidden fields
    public int tempDelay; // temporary storage for delay
    [HideInInspector]
    public bool halt; // for skipping actions

    // method to use in animation event
    public void LoadNextScene()
    {
        if (sceneToLoad == 0) // no scene to load
            return;
        SceneManager.LoadScene(sceneToLoad);
    }

    // by sh0
}
