using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using System;
using System.Linq;

/// <summary>
/// A test to recognize the default Windows speech language to control
/// the transform of the object this script is attached to via voice
/// </summary>
public class VoiceControlTestI : MonoBehaviour {

    private KeywordRecognizer keywordRecognizer;

    /// <summary>
    /// Our Dictionary of potential actions mapping speech keywords to our
    /// Left/Right functions
    /// </summary>
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();

    /// <summary>
    /// Set up our keyword recognizer
    /// </summary>
    void Start() {

        // English language actions
        actions.Add("Left", MoveLeft);
        actions.Add("Right", MoveRight);

        // Support for Windows Japanese language pack, if currently set to speech language
        // Hiragana and Kanji input to cover all bases
        actions.Add("ひだり", MoveLeft);
        actions.Add("みぎ", MoveRight);
        actions.Add("左", MoveLeft);
        actions.Add("右", MoveRight);

        // Also maritime directions, for our sailors
        actions.Add("Port", MoveLeft);
        actions.Add("Starboard", MoveRight);


        // Add our actions as an array to our keyword recognizer
        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());

        // Run the appropriate function based on the phrase recognized
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;

        // start our keyword recognizer!
        keywordRecognizer.Start();

    }

    /// <summary>
    /// Once a phrase is recognize, convert our speech to a string and print to Unity console
    /// </summary>
    /// <param name="speech"></param>
    private void RecognizedSpeech(PhraseRecognizedEventArgs speech) {

        Debug.Log(speech.text); // Print out what we said in our console

        // Invoke the appropriate method based on our added actions
        actions[speech.text].Invoke();

    }


    /// <summary>
    /// Move the transform attached to this script left one unit
    /// </summary>
    private void MoveLeft() {
        transform.Translate(Vector3.left);
    }

    /// <summary>
    /// Move the transform attached to this script right one unit
    /// </summary>
    private void MoveRight() {
        transform.Translate(Vector3.right);
    }

}
