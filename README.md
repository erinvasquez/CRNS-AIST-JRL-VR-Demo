# CRNS-AIST-JRL-VR-Demo
 CRNS AIST JRL VR Assignment for VR Software Engineer interview. This project consists of two main
 tasks: Task 1 and Task 2. Each task involves different scripts and functionalities within the project.

YouTube Link to our Demo: https://youtu.be/CUNNj6h22HU

## Task 1: Voice and Input Control
Task 1 focuses on implementing voice and input control to interact with a UI element that indicates
left or right based on the input received. The scripts involved in this task are
VoiceControlTestI.cs and LRControlTestI.cs.

### VoiceControlTestI.cs
The VoiceControlTestI script allows the user to control the transform of an object via voice
commands. It recognizes specific keywords in different languages, such as English, Japanese,
and maritime terms, to perform the left or right movement. The script utilizes the
KeywordRecognizer class from the Windows Speech API to recognize and map speech keywords to
corresponding actions. When a recognized phrase is detected, the script invokes the appropriate
method to move the object accordingly.

### LRControlTestI.cs
The LRControlTestI script is responsible for displaying a Canvas UI with on-screen controls and
a feedback text display. It activates in response to voice input for left or right commands, mouse
left click, specific keyboard keys, arrow keys, or left and right triggers on an HTC Vive XR
Controller. The UI is shown for a certain duration and then hidden automatically. The script uses
the KeywordRecognizer to recognize voice commands and updates the UI accordingly.

To control the UI, the script provides various methods such as ShowUI() to show the UI,
HideCanvasAfterDelay() to hide the UI after a specified delay, and RecognizedSpeech() to handle
recognized speech phrases and update the UI elements accordingly.


## Task 2: Force Sensors and UI Controls
Task 2 focuses on simulating force sensors and providing UI controls to manipulate and visualize
the sensors. The scripts involved in this task are ColorPickerControl.cs, SVIControl.cs,
ForceSensorSimulator.cs, SimulateSensor.cs, and SensorUI.cs.

### ForceSensorSimulator.cs
The ForceSensorSimulator script simulates multiple force sensors in the scene. It provides
functionality to create arrow objects representing the sensors and update their positions, forces,
and visual representation. The script includes methods to add, remove, and update sensors, as well
as update the arrows and UI elements associated with the sensors. It utilizes the SimulateSensor
class to represent individual sensors and communicates with other UI scripts to control the sensors.

### SimulateSensor.cs
The SimulateSensor class represents a simulated force sensor unit. It holds information about the
sensor's position and force reading. The class provides methods to update the position and force
values of the sensor.

### SensorUI.cs
The SensorUI script handles the UI controls for the force sensors and color picker. It toggles the
visibility of the sensor controls UI and the color picker UI based on input events. The script
utilizes the InputSystem package to detect input actions and control the UI visibility.

### ColorPickerControl.cs
The ColorPickerControl script is responsible for controlling a color picker UI. It allows the user
to select a color by manipulating the hue, saturation, and value components. The script updates the
color picker UI based on user input and provides methods to retrieve the selected color. It
utilizes textures and UI elements to display the color picker and interacts with the
ColorPickerControl UI.

### SVIControl.cs
The SVIControl script works in conjunction with the color picker UI to control the saturation and
value components. It allows the user to click or drag on the UI to update the saturation and value
values, which in turn update the color picker UI. The script implements the IDragHandler and
IPointerClickHandler interfaces to detect input events and update the color picker accordingly.


## Conclusion
Task 1 focuses on implementing voice and input control for left and right movements, while Task 2
involves simulating force sensors and providing UI controls for manipulating and visualizing the
sensors. These tasks combine various scripts and UI elements to create an interactive and versatile
system for controlling objects and managing sensor data.


## Editor Installation

To run this Unity project, follow these steps:

### 1) Clone the Respository:
Click on the "Code" button and copy the repository URL. Open a terminal or command prompt on your
local machine, navigate to the desired directory, and run the following command:

` git clone https://github.com/erinvasquez/CRNS-AIST-JRL-VR-Demo.git `

This will clone the repository to your local machine.

OR

Click on the "Code" button and download the ZIP file and save it to the desired directory, and
unzip the file.

Included will be all the project files, as well as a .UnityPackage file that can
be imported into the Unity Editor.

