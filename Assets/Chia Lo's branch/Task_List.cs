using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Task_List : MonoBehaviour
{
    private int sweepMode = 0;

    public TextMeshProUGUI mode;

    [SerializeField] private List<ClusterNavigator> security;
    // Start is called before the first frame update
    void Start()
    {
        mode.text = "Sweep Mode";
    }

    // Update is called once per frame
    void Update()
    {
        switch (sweepMode)
        {
            case 0 :
                mode.text = "stadium sweep start";
                foreach (ClusterNavigator change in security)
                {
                    change.newNPCRole(NPCRole.SweepStadium);
                }
                break;
            case 1 :
                mode.text = "parking lot sweep start";
                foreach (ClusterNavigator change in security)
                {
                    change.newNPCRole(NPCRole.SweepParkingLot);
                }
                break;
        }
    }

    public void changeMode(int mode)
    {
        sweepMode = mode;
    }
}
