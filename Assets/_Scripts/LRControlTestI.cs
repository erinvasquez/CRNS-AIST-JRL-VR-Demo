using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Linq;
using UnityEngine.Windows.Speech;
using TMPro;

/// <summary>
/// Shows a Canvas UI with on-screen controls and a feedback text display on input.
/// Activates on voice input for Left or Right in English, Japanese, and Maritime sailor-speak,
/// mouse left click, the "L" or "R" key, the Left or Right arrow keys, and the left and right
/// triggers on an HTC Vive XR Controller.
/// TODO: add all XR controller trigger buttons.
/// 
/// Hides the UI after a certain amount of seconds
/// </summary>
public class LRControlTestI : MonoBehaviour {

    /// <summary>
    /// Our UI gameobject
    /// </summary>
    public GameObject leftRightUI;

    /// <summary>
    /// Our VR UI gameobject
    /// </summary>
    public GameObject leftRightVRUI;

    /// <summary>
    /// Our TMPro text object
    /// </summary>
    public TextMeshProUGUI leftRightText;

    /// <summary>
    /// Our VR TMPro text ojbect
    /// </summary>
    public TextMeshProUGUI leftRightVRText;


    /// <summary>
    /// Our Windows speech keyword recognizer
    /// </summary>
    private KeywordRecognizer keywordRecognizer;

    /// <summary>
    /// A reference to our current hideCanvas coroutine
    /// </summary>
    private Coroutine hideCanvasCoroutine;

    /// <summary>
    /// The amount of time in seconds it takes for our UI
    /// </summary>
    public float UIHideTime = 5f;

    /// <summary>
    /// Our Dictionary of potential actions mapping speech keywords to our
    /// Left/Right functions
    /// </summary>
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();

    /// <summary>
    /// Set up our keyword recognizer
    /// TODO: move some code to Awake?
    /// </summary>
    void Start() {

        // Disable our UIs from the start
        leftRightUI.SetActive(false);
        leftRightVRUI.SetActive(false);

        // Show all our Microphone devices
        /*
        foreach (var device in Microphone.devices) {
           Debug.Log("Name: " + device);
        }
        */

        // English language actions
        actions.Add("Left", Left);
        actions.Add("Right", Right);

        // Support for Windows Japanese language pack, if currently set to speech language
        // Hiragana and Kanji input to cover all bases
        actions.Add("ひだり", Left);
        actions.Add("みぎ", Right);
        actions.Add("左", Left);
        actions.Add("右", Right);

        // Also maritime directions, for our sailors
        actions.Add("Port", Left);
        actions.Add("Starboard", Right);


        // Add our actions as an array to our keyword recognizer
        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());

        // Run the appropriate function based on the phrase recognized
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;

        // start our keyword recognizer!
        keywordRecognizer.Start();

    }

    /// <summary>
    /// Set our canvas UI to true if it hasn't already and start a coroutine
    /// to hide it
    /// </summary>
    public void ShowUI() {
        leftRightUI.SetActive(true);
        leftRightVRUI.SetActive(true);
        //StartCoroutine(HideCanvasAfterDelay(5f));

        if (hideCanvasCoroutine != null) {
            StopCoroutine(hideCanvasCoroutine);
        }

        // Set the coroutine we want to start, and start it
        hideCanvasCoroutine = StartCoroutine(HideCanvasAfterDelay(UIHideTime));
    }

    /// <summary>
    /// Hide our canvas UI after a specific float time seconds
    /// </summary>
    /// <param name="delay">Time in seconds it takes to hide UI</param>
    /// <returns></returns>
    private IEnumerator HideCanvasAfterDelay(float delay) {

        yield return new WaitForSeconds(delay);

        // Reset our text
        leftRightText.text = "";
        leftRightVRText.text = "";

        // Disable our UI
        leftRightUI.SetActive(false);
        leftRightVRUI.SetActive(false);

        //set our coroutine reference to null
        hideCanvasCoroutine = null;
    }

    /// <summary>
    /// Once a phrase is recognized, convert our speech to a string
    /// </summary>
    /// <param name="speech">Windows speech recognized phrase data</param>
    private void RecognizedSpeech(PhraseRecognizedEventArgs speech) {
        
        // Print out what we said in our console
        // Debug.Log(speech.text); 

        // Invoke the appropriate method based on our added actions
        actions[speech.text].Invoke();

        // activate our canvas when we get our speech input too
        ShowUI();

    }

    /// <summary>
    /// Callback function for left input
    /// </summary>
    /// <param name="context"></param>
    public void LeftInput(InputAction.CallbackContext context) {
        //Debug.Log("Left button pressed, performed?...");

        // If our button was performed, do our Left function
        if (context.performed) {
            Left();

        }

    }

    /// <summary>
    /// Callback function for right input
    /// </summary>
    /// <param name="context"></param>
    public void RightInput(InputAction.CallbackContext context) {
        //Debug.Log("Right button pressed, performed?...");

        // if our button was performed, do our Right function
        if (context.performed) {
            Right();
        }

    }

    /// <summary>
    /// Show our canvas while doing our left button action
    /// </summary>
    public void Left() {
        Debug.Log("Left performed!");

        // activate our canvas
        ShowUI();

        // Set our text to left
        leftRightText.text = "Left";
        leftRightVRText.text = "Left";
    }



    /// <summary>
    /// Show our canvas while doing our right button action
    /// </summary>
    public void Right() {
        Debug.Log("Right performed!");

        // activate our canvas
        ShowUI();

        // Set our text to right
        leftRightText.text = "Right";
        leftRightVRText.text = "Right";
    }

}
