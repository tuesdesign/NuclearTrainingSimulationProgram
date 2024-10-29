using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EventsConsole : MonoBehaviour
{
    public int Rows = 10;
    public TMP_Text Console;

    public void PrintToConsole(string message)
    {
        Console.text += message + "\n";
        // If the number of lines in the console exceeds the number of rows, remove the first line
        if (Console.text.Split('\n').Length > Rows) Console.text = Console.text.Substring(Console.text.IndexOf('\n') + 1);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
