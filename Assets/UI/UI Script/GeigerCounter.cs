using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeigerCounter : MonoBehaviour
{
    public Slider slider;
    public RadiationDetection radiationDetection;

    public RectTransform arrow; // Reference to the arrow's RectTransform
    public float arrowOffset = 10f; // Offset to adjust arrow position

    void Update()
    {
        

        // Update the arrow's position based on the slider value
        
    }

    void UpdateArrowPosition(float value)
    {
        // Calculate the normalized position of the arrow (0 to 1)
        float normalizedValue = Mathf.Clamp01(value / slider.maxValue);

        // Get the slider's track dimensions
        RectTransform sliderRect = slider.GetComponent<RectTransform>();
        float sliderWidth = sliderRect.rect.width;

        // Calculate the arrow's position along the slider's track
        float arrowX = normalizedValue * sliderWidth - (sliderWidth / 2f) + arrowOffset;

        // Update the arrow's anchored position
        arrow.anchoredPosition = new Vector2(arrowX, arrow.anchoredPosition.y);
    }

    public void receiveData(float value)
    {
        
        slider.value = value;

        UpdateArrowPosition(value);
    }
}