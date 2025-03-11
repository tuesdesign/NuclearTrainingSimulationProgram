using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GeigerCounter : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public RadiationDetection radiationDetection;

    void Start()
    {
        GradientStuff();
    }

    public void GradientStuff()
    {
        slider.maxValue = 10f;
        slider.value = 0f;
        fill.color = gradient.Evaluate(0f);
    }

    void Update()
    {
        float detectedRadAmt = radiationDetection.DetectedRadAmt;
        slider.value = detectedRadAmt;

        // Normalize the detected radiation amount to a value between 0 and 1
        float normalizedValue = Mathf.Clamp01(detectedRadAmt / slider.maxValue);

        // Set the fill color based on the normalized value
        fill.color = gradient.Evaluate(normalizedValue);
    }
}