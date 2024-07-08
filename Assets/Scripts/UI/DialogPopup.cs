using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogPopup : MonoBehaviour
{
    public GameObject popUpBox;
    public Text popUpText;

    // activates the pop up and changes the text
    public void PopUp(string text)
    {
        popUpBox.SetActive(true);
        popUpText.text = text;
    }

    // deactivates the pop ups
    public void PopDown()
    {
        popUpBox.SetActive(false);
    }
}
