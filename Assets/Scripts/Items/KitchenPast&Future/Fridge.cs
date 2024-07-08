using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Fridge : MonoBehaviour
{
    static public bool interactable = true;
    public GameObject fridgeCanvas;
    public GameObject openFridgeCanvas;
    static public float correctCodes = 0;

    public bool hold1Full, hold2Full, hold3Full, hold4Full;
    public string hold1Name, hold2Name, hold3Name, hold4Name;

    // Update is called once per frame
    void Update()
    {
        // checks if everything is in the right position
        if (correctCodes == 4)
        {
            interactable = false;
            StopInteract();
            correctCodes += 1;
        }
    }

    // when mouse is clicked
    private void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
            Interact();
    }

    public void Interact()
    {
        // opens the frige based on if the fridge is unlocked of not
        if (interactable)
            fridgeCanvas.SetActive(true);
        else
            openFridgeCanvas.SetActive(true);
    }

    // closes everything 
    public void StopInteract()
    {
        fridgeCanvas.SetActive(false);
        openFridgeCanvas.SetActive(false);
    }

    // adds to the correct code
    public void AddCorrect()
    {
        correctCodes++;
    }

}   
