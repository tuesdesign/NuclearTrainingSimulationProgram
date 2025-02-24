using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POIItem : MonoBehaviour
{


    public int ID;
    private LevelEditorManager editor;

    // Start is called before the first frame update
    void Start()
    {
        editor = GameObject.FindGameObjectWithTag("LevelEditorManager").GetComponent<LevelEditorManager>();
    }


    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Destroy(this.gameObject);
            editor.ItemButtons[ID].quantity--;
            editor.ItemButtons[ID].quantityText.text = editor.ItemButtons[ID].quantity.ToString();
        }
    }
}