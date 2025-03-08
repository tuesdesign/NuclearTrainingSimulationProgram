using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraManager : MonoBehaviour
{
    //Cameras
    public Camera mainCam;
    public Camera[] topDownCams;
    public Camera[] perspectiveCams;

    //Buttons
    public Button[] camButtons;
    public Button backToMainButton;
    public Button toggleViewButton;

    private int currentCameraIndex = -1;

    void Start()
    {
        //Enable main camera, Disable other cameras
        mainCam.gameObject.SetActive(true);
        DisableAllCameras();

        //Disable specific buttons
        backToMainButton.gameObject.SetActive(false);
        toggleViewButton.gameObject.SetActive(false);

        //Listen to active camera buttons
        for (int i = 0; i < camButtons.Length; i++)
        {
            int camIndex = i;
            if (camIndex < topDownCams.Length)
            {
                camButtons[camIndex].onClick.AddListener(() => SwitchCamera(camIndex));
            }
        }

        backToMainButton.onClick.AddListener(SwitchToMain);
        toggleViewButton.onClick.AddListener(ToggleView);
    }

    private void DisableAllCameras()
    {
        //Disable every other camera
        foreach (Camera c in topDownCams)
        {
            c.gameObject.SetActive(false);
        }

        foreach (Camera c in perspectiveCams)
        {
            c.gameObject.SetActive(false);
        }
    }

    private void ToggleView()
    {
        if (currentCameraIndex == -1) return;

        //If toggle view button is pressed, switch camera perspectives
        if (topDownCams[currentCameraIndex].gameObject.activeSelf)
        {
            topDownCams[currentCameraIndex].gameObject.SetActive(false);
            perspectiveCams[currentCameraIndex].gameObject.SetActive(true);
        }
        else
        {
            perspectiveCams[currentCameraIndex].gameObject.SetActive(false);
            topDownCams[currentCameraIndex].gameObject.SetActive(true);
        }
    }

    private void SwitchToMain()
    {
        //Disable all other cameras
        DisableAllCameras();

        //Enable main camera
        mainCam.gameObject.SetActive(true);

        //Disable specific buttons
        backToMainButton.gameObject.SetActive(false);
        toggleViewButton.gameObject.SetActive(false);

        //Enable camera buttons
        foreach (Button b in camButtons)
        {
            b.gameObject.SetActive(true);
        }

        currentCameraIndex = -1;
    }

    private void SwitchCamera(int camIndex)
    {
        //Disable main camera
        mainCam.gameObject.SetActive(false);
        DisableAllCameras();

        //Enable specified camera
        topDownCams[camIndex].gameObject.SetActive(true);

        currentCameraIndex = camIndex;

        //Enable back and toggle view buttons
        backToMainButton.gameObject.SetActive(true);
        toggleViewButton.gameObject.SetActive(true);

        //Disable Camera buttons
        foreach (Button b in camButtons)
        {
            b.gameObject.SetActive(false);
        }
    }
}
