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

    private RectTransform rectTransform, pickerTransform;

    private void Awake() {

        SVImage = GetComponent<RawImage>();
        colorPickerControl = FindObjectOfType<ColorPickerControl>();
        rectTransform = GetComponent<RectTransform>();

        pickerTransform = pickerImage.GetComponent<RectTransform>();
        pickerTransform.position = new Vector2(-(rectTransform.sizeDelta.x * 0.5f),
            -(rectTransform.sizeDelta.y * 0.5f));

    }

    /// <summary>
    /// Take some pointer event data and use the X and Y value
    /// to turn it into Saturation and Value data
    /// </summary>
    /// <param name="eventData"></param>
    void UpdateColor(PointerEventData eventData) {
        Vector3 pos = rectTransform.InverseTransformPoint(eventData.position);

        float deltaX = rectTransform.sizeDelta.x * 0.5f;
        float deltaY = rectTransform.sizeDelta.y * 0.5f;

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

        float x = pos.x + deltaX;
        float y = pos.y + deltaY;

        float xNorm = x / rectTransform.sizeDelta.x;
        float yNorm = y / rectTransform.sizeDelta.y;

        pickerTransform.localPosition = pos;
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