### 2) Open the project in Unity:
Launch the Unity Editor on your computer. Click on "Open" or "Load" in the Unity Editor and navigate
to the directory where you cloned the repository. Select the Unity project folder (it should contain
the Assets, Library, and ProjectSettings folders) and click "Open".

### 3) Resolve Dependencies:
Once the project is open in Unity, the editor will start importing and compiling the necessary assets
and scripts. If there are any missing dependencies or errors, Unity will prompt you to resolve them.
Follow the on-screen instructions to resolve any dependencies and ensure the project compiles
successfully.

### 4) Set the Scene:
In the Unity Editor, navigate to the "Project" window (usually located in the bottom-left corner).
Expand the folder structure and find the main scene file **(VRDemoScene1.unity)**. Double-click
on the scene file to open it in the Scene View.

### 5) Run the Project:
Press the "Play" button in the Unity Editor toolbar (or use the shortcut Ctrl + P) to run the
project. Unity will compile the scripts and assets and launch the project in Play Mode. Note:
Make sure you have the necessary hardware or plugins configured to suport the specific input
methods used in the project, such as VR controllers or voice input devices.

## Unity Package installation:

### 1) Clone the Respository:
Click on the "Code" button and copy the repository URL. Open a terminal or command prompt on your
local machine, navigate to the desired directory, and run the following command:

` git clone https://github.com/erinvasquez/CRNS-AIST-JRL-VR-Demo.git `

This will clone the repository to your local machine.

OR

Click on the "Code" button and download the ZIP file and save it to the desired directory, and
unzip the file.

Included will be all the project files, as well as a .UnityPackage file that can
be imported into the Unity Editor.

### 2) Import the .UnityPackage file

Import the .UnityPackage file into your Unity project by going to: "Assets" -> "Import Package"
-> "Custom Package" -> Select the Unity Package file.

# Application Instructions

Upon running the application, Tasks 1 and 2 can be accomplished and controlled using the
following instructions.

## Task 1 Instructions:
In the scene is included 5 default Force Simulate Sensors positioned at the world origin with
varying default position and force readings, as well as a UI at the top left corner of the
Desktop window showing each sensors' name, position, and force reading.

To toggle the hidden Sensor controls, press **S**. Controls for the sensors will appear, including
- A sensor selection dropdown
- A button to add remove the selected sensor
- A button to add a new sensor at Position (0,0,0) and Force reading (0,0,0)
- Input fields to edit the selected sensor's Position and Force readings
- A button to update the selected sensor with the inputted Position and Force readings
- A color change threshold slider to determine at what Force magnitude our sensor's readings are considered "High Force"
- X, Y, and Z Force sliders to change force readings for **all** sensors at once
- A button to set **all** sensors' force readings

To toggle the hidden Low/High Force Color Picker controls, press **P**. A color picker UI will appear,
which allows you to choose a certain Saturation and Value using the UI cursor, as well as the desired
Hue. On the bottom left, a hex value will also be shown for the current color, and can be edited by
typing in your own hexadecimal color value.

Once a color is chosen, an ouput color is shown on the right-most side, and can be used to set
all simulated sensors' low and high force colors using the appropriate buttons on the top-right of the
UI.

## Task 2 Instructions:
In order to accomlish task 2, the desktop can be clicked at any point in time to show the Left/Right
controls UI. Pressing **L**, **R**, **Left Arrow Key**, **Right Arrow Key**, or **Left Trigger**
and **Right Trigger** on an VR/XR controller, or using a microphone to say "Left" or "Right" will
show the Left/Right controls UI. Any one of these inputs will show a feedback for a Left or Right action that was input.
Once the Left/Right controls UI is active and shown, you can also use the mouse on the desktop
to click on the Left and Right buttons to trigger Left or Right actions. If 5 seconds have passed, the UI
will automatically be hidden, and another input must be made to show the Left/Right feedback UI once again,
and you can continue to send Left/Right inputs in any desired fashion. Voice input may be made in English
("Left" and "Right"), Japanese ("みぎ/右" and "ひだり/左"), and in maritime directions (i.e. "Port" and
"Starboard").


# Credits:
For the Color Picker UI implementation, Baker FX's tutorial on
YouTube: https://www.youtube.com/watch?v=otDHGmncBQY&ab_channel=BakerFX
