using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SensorUI : MonoBehaviour {

    [SerializeField] private GameObject sensorControlsUI;
    [SerializeField] private GameObject allForceControlsUI;
    [SerializeField] private GameObject colorPickerUI;

    public void ToggleSensorControlsUI(InputAction.CallbackContext context) {

        if(sensorControlsUI != null) {
            // Toggle visibility of the UI parent game object

            sensorControlsUI.SetActive(!sensorControlsUI.activeSelf);
            allForceControlsUI.SetActive(!allForceControlsUI.activeSelf);

        }

    }

    public void ToggleColorPickerUI(InputAction.CallbackContext context) {

        if (sensorControlsUI != null) {
            // Toggle visibility of the UI parent game object

            colorPickerUI.SetActive(!colorPickerUI.activeSelf);

        }

    }

}
