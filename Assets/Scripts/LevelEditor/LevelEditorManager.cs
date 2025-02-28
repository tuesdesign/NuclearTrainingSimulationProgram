using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;

public class LevelEditorManager : MonoBehaviour
{
    public ItemController[] ItemButtons;
    public GameObject[] ItemPrefabs;
    public GameObject[] ItemImage;
    public int CurrentButtonPressed;
    [SerializeField]
    NavMeshSurface surface;

    private void Update()
    {
        Vector3 screenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

        if(Input.GetMouseButtonDown(0) && ItemButtons[CurrentButtonPressed].Clicked)
        {
            ItemButtons[CurrentButtonPressed].Clicked = false;

            if (CurrentButtonPressed == 1) //This means we selected the POI so it needs to be height 0
            {
                Instantiate(ItemPrefabs[CurrentButtonPressed], new Vector3(worldPosition.x, 0, worldPosition.z), Quaternion.identity);

            }
            else
            {
                Instantiate(ItemPrefabs[CurrentButtonPressed], new Vector3(worldPosition.x, 1, worldPosition.z), Quaternion.identity);
            }
            Destroy(GameObject.FindGameObjectWithTag("ItemImage"));
        }
    }

    public void bakeNavMesh()
    {
        surface.BuildNavMesh();
    }
}
