using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.InputSystem;

/// <summary>
/// Simulates multiple force sensors
/// </summary>
public class ForceSensorSimulator : MonoBehaviour {
    public GameObject arrowPrefab;  // Prefab for the arrow object

    [SerializeField] private Color lowForceColor = Color.green;  // Color for low force readings
    [SerializeField] private Color highForceColor = Color.red;  // Color for high force readings
    [SerializeField][Range(0f, 1f)] private float colorTransparency = 0.66f;

    [SerializeField] private float colorChangeThreshold = 25f;  // Threshold for color change

    [SerializeField] private SimulateSensor[] sensors = new SimulateSensor[0];

    private GameObject[] arrows = new GameObject[0]; // store instantiated arrow objects
    private bool updateArrowsNeeded = false; // flag to track if arrows need updating


    public TMP_Dropdown sensorDropdown;
    public Button updateSensorButton;
    public Button addSensorButton;
    public Button removeSensorButton;

    // Input fields to change position of currently selected sensor
    public TMP_InputField positionXInput;
    public TMP_InputField positionYInput;
    public TMP_InputField positionZInput;

    // Input fields to change force of currently selected sensor
    public TMP_InputField forceXInput;
    public TMP_InputField forceYInput;
    public TMP_InputField forceZInput;

    /// <summary>
    /// Feedback text showing all our sensors and their readings
    /// </summary>
    public TextMeshProUGUI sensorsText;

    // UI Controls to change all Force readings at once
    public Slider allForceXSlider;
    public Slider allForceYSlider;
    public Slider allForceZSlider;
    public TextMeshProUGUI allForceXValText;
    public TextMeshProUGUI allForceYValText;
    public TextMeshProUGUI allForceZValText;
    public Button setAllForcesButton;

    private void Start() {
        CreateArrows();
        UpdateSensorDataFields();
    }

    private void Update() {

        if (updateArrowsNeeded) {
            UpdateArrows();
            updateArrowsNeeded = false;
        }

    }

    private void CreateArrows() {
        // Destroy any existing arrow GameObjects
        foreach (GameObject arrow in arrows) {
            if (arrow != null) {
                Destroy(arrow);
            }
        }

        // Make an entirely new set of arrows
        arrows = new GameObject[sensors.Length];

        // Go through our list of sensors and create new arrows
        for (int i = 0; i < sensors.Length; i++) {
            SimulateSensor sensor = sensors[i];

            GameObject arrow = Instantiate(arrowPrefab, sensor.Position, Quaternion.identity);
            arrows[i] = arrow;
            arrow.transform.parent = transform;

            if (sensor.ForceReading == Vector3.zero) {
                // Set the force reading for the a default arrow
                sensor.ForceReading = new Vector3(1f, 1f, 1f);
            }

            // Set the scale of the arrow based on the initial force magnitude
            float initialForceMagnitude = sensor.ForceReading.magnitude;
            arrow.transform.localScale = new Vector3(initialForceMagnitude, initialForceMagnitude, initialForceMagnitude);


            // Change the color of the arrow based on the initial force magnitude
            Color arrowColor = GetArrowColor(initialForceMagnitude);
            arrowColor.a = colorTransparency;
            arrow.GetComponent<Renderer>().material.SetColor("_Color", arrowColor);


            // Orient the arrow based on force reading
            arrow.transform.forward = sensors[i].ForceReading.normalized;

        }

        // Update our UI
        UpdateSensorsText();
        UpdateSensorDropdownOptions();
    }

    public void TriggerUpdateArrows() {
        updateArrowsNeeded = true;
    }

    /// <summary>
    /// Update our arrows based on their sensor readings, as well as our UI
    /// text for all sensors and our input fields
    /// </summary>
    private void UpdateArrows() {
        int selectedIndex = sensorDropdown.value; // get the selected sensor

        for (int i = 0; i < sensors.Length; i++) {
            SimulateSensor sensor = sensors[i];

            // if the sensor has a zero force reading, just skip it
            if (sensor.ForceReading == Vector3.zero) {
                continue;
            }

            GameObject arrow = arrows[i];

            // If arrow isn't null
            if (arrow != null) {
                // Update position of arrow
                arrow.transform.position = sensor.Position;

                // Update scale of arrow based on force magnitude
                float forceMagnitude = sensor.ForceReading.magnitude;
                arrow.transform.localScale = Vector3.one * forceMagnitude;

                // Change the color of the arrow based on the force magnitude
                Color arrowColor = GetArrowColor(forceMagnitude);
                arrowColor.a = colorTransparency;
                arrow.GetComponent<Renderer>().material.SetColor("_Color", arrowColor);

                // Orient the arrow based on the force reading
                arrow.transform.forward = sensor.ForceReading.normalized;
            } else {
                // this sensor has no arrow as it had a zero-force reading before, make one
                // Create the arrow for the updated sensor arrow
                arrow = Instantiate(arrowPrefab, sensor.Position, Quaternion.identity);
                arrows[i] = arrow;
                arrow.transform.parent = transform;

                // Update scale of arrow based on force magnitude
                float forceMagnitude = sensor.ForceReading.magnitude;
                arrow.transform.localScale = Vector3.one * forceMagnitude;

                // Change the color of the arrow based on the force magnitude
                Color arrowColor = GetArrowColor(forceMagnitude);
                arrowColor.a = colorTransparency;
                arrow.GetComponent<Renderer>().material.SetColor("_Color", arrowColor);

                // Orient the arrow based on the force reading
                arrow.transform.forward = sensor.ForceReading.normalized;
            }

        }

        UpdateSensorsText();
    }


