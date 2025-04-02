using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SweepMenuManager : MonoBehaviour
{
    [Header("Buttons")]
    public Button StationaryButton;
    public Button MobileButton;
    public Button VanButton;

    [Header("Menus")]
    public GameObject StationaryMenu;
    public GameObject MobileMenu;
    public GameObject VanMenu;

    void Start()
    {
        // Set initial state
        SwitchToMenu(1);

        // Add button listeners
        StationaryButton.onClick.AddListener(() => SwitchToMenu(1));
        MobileButton.onClick.AddListener(() => SwitchToMenu(2));
        VanButton.onClick.AddListener(() => SwitchToMenu(3));
    }

    public void SwitchToMenu(int menuIndex)
    {
        // Disable all menus
        StationaryMenu.SetActive(false);
        MobileMenu.SetActive(false);
        VanMenu.SetActive(false);

        // Enable the selected menu
        switch (menuIndex)
        {
            case 1:
                StationaryMenu.SetActive(true);
                StationaryButton.interactable = false;
                MobileButton.interactable = true;
                VanButton.interactable = true;
                break;
            case 2:
                MobileMenu.SetActive(true);
                StationaryButton.interactable = true;
                MobileButton.interactable = false;
                VanButton.interactable = true;
                break;
            case 3:
                VanMenu.SetActive(true);
                StationaryButton.interactable = true;
                MobileButton.interactable = true;
                VanButton.interactable = false;
                break;
        }
    }
}

