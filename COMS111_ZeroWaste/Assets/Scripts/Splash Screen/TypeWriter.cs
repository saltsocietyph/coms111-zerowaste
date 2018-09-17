using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypeWriter : MonoBehaviour {
    
    [SerializeField] // able to edit on inspector even if private
    private TextMeshProUGUI textDisplay; // ui display of text
    [SerializeField]
    private int delay; // time before animation starts
    [SerializeField]
    private bool delayPerChar; // use delay for every character

    private char[] textDisplayArr; // array of char fr text
    private int index = 0; // used to loop around char array
    private int tempDelay = 0; // variable to -- instead of delay var
    private bool animFinished = false; // animation state of loho
    public bool typeFinished = false; // typewritter animation state

	void Start () {
        string text = textDisplay.text; // get text fr display
        textDisplayArr = text.ToCharArray(); // convert to char array
        textDisplay.text = ""; // remove text fr display
        tempDelay = delay; // pass the value of delay to tempDelay
	}
	
	void Update () {
        // check if bounce animation is finished
        if (!animFinished)
            return;

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
            if (delayPerChar) // if has delay on every char
                tempDelay = delay;
        }
        else
            typeFinished = true; // value taken by a controller to 
                                 // go to next scene
	}


    // ANIMATIONS
    // sets bounce animation state to finished
    public void ShakeFinished()
    {
        animFinished = true;
    }

    // by sh0
    // notes: made for the dev name on splash screen, but will
    // improve later on for dialogue system
}
