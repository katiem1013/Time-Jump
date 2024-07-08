using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Phone : MonoBehaviour
{
    [Header("Canvas")]
    public GameObject phoneCanvas;
    public GameObject fileNames;
    static public bool phoneIsOn = false;
    public InputField phoneInputField;

    [Header("Messages")]
    static public bool passwordEntered;
    public bool lookAtMessages;
    public GameObject enterPassword, multipleMessages, hint;
    public GameObject shannon, unknownNumber, lucy, savanah, mircosoft;


    private void Update()
    {
        // enables info in file
        if (passwordEntered)
        {
            fileNames = FindInActiveObjectByTag("Seen");
            fileNames.SetActive(true);
        }

        // checks if the right password has been entered
        if (phoneInputField.text == "Peacefront" || phoneInputField.text == "peacefront")
            passwordEntered = true;

        // gives a hint if not
        if (phoneInputField.text != "Peacefront" && phoneInputField.text != "peacefront")
            hint.SetActive(true);

        // shows the multiple messages 
        if (passwordEntered && !lookAtMessages)
        {
            lookAtMessages = true;
            enterPassword.SetActive(false);
            multipleMessages.SetActive(true);
        }

        // shows the password screen
        else if (!passwordEntered && !lookAtMessages)
        {
            enterPassword.SetActive(true);
            multipleMessages.SetActive(false);
        }

    }

    private void OnMouseDown()
    {
        Interact();
    }

    // enables the canvas
    public void Interact()
    {
        phoneIsOn = true;
        phoneCanvas.SetActive(true);
        
        // checks if the password is entered
        if (passwordEntered)
            multipleMessages.SetActive(true);

        if (!passwordEntered)
            enterPassword.SetActive(true);

    }

    // disables the canvas
    public void StopInteract()
    {
        phoneIsOn = false;
        phoneCanvas.SetActive(false);
    }

    // sets shannons messages as active
    public void Shannon()
    {
        multipleMessages.SetActive(false);
        shannon.SetActive(true);
        unknownNumber.SetActive(false);
        lucy.SetActive(false);
        savanah.SetActive(false);
        mircosoft.SetActive(false);
    }

    // sets unknowns messages as active
    public void Unknown()
    {
        multipleMessages.SetActive(false);
        shannon.SetActive(false);
        unknownNumber.SetActive(true);
        lucy.SetActive(false);
        savanah.SetActive(false);
        mircosoft.SetActive(false);
    }

    public void UnknownNumber()
    {
        multipleMessages.SetActive(false);
        shannon.SetActive(false);
        unknownNumber.SetActive(true);
        lucy.SetActive(false);
        savanah.SetActive(false);
        mircosoft.SetActive(false);
    }

    // sets lucys messages as active
    public void Lucy()
    {
        multipleMessages.SetActive(false);
        shannon.SetActive(false);
        unknownNumber.SetActive(false);
        lucy.SetActive(true);
        savanah.SetActive(false);
        mircosoft.SetActive(false);
    }

    // sets savanah messages as active
    public void Savanah()
    {
        multipleMessages.SetActive(false);
        shannon.SetActive(false);
        unknownNumber.SetActive(false);
        lucy.SetActive(false);
        savanah.SetActive(true);
        mircosoft.SetActive(false);
    }

    // sets mircosoft messages as active
    public void Microsoft()
    {
        multipleMessages.SetActive(false);
        shannon.SetActive(false);
        unknownNumber.SetActive(false);
        lucy.SetActive(false);
        savanah.SetActive(false);
        mircosoft.SetActive(true);
    }

    // returns to message screen
    public void GoBack()
    {
        multipleMessages.SetActive(true);
        shannon.SetActive(false);
        unknownNumber.SetActive(false);
        lucy.SetActive(false);
        savanah.SetActive(false);
        mircosoft.SetActive(false);
    }

    // finds inactive objects 
    GameObject FindInActiveObjectByTag(string tag)
    {

        Transform[] objs = Resources.FindObjectsOfTypeAll<Transform>() as Transform[];
        for (int i = 0; i < objs.Length; i++)
        {
            if (objs[i].hideFlags == HideFlags.None)
            {
                if (objs[i].CompareTag(tag))
                {
                    return objs[i].gameObject;
                }
            }
        }
        return null;
    }
}