    public void AddSensor() {
        // Create a new sensor with zero values
        SimulateSensor newSensor = new SimulateSensor {
            Position = Vector3.zero,
            ForceReading = Vector3.zero,
            HasArrow = false // Set the flag to false initially
        };

        // Add the new sensor to the array
        Array.Resize(ref sensors, sensors.Length + 1);
        sensors[sensors.Length - 1] = newSensor;

        // Resize the arrows array to match the new sensor count
        Array.Resize(ref arrows, arrows.Length + 1);

        if (newSensor.ForceReading != Vector3.zero) {
            // Create the arrow for the new sensor only if it has a non-zero force reading
            GameObject arrow = Instantiate(arrowPrefab, newSensor.Position, Quaternion.identity);
            arrows[arrows.Length - 1] = arrow;
            arrow.transform.parent = transform;

            // Update scale, color, and orientation of the arrow based on the force reading
            float forceMagnitude = newSensor.ForceReading.magnitude;
            arrow.transform.localScale = Vector3.one * forceMagnitude;

            Color arrowColor = GetArrowColor(forceMagnitude);
            arrowColor.a = colorTransparency;
            arrow.GetComponent<Renderer>().material.SetColor("_Color", arrowColor);

            arrow.transform.forward = newSensor.ForceReading.normalized;

            newSensor.HasArrow = true; // Set the flag to true for sensors with arrows
        }

        // Update the arrows and UI elements
        UpdateArrows();
        UpdateSensorDropdownOptions();
    }


    public void UpdateSelectedSensor() {
        int selectedIndex = sensorDropdown.value;

        if (selectedIndex >= 0 && selectedIndex < sensors.Length) {
            // Retrieve position values from input fields
            if (float.TryParse(positionXInput.text, out float posX) &&
                float.TryParse(positionYInput.text, out float posY) &&
                float.TryParse(positionZInput.text, out float posZ)) {
                sensors[selectedIndex].Position = new Vector3(posX, posY, posZ);
            } else {
                Debug.Log("Parsing position values failed! TODO: Send an error message!");
            }

            // Retrieve force values from input fields
            if (float.TryParse(forceXInput.text, out float forceX) &&
                float.TryParse(forceYInput.text, out float forceY) &&
                float.TryParse(forceZInput.text, out float forceZ)) {
                sensors[selectedIndex].ForceReading = new Vector3(forceX, forceY, forceZ);
            } else {
                Debug.Log("Parsing force values failed! TODO: Send an error message!");
            }
        }

        // Update the arrows and UI elements
        if (arrows != null && arrows.Length > 0) // Add null and empty check for arrows array
        {
            UpdateArrows();
        }
        UpdateSensorsText();
    }


    /// <summary>
    /// Remove a sensor object at the selected dropdown value
    /// </summary>
    public void RemoveSensor() {
        int selectedIndex = sensorDropdown.value;

        // Remove the selected sensor from the array
        if (selectedIndex >= 0 && selectedIndex < sensors.Length) {
            // Destroy the arrow gameobject if it exists
            if (arrows[selectedIndex] != null) {
                Destroy(arrows[selectedIndex]);
            }

            // Shift the remaining sensors and arrows to fill the empty slot
            for (int i = selectedIndex; i < sensors.Length - 1; i++) {
                sensors[i] = sensors[i + 1];
                arrows[i] = arrows[i + 1];
            }

            // Resize the arrays to remove the last element
            Array.Resize(ref sensors, sensors.Length - 1);
            Array.Resize(ref arrows, arrows.Length - 1);

            // Update the selected index of the dropdown
            if (sensors.Length > 0) {
                if (selectedIndex >= sensors.Length) {
                    // If the removed sensor was the last one, set the selected index to the new last sensor
                    sensorDropdown.value = sensors.Length - 1;
                } else {
                    // Otherwise, keep the same selected index
                    sensorDropdown.value = selectedIndex;
                }
            } else {
                // If all sensors are removed, reset the selected index to -1
                sensorDropdown.value = -1;
            }
        }

        // Update the arrows and UI elements
        UpdateArrows();
        UpdateSensorsText();
        UpdateSensorDataFields();
        UpdateSensorDropdownOptions();
    }





