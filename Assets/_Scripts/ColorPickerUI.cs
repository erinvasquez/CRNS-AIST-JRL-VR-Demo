using UnityEngine;

public class ColorPickerUI : MonoBehaviour {
    public static ColorPickerUI Instance { get; private set; }

    public GameObject colorPickerPanel;
    public UnityEngine.UI.Button colorPickerConfirmButton;
    public UnityEngine.UI.Button colorPickerCancelButton;
    public UnityEngine.UI.Image colorPickerPreviewImage;

    private System.Action<Color> colorSelectedCallback;
    private Color initialColor;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }

        colorPickerPanel.SetActive(false);
    }

    public void OpenColorPicker(Color initialColor, System.Action<Color> callback) {
        this.initialColor = initialColor;
        this.colorSelectedCallback = callback;
        colorPickerPreviewImage.color = initialColor;
        colorPickerPanel.SetActive(true);
    }

    public void ConfirmColorSelection() {
        colorPickerPanel.SetActive(false);
        colorSelectedCallback?.Invoke(colorPickerPreviewImage.color);
    }

    public void CancelColorSelection() {
        colorPickerPanel.SetActive(false);
        colorSelectedCallback?.Invoke(initialColor);
    }
}
