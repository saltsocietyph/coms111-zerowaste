using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypeWriter : MonoBehaviour {
    
    [SerializeField] // able to edit on inspector even if private
    private TextMeshProUGUI textDisplay; // ui display of text
    [SerializeField]
    [TextArea(1, 3)]
    private string textContent;
    [SerializeField]
    private int delay; // time before animation starts
    [SerializeField]
    private bool delayPerChar; // use delay for every character
    [SerializeField]
    private bool animFin = false; // animation state of logo

    [HideInInspector]
    public bool typeFinished = false; // typewritter animation state

    private char[] textDisplayArr; // array of char fr text
    private int index = 0; // used to loop around char array
    private int tempDelay = 0; // variable to --; instead of delay var
    private bool stopTyping = false; // skip animation to end

	void Start () {
        textDisplayArr = textContent.ToCharArray(); // convert to char array
        tempDelay = delay; // pass the value of delay to tempDelay
	}
	
	void Update () {
        if (!stopTyping) // check if user halts actions by tap
        {
            if (!animFin) return; // check if animation is finished

            // displays character per update
            if (index < textDisplayArr.Length)
            {
                if (tempDelay > 0) // delays animation at start
                {
                    tempDelay--;
                    return;
                }
                // if delay is finished
                textDisplay.text += textDisplayArr[index].ToString();
                index++;
                Debug.Log("Text Display: " + textDisplay.text);

                if (delayPerChar) // if has delay on every char
                    tempDelay = delay;

                Debug.Log("Type Writer animation on going...");
            }
            else
            {
                typeFinished = true; // value taken by a controller to 
                                     // go to next scene
            }
                
        }
	}

    // restart typewritter
    public void RestartTyping(string textContent)
    {
        typeFinished = false;

        // clear array with new content
        textDisplayArr = textContent.ToCharArray();
        textDisplay.text = ""; // empty display
        // reset counters
        tempDelay = delay;
        index = 0;
    }

    // stop typing
    public void StopTyping()
    {
        // skip to end of animation
        for (int i = 1; i < textDisplayArr.Length - (index - 1); i++)
        {
            textDisplay.text += textDisplayArr[index].ToString();
            index++;
        }
        // stop animation
        typeFinished = true;
        stopTyping = true; 
    }

    // animations
    // sets shake animation state to finished
    public void ShakeFinished()
    {
        animFin = true;
    }

    // by sh0
    // notes: made for the dev name on splash screen, but will
    // improve later on for dialogue system
}
