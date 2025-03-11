using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SensorThresholdManager : MonoBehaviour
{
    [System.Serializable]
    public class Sensor
    {
        public string sensorName; // Name of the sensor
        public float threshold;  // Threshold for this sensor
        public GameObject popupPanel; // Pop-up Panel for this sensor
        public float currentValue; // Current value of the sensor (you can update this dynamically)
        public GameObject sensorObj;
    }

    public Sensor[] sensors;

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < sensors.Length; i++)
        {
            sensors[i].currentValue = sensors[i].sensorObj.GetComponent<RadiationDetection>().DetectedRadAmt;

            if (sensors[i].currentValue >= sensors[i].threshold)
            {
                ShowPopup(sensors[i].popupPanel, sensors[i].sensorName, sensors[i].currentValue);
            }
        }
    }

    void ShowPopup(GameObject popupPanel, string sensorName, float currentValue)
    {
        if (popupPanel != null && !popupPanel.activeSelf)
        {
            // Update the text component of the pop-up
            Text popupText = popupPanel.GetComponentInChildren<Text>();
            if (popupText != null)
            {
                popupText.text = $"{sensorName} Threshold Reached! Value: {currentValue.ToString("F2")}";
            }

            popupPanel.SetActive(true); // Show the pop-up
            StartCoroutine(HidePopupAfterDelay(popupPanel, 3f)); // Hide after 3 seconds
        }
    }

    IEnumerator HidePopupAfterDelay(GameObject popupPanel, float delay)
    {
        yield return new WaitForSeconds(delay);
        popupPanel.SetActive(false); // Hide the pop-up
    }
}
