using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Handles UI controls for our simulate force sensors and our color picker.
/// Toggle visibility of the sensor controls and color picker UI based on Unity
/// input system events
/// </summary>
public class SensorUI : MonoBehaviour {

    [SerializeField] private GameObject sensorControlsUI;
    [SerializeField] private GameObject allForceControlsUI;
    [SerializeField] private GameObject colorPickerUI;

    /// <summary>
    /// Toggles our simulate force sensor controls UI. Called by the
    /// Input System and set in the editor with a Player Input.
    /// </summary>
    /// <param name="context"></param>
    public void ToggleSensorControlsUI(InputAction.CallbackContext context) {

        if(sensorControlsUI != null) {
            // Toggle visibility of the UI parent game object

            sensorControlsUI.SetActive(!sensorControlsUI.activeSelf);
            allForceControlsUI.SetActive(!allForceControlsUI.activeSelf);

        }

    }

    /// <summary>
    /// Toggles our color picker UI. Called by the Input System and set in
    /// the editor with a Player Input.
    /// </summary>
    /// <param name="context"></param>
    public void ToggleColorPickerUI(InputAction.CallbackContext context) {

        if (sensorControlsUI != null) {
            // Toggle visibility of the UI parent game object

            colorPickerUI.SetActive(!colorPickerUI.activeSelf);

        }

    }

}
