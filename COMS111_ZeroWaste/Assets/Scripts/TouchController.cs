using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour {

    [Header("Common Values")]
    [SerializeField]
    private ParticleSystem effectsOnTouch;
    [SerializeField]
    private Camera effectsCam;

    private Touch touch; // holds touch values

	// for initialization
	void Start () {
		
	}
	
	void Update () {
        // clear touch value
        touch = new Touch();

		// check if user is touching screen
        if (Input.touchCount > 0)
        {
            // get first touch
            touch = Input.GetTouch(0);
            EmitParticles();
        }
	}

    private void EmitParticles()
    {
        Vector2 touchPos = new Vector2(); // touch position
        Vector3 point = new Vector3(); // point where particles emit

        // get position of touch
        touchPos.x = touch.position.x;
        touchPos.y = touch.position.y;
        Debug.Log(touch.position); // debugging

        // to keep particles inside camera bounds
        point = effectsCam.ScreenToWorldPoint(new Vector3(touchPos.x, 
            touchPos.y, effectsCam.nearClipPlane));

        // place particle on touch position
        effectsOnTouch.transform.position = point;
        effectsOnTouch.Play(); // particle is burst
    }
}
