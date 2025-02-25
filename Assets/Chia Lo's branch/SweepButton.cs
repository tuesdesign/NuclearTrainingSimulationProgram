using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SweepButton : MonoBehaviour
{
    private Task_List task;
    // Start is called before the first frame update
    void Start()
    {
        task = FindObjectOfType<Task_List>();
    }

    public void StadiumSweepStart()
    {
        task.changeMode(1);
    }

    public void ParkingLotSweepStart()
    {
        task.changeMode(2);
    }
    public void Detected()
    {
        task.changeMode(3);
    }
}
