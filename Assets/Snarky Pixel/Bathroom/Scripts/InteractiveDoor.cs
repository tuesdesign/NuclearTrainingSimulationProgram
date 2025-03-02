using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveDoor : InteractiveObject
{
    public string animPrefix = "Door|";
    public string openAnim;
    public string closeAnim;
    public bool locked = false;

    private Animator anim;

    private bool isOpen = false;

    // Use this for initialization
    void Start()
    {
        Transform objectParent = transform.parent;

        while (objectParent != null)
        {
            if (objectParent.GetComponent<Animator>() != null)
            {
                anim = objectParent.GetComponent<Animator>();
                break;
            }

            objectParent = objectParent.parent;
        }

        if (anim == null)
        {
            Debug.Log("InteractiveDoor.cs: failed to find animator in parent for object '" + transform.name + "'!");
        }
    }

    // This function is called whenever the door is interacted with
    public override void Action()
    {
        if (!locked)
        {
            if (!isOpen)
            {
                anim.CrossFade(animPrefix + openAnim, 0.1f, -1, 0.0f);
            }
            else
            {
                anim.CrossFade(animPrefix + closeAnim, 0.1f, -1, 0.0f);
            }

            isOpen = !isOpen;
        }
    }
}
