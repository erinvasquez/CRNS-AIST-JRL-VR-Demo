using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// With help from: https://www.youtube.com/watch?v=otDHGmncBQY&ab_channel=BakerFX
/// 
/// Controls and handles our UI for our color picker, as well as sets
/// our Force Simulator sensor's low and high force values
/// </summary>
public class ColorPickerControl : MonoBehaviour {

    public float currentHue, currentSat, currentVal;

    [SerializeField] RawImage hueImage, satValImage, outputImage;
    [SerializeField] Slider hueSlider;
    [SerializeField] TMP_InputField hexInputField;
    private Texture2D hueTexture, svTexture, outputTexture;

    //[SerializeField] MeshRenderer objectToChangeColor;

    /// <summary>
    /// Currently selected color in out Color Picker UI
    /// </summary>
    [SerializeField] private Color currentColor;

    // Our Simulate Sensor arrows' colors
    [SerializeField] private Color lowForceColor;
    [SerializeField] private Color highForceColor;

    // Our local force sensor simulator whose colors we can change
    [SerializeField] private ForceSensorSimulator forceSensorSimulator;


    private void Start() {

        // Start by making our HSV image textures, our output image texture,
        // and by updating our Image and color values
        CreateHueImage();
        CreateSVImage();
        CreateOutputImage();
        UpdateOutputImageAndColor();

    }

    /// <summary>
    /// Create a Hue texture
    /// </summary>
    private void CreateHueImage() {

        // Create our Hue image texture
        hueTexture = new Texture2D(1, 16);
        hueTexture.wrapMode = TextureWrapMode.Clamp;
        hueTexture.name = "HueTexture";

        // Update our texture's pixels
        for (int i = 0; i < hueTexture.height; i++) {

            hueTexture.SetPixel(0, i, Color.HSVToRGB((float)i / hueTexture.height, 1, 0.5f));

        }

        // Apply our pixels to our texture
        hueTexture.Apply();

        // Reset our current Hue value
        currentHue = 0;

        // Update our hue image texture to our result
        hueImage.texture = hueTexture;

    }

    /// <summary>
    /// Create our Saturation/Value image texture based on our Hue value
    /// </summary>
    private void CreateSVImage() {
        
        // Create our SV image texture
        svTexture = new Texture2D(16, 16);
        svTexture.wrapMode = TextureWrapMode.Clamp;
        svTexture.name = "SatValTexture";

        // Update our texture with our hue value
        for (int y = 0; y < svTexture.height; y++) {

            for (int x = 0; x < svTexture.width; x++) {

                svTexture.SetPixel(x, y, Color.HSVToRGB(currentHue,
                    (float)x / svTexture.width,
                    (float)y / svTexture.height));

            }


        }

        // Apply our pixel changes
        svTexture.Apply();

        // Reset our current saturation and value
        currentSat = 0;
        currentVal = 0;

        // Update our Saturation/Value texture with our result
        satValImage.texture = svTexture;

    }

    /// <summary>
    /// Create our output image texture and update our currentColor
    /// </summary>
    private void CreateOutputImage() {

        // Create a new texture
        outputTexture = new Texture2D(1, 16);
        outputTexture.wrapMode = TextureWrapMode.Clamp;
        outputTexture.name = "OutputTexture";

        // Set a local current color based on our HSV values
        Color outputColor = Color.HSVToRGB(currentHue, currentSat, currentVal);

        // Use this output color to update our output texture
        for (int i = 0; i < outputTexture.height; i++) {

            outputTexture.SetPixel(0, i, outputColor);

        }

        // Apply our pixel changes to our texture
        outputTexture.Apply();

        // Set our output image's texture to our output texture
        outputImage.texture = outputTexture;

        // Update our hexInputField text to our output color
        hexInputField.text = ColorUtility.ToHtmlStringRGB(outputColor);

        // Update our global current color based on this output
        currentColor = outputColor;

    }

    

    /// <summary>
    /// Set our Saturation and Value, updating our current color and
    /// output image
    /// </summary>
    /// <param name="_newSaturation">Our new saturation value</param>
    /// <param name="_newValue">Our new brightness value</param>
    public void SetSV(float _newSaturation, float _newValue) {

        currentSat = _newSaturation;
        currentVal = _newValue;

        // Update our current color and output image
        UpdateOutputImageAndColor();

    }

    /// <summary>
    /// Update our Saturation/Value image based on our Hue value from
    /// our Hue slider
    /// </summary>
    public void UpdateSVImage() {
        
        // Get our hue value from our slider
        currentHue = hueSlider.value;

        // Update our Saturation/Value image's pixel
        for (int y = 0; y < svTexture.height; y++) {

            for (int x = 0; x < svTexture.width; x++) {

                svTexture.SetPixel(x, y, Color.HSVToRGB(
                    currentHue,
                    (float)x / svTexture.width,
                    (float)y / svTexture.height));

            }

        }

        // Apply our pixel changes
        svTexture.Apply();
        
        // Update our current color and output color image
        UpdateOutputImageAndColor();

    }

    /// <summary>
    /// Called by our Input Field when a new hex value is typed in,
    /// converts HEX to our new color and updates our color and output image
    /// </summary>
    public void OnTextInput() {

        if(hexInputField.text.Length < 6) {
            return;
        }

        Color newCol;

        if(ColorUtility.TryParseHtmlString("#" + hexInputField.text, out newCol)) {
            Color.RGBToHSV(newCol, out currentHue, out currentSat, out currentVal);
        }

        // Update our hue slider based on our new hex value
        hueSlider.value = currentHue;

        hexInputField.text = "";

        // Update our current color and output color image
        UpdateOutputImageAndColor();

    }

    /// <summary>
    /// Uses our calculated HSV values to output a UI texture showing
    /// the color we created
    /// </summary>
    private void UpdateOutputImageAndColor() {
        Color newColor = Color.HSVToRGB(currentHue, currentSat, currentVal);
        currentColor = newColor;

        // Set pixels for our output texture based on our new color
        for (int i = 0; i < outputTexture.height; i++) {

            outputTexture.SetPixel(0, i, newColor);

        }

        // Apply our pixel changes to our texture
        outputTexture.Apply();

        // Set our hex input field text to our hex value for this color
        hexInputField.text = ColorUtility.ToHtmlStringRGB(newColor);

        //objectToChangeColor.GetComponent<MeshRenderer>().material.color = newColor;

    }


    /// <summary>
    /// Grabs our current color in our color picker UI to our
    /// ForceSensorSimulator's Low Force color
    /// </summary>
    public void SetLowForceColor() {
        lowForceColor = currentColor;
        forceSensorSimulator.SetLowForceColor(lowForceColor);
    }

    /// <summary>
    /// Grabs our current color in our color picker UI to our
    /// ForceSensorSimulator's High Force color
    /// </summary>
    public void SetHighForceColor() {
        highForceColor = currentColor;
        forceSensorSimulator.SetHighForceColor(highForceColor);
    }

}