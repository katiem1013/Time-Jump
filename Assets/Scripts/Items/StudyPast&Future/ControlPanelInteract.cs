using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ControlPanelInteract : MonoBehaviour
{
    [Header("UI Canvas")]
    static public bool interactable = true;
    public GameObject panelCanvas;

    private void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
            Interact();
    }

    public void Interact()
    {
        // brings up the panel if interactable
        if (interactable)
        {
            panelCanvas.SetActive(true);
        }
    }

    // gets rid of the canvas
    public void StopInteract()
    {
        panelCanvas.SetActive(false);
    }
}
