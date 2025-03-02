using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayUpdate : MonoBehaviour
{
    public Material[] screens;
    public float changeFrequency = 5.0f;

    private int currentScreen = 0;
    private float timer = 0.0f;

    // Use this for initialization
    void Start()
    {
        timer = changeFrequency;
    }

    // Change to the next screen
    void NextScreen()
    {
        currentScreen++;

        if (currentScreen == screens.Length)
        {
            currentScreen = 0;
        }

        GetComponent<Renderer>().material = screens[currentScreen];
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            NextScreen();
            timer = changeFrequency;
        }
    }
}
