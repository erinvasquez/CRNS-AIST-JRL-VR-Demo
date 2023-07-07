using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// With help from: https://www.youtube.com/watch?v=otDHGmncBQY&ab_channel=BakerFX
/// 
/// Controls a Saturation and Value UI
/// </summary>
public class SVIControl : MonoBehaviour, IDragHandler, IPointerClickHandler {

    [SerializeField] private Image pickerImage;
    private RawImage SVImage;
    private ColorPickerControl colorPickerControl;
    private RectTransform rectTransform, pickerCursorTransform;

    private void Awake() {

        SVImage = GetComponent<RawImage>();
        colorPickerControl = FindObjectOfType<ColorPickerControl>();
        rectTransform = GetComponent<RectTransform>();

        pickerCursorTransform = pickerImage.GetComponent<RectTransform>();

        // Set position of our color picker cursor relative to the UI, positioning
        // it to the bottom left of the UI area
        pickerCursorTransform.position = new Vector2(-(rectTransform.sizeDelta.x * 0.5f),
            -(rectTransform.sizeDelta.y * 0.5f));

    }

    /// <summary>
    /// Take some pointer event data and use the X and Y value
    /// to turn it into Saturation and Value data
    /// </summary>
    /// <param name="eventData"></param>
    void UpdateColor(PointerEventData eventData) {
        
        // Convert the screen position of our pointer event to the local position within our
        // Saturation and Value UI relative to its boundaries
        Vector3 pos = rectTransform.InverseTransformPoint(eventData.position);

        // Get our delta X and Y for clamping our pointer position
        float deltaX = rectTransform.sizeDelta.x * 0.5f;
        float deltaY = rectTransform.sizeDelta.y * 0.5f;

        // Check if our position values exceed our boundary limits,
        // if they do, clamp them!
        if (pos.x < -deltaX) {
            pos.x = -deltaX;
        } else if (pos.x > deltaX) {
            pos.x = deltaX;
        }

        if (pos.y < -deltaY) {
            pos.y = -deltaY;
        } else if (pos.y > deltaY) {
            pos.y = deltaY;
        }

        // Get our final position values to determin our saturation (xNorm) and value
        // values (yNorm)
        float x = pos.x + deltaX;
        float y = pos.y + deltaY;

        // Normalize our saturation and value
        float xNorm = x / rectTransform.sizeDelta.x;
        float yNorm = y / rectTransform.sizeDelta.y;

        // update our color picker cursor to our translated position
        pickerCursorTransform.localPosition = pos;

        // Set our color based on our normalized y value
        pickerImage.color = Color.HSVToRGB(0, 0, 1 - yNorm);

        // Set Saturation and Value based on our translated pointer data
        colorPickerControl.SetSV(xNorm, yNorm);

    }

    /// <summary>
    /// Call our Update Color method when dragging our pointer,
    /// or holding and moving the mouse
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData) {

        UpdateColor(eventData);

    }

    /// <summary>
    /// Calls our UpdateColor() method when clicking
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData) {

        UpdateColor(eventData);

    }

}
