using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OpenCupboard : MonoBehaviour
{
    static public bool interactable = true;
    public GameObject insideCanvas;
    public float correctCodes = 0;

    public GameObject text;

    // Update is called once per frame
    void Update()
    {
        // checks if the code is correct and sets the text to active
        if (correctCodes == 6)
        {
            correctCodes += 1;
            text.SetActive(true);
        }
    }

    // adds one to the amount of correct codes
    public void AddCorrect()
    {
        correctCodes++;
    }

    private void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
            Interact();
    }

    // brings up the inside of the cupboard canvas
    public void Interact()
    {
        if (interactable)
            insideCanvas.SetActive(true);
    }

    // closes the canvas
    public void StopInteract()
    {
        insideCanvas.SetActive(false);
    }

}
