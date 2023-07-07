# CRNS-AIST-JRL-VR-Demo
 CRNS AIST JRL VR Assignment for VR Software Engineer interview. Accomplishes Task 1 & 2 assignments to showcase dummy Force sensors with controls, VR capabilities, Input control, and voice recognition

## Project Readme
This project consists of two main tasks: Task 1 and Task 2. Each task involves different scripts and functionalities within the project.

## Task 1: Voice and Input Control
Task 1 focuses on implementing voice and input control to interact with a UI element that indicates left or right based on the input received. The scripts involved in this task are VoiceControlTestI.cs and LRControlTestI.cs.

### VoiceControlTestI.cs
The VoiceControlTestI script allows the user to control the transform of an object via voice commands. It recognizes specific keywords in different languages, such as English, Japanese, and maritime terms, to perform the left or right movement. The script utilizes the KeywordRecognizer class from the Windows Speech API to recognize and map speech keywords to corresponding actions. When a recognized phrase is detected, the script invokes the appropriate method to move the object accordingly.

### LRControlTestI.cs
The LRControlTestI script is responsible for displaying a Canvas UI with on-screen controls and a feedback text display. It activates in response to voice input for left or right commands, mouse left click, specific keyboard keys, arrow keys, or left and right triggers on an HTC Vive XR Controller. The UI is shown for a certain duration and then hidden automatically. The script uses the KeywordRecognizer to recognize voice commands and updates the UI accordingly.

To control the UI, the script provides various methods such as ShowUI() to show the UI, HideCanvasAfterDelay() to hide the UI after a specified delay, and RecognizedSpeech() to handle recognized speech phrases and update the UI elements accordingly.

## Task 2: Force Sensors and UI Controls
Task 2 focuses on simulating force sensors and providing UI controls to manipulate and visualize the sensors. The scripts involved in this task are ColorPickerControl.cs, SVIControl.cs, ForceSensorSimulator.cs, SimulateSensor.cs, and SensorUI.cs.

### ColorPickerControl.cs
The ColorPickerControl script is responsible for controlling a color picker UI. It allows the user to select a color by manipulating the hue, saturation, and value components. The script updates the color picker UI based on user input and provides methods to retrieve the selected color. It utilizes textures and UI elements to display the color picker and interacts with the ColorPickerControl UI.

### SVIControl.cs
The SVIControl script works in conjunction with the color picker UI to control the saturation and value components. It allows the user to click or drag on the UI to update the saturation and value values, which in turn update the color picker UI. The script implements the IDragHandler and IPointerClickHandler interfaces to detect input events and update the color picker accordingly.

### ForceSensorSimulator.cs
The ForceSensorSimulator script simulates multiple force sensors in the scene. It provides functionality to create arrow objects representing the sensors and update their positions, forces, and visual representation. The script includes methods to add, remove, and update sensors, as well as update the arrows and UI elements associated with the sensors. It utilizes the SimulateSensor class to represent individual sensors and communicates with other UI scripts to control the sensors.

### SimulateSensor.cs
The SimulateSensor class represents a simulated force sensor unit. It holds information about the sensor's position and force reading. The class provides methods to update the position and force values of the sensor.

### SensorUI.cs
The SensorUI script handles the UI controls for the force sensors and color picker. It toggles the visibility of the sensor controls UI and the color picker UI based on input events. The script utilizes the InputSystem package to detect input actions and control the UI visibility.

## Conclusion
Task 1 focuses on implementing voice and input control for left and right movements, while Task 2 involves simulating force sensors and providing UI controls for manipulating and visualizing the sensors. These tasks combine various scripts and UI elements to create an interactive and versatile system for controlling objects and managing sensor data.