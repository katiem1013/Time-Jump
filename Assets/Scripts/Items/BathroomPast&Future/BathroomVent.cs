using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BathroomVent : MonoBehaviour
{
    static public bool interactable = true;
    public GameObject ventCanvas;

    private void OnMouseDown()
    {
        Interact();
    }

    // sets canvas as active
    public void Interact()
    {
        if (interactable)
            ventCanvas.SetActive(true);
    }

    // sets the canvas as false
    public void StopInteract()
    {
        ventCanvas.SetActive(false);
    }
}
