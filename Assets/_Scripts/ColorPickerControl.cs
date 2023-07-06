using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

/// <summary>
/// With help from: https://www.youtube.com/watch?v=otDHGmncBQY&ab_channel=BakerFX
/// </summary>
public class ColorPickerControl : MonoBehaviour {

    public float currentHue, currentSat, currentVal;

    [SerializeField] RawImage hueImage, satValImage, outputImage;

    [SerializeField] Slider hueSlider;

    [SerializeField] TMP_InputField hexInputField;

    private Texture2D hueTexture, svTexture, outputTexture;

    //[SerializeField] MeshRenderer objectToChangeColor;


    [SerializeField] private Color currentColor;

    // Now to connect to our Simulate Sensor arrows
    [SerializeField] private Color lowForceColor;
    [SerializeField] private Color highForceColor;

    [SerializeField] private ForceSensorSimulator forceSensorSimulator;


    private void Start() {

        CreateHueImage();
        CreateSVImage();
        CreateOutputImage();
        UpdateOutputImage();

    }

    private void CreateHueImage() {
        hueTexture = new Texture2D(1, 16);
        hueTexture.wrapMode = TextureWrapMode.Clamp;
        hueTexture.name = "HueTexture";

        for (int i = 0; i < hueTexture.height; i++) {

            hueTexture.SetPixel(0, i, Color.HSVToRGB((float)i / hueTexture.height, 1, 0.5f));

        }

        hueTexture.Apply();
        currentHue = 0;

        hueImage.texture = hueTexture;

    }

    private void CreateSVImage() {
        svTexture = new Texture2D(16, 16);
        svTexture.wrapMode = TextureWrapMode.Clamp;
        svTexture.name = "SatValTexture";

        for (int y = 0; y < svTexture.height; y++) {

            for (int x = 0; x < svTexture.width; x++) {

                svTexture.SetPixel(x, y, Color.HSVToRGB(currentHue,
                    (float)x / svTexture.width,
                    (float)y / svTexture.height));

            }


        }

        svTexture.Apply();
        currentSat = 0;
        currentVal = 0;

        satValImage.texture = svTexture;

    }

    private void CreateOutputImage() {

        outputTexture = new Texture2D(1, 16);
        outputTexture.wrapMode = TextureWrapMode.Clamp;
        outputTexture.name = "OutputTexture";

        Color currentColor = Color.HSVToRGB(currentHue, currentSat, currentVal);

        for (int i = 0; i < outputTexture.height; i++) {

            outputTexture.SetPixel(0, i, currentColor);

        }

        outputTexture.Apply();
        outputImage.texture = outputTexture;

    }

    

    public void SetSV(float s, float v) {

        currentSat = s;
        currentVal = v;

        UpdateOutputImage();

    }

    public void UpdateSVImage() {
        currentHue = hueSlider.value;

        for (int y = 0; y < svTexture.height; y++) {

            for (int x = 0; x < svTexture.width; x++) {

                svTexture.SetPixel(x, y, Color.HSVToRGB(
                    currentHue,
                    (float)x / svTexture.width,
                    (float)y / svTexture.height));

            }

        }

        svTexture.Apply();

        UpdateOutputImage();

    }

    public void OnTextInput() {

        if(hexInputField.text.Length < 6) {
            return;
        }

        Color newCol;

        if(ColorUtility.TryParseHtmlString("#" + hexInputField.text, out newCol)) {
            Color.RGBToHSV(newCol, out currentHue, out currentSat, out currentVal);
        }

        hueSlider.value = currentHue;

        hexInputField.text = "";

        UpdateOutputImage();

    }

    private void UpdateOutputImage() {
        Color newColor = Color.HSVToRGB(currentHue, currentSat, currentVal);

        for (int i = 0; i < outputTexture.height; i++) {

            outputTexture.SetPixel(0, i, newColor);

        }

        outputTexture.Apply();

        hexInputField.text = ColorUtility.ToHtmlStringRGB(newColor);

        //objectToChangeColor.GetComponent<MeshRenderer>().material.color = newColor;

        currentColor = newColor;
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