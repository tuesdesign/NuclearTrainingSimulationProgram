using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemController : MonoBehaviour
{

    public int ID;
    public int quantity;
    public TextMeshProUGUI quantityText;
    public bool Clicked = false;
    private LevelEditorManager editor;

    // Start is called before the first frame update
    void Start()
    {
        quantityText.text = quantity.ToString();   
        editor = GameObject.FindGameObjectWithTag("LevelEditorManager").GetComponent<LevelEditorManager>();
    }

    public void buttonClicked()
    {
        Vector3 screenPosition = new Vector3(Input.mousePosition.x, 1, Input.mousePosition.z);
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        Clicked = true;
        Instantiate(editor.ItemImage[ID], new Vector3(worldPosition.x, 1, worldPosition.z), Quaternion.identity);
        quantity++;
        quantityText.text = quantity.ToString();
        editor.CurrentButtonPressed = ID;
    }

    
}
