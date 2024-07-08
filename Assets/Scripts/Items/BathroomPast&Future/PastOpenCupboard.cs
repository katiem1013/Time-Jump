using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class PastOpenCupboard : MonoBehaviour
{
    [Header("UI Canvas")]
    public GameObject insideCanvas;

    [Header("Items")]
    public GameObject buttons;
    public GameObject letter;

    [Header("Keycode")]
    public List<string> correctKeycode;
    public List<string> inputtedKeycode;
    static public bool correctCode;
    public float inputAttempts;

    private void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
            Interact();
    }

    private void Update()
    {
        // prints the keycode
        foreach (var x in inputtedKeycode)
        {
            Debug.Log(x.ToString());
        }

        // resets the attempts if they hit 6
        if (inputAttempts > 6)
        {
            inputAttempts = 0;
            inputtedKeycode.Clear();
        }

        // checks if the sequeence is correct
        if (inputtedKeycode.SequenceEqual(correctKeycode))
            correctCode = true;

        // reveals the letter if so
        if (correctCode)
        {
            buttons.SetActive(false);
            letter.SetActive(true);
        }
    }

    // enables the canvas
    public void Interact()
    {
        insideCanvas.SetActive(true);
        
    }

    // disables the canvas
    public void StopInteract()
    {
        insideCanvas.SetActive(false);
    }

    // add each arrow to the inpputed code
    public void LeftArrow()
    {
        if (inputAttempts <= 6)
        {
            inputtedKeycode.Add("Left");
            inputAttempts++;
        }
    }

    public void RightArrow()
    {
        if (inputAttempts <= 6)
        {
            inputtedKeycode.Add("Right");
            inputAttempts++;
        }
    }

    public void UpArrow()
    {
        if (inputAttempts <= 6)
        {
            inputtedKeycode.Add("Up");
            inputAttempts++;
        }
    }

    public void DownArrow()
    {
        if (inputAttempts <= 6)
        {
            inputtedKeycode.Add("Down");
            inputAttempts++;
        }
    }
}