    private void UpdateSensorDropdownOptions() {
        sensorDropdown.ClearOptions();

        for (int i = 0; i < sensors.Length; i++) {
            sensorDropdown.options.Add(new TMP_Dropdown.OptionData($"Sensor {i}"));
        }

        sensorDropdown.RefreshShownValue();

        // Update the sensor data fields with the newly selected sensor
        UpdateSensorDataFields();
    }

    /// <summary>
    /// Call this when an option is selected in our dropdown.
    /// Updates our input fields to have the data from the newly selected
    /// sensor.
    /// </summary>
    private void UpdateSensorDataFields() {
        int selectedIndex = sensorDropdown.value;

        if (selectedIndex >= 0 && selectedIndex < sensors.Length) {
            SimulateSensor selectedSensor = sensors[selectedIndex];

            // Update position input fields
            positionXInput.text = selectedSensor.Position.x.ToString();
            positionYInput.text = selectedSensor.Position.y.ToString();
            positionZInput.text = selectedSensor.Position.z.ToString();

            // Update force input fields
            forceXInput.text = selectedSensor.ForceReading.x.ToString();
            forceYInput.text = selectedSensor.ForceReading.y.ToString();
            forceZInput.text = selectedSensor.ForceReading.z.ToString();
        } else {
            // No sensor selected or out of range, clear input fields
            positionXInput.text = "";
            positionYInput.text = "";
            positionZInput.text = "";
            forceXInput.text = "";
            forceYInput.text = "";
            forceZInput.text = "";
        }
    }



    /// <summary>
    /// Updates our sensor list text on our UI to show the current sensor
    /// position and force readings
    /// </summary>
    private void UpdateSensorsText() {
        sensorsText.text = "";

        for (int i = 0; i < sensors.Length; i++) {

            sensorsText.text += $"Sensor {i}\tPos:({sensors[i].Position.x}, {sensors[i].Position.y}, {sensors[i].Position.z})" +
                $" Force:({sensors[i].ForceReading.x}, {sensors[i].ForceReading.y}, {sensors[i].ForceReading.z})\n";

        }

    }

    /// <summary>
    /// Updates UI showing X Value to be used when setting
    /// all sensors' force readings
    /// </summary>
    public void UpdateXValText() {
        allForceXValText.text = allForceXSlider.value.ToString();
    }

    /// <summary>
    /// Updates UI showing Y Value to be used when setting
    /// all sensors' force readings
    /// </summary>
    public void UpdateYValText() {
        allForceYValText.text = allForceYSlider.value.ToString();
    }

    /// <summary>
    /// Updates UI showing Z Value to be used when setting
    /// all sensors' force readings
    /// </summary>
    public void UpdateZValText() {
        allForceZValText.text = allForceZSlider.value.ToString();
    }

    /// <summary>
    /// iterates through all sensors and sets ALL forces based on our
    /// Set All Forces UI
    /// </summary>
    public void SetAllForces() {
        // Update the text values of the sliders
        UpdateXValText();
        UpdateYValText();
        UpdateZValText();

        // Create a new Vector3 using the values from the sliders
        Vector3 allForces = new(allForceXSlider.value, allForceYSlider.value, allForceZSlider.value);

        // Iterate through each sensor and set its force reading
        for (int i = 0; i < sensors.Length; i++) {
            SimulateSensor sensor = sensors[i];

            // Update the force reading for the current sensor
            sensor.ForceReading = allForces;
        }

        // Update all our arrows and UI elements
        UpdateSensorsText(); // Update the UI elements first
        UpdateArrows(); // Then update the arrows based on the updated force readings
    }

    private Color GetArrowColor(float forceMagnitude) {

        if (forceMagnitude >= colorChangeThreshold) {

            return highForceColor;
        } else {
            float t = Mathf.Clamp01(forceMagnitude / colorChangeThreshold);
            return Color.Lerp(lowForceColor, highForceColor, t);
        }

    }

    /// <summary>
    /// Sets a new low force color
    /// </summary>
    /// <param name="_newColor"></param>
    public void SetLowForceColor(Color _newColor) {
        lowForceColor = _newColor;

        // Update the color of each arrow
        foreach (GameObject arrow in arrows) {
            if (arrow != null) {
                Color arrowColor = GetArrowColor(arrow.transform.localScale.magnitude);
                arrowColor.a = colorTransparency;
                arrow.GetComponent<Renderer>().material.SetColor("_Color", arrowColor);
            }
        }

    }

    /// <summary>
    /// Sets a new high force color
    /// </summary>
    /// <param name="_newColor"></param>
    public void SetHighForceColor(Color _newColor) {
        highForceColor = _newColor;

        // Update the color of each arrow
        foreach (GameObject arrow in arrows) {
            if (arrow != null) {
                Color arrowColor = GetArrowColor(arrow.transform.localScale.magnitude);
                arrowColor.a = colorTransparency;
                arrow.GetComponent<Renderer>().material.SetColor("_Color", arrowColor);
            }
        }

    }

}
