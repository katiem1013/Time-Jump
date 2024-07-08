using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class Doors : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite newSprite;

    public bool doorOpened = false;
    public bool doorLocked = true;

    public bool inRange = false;

    void OnMouseDown()
    {
        // changes the sprites based on door status
        if (!doorLocked)
        {
            spriteRenderer.sprite = newSprite; 
            doorOpened = true;
        }

        // loads the scene based on the door used
        // this script didn't work because of the static variables
        if(doorOpened == true && this.gameObject.name == "Kitchen Door")
        {
            SceneManager.LoadScene("Kitchen");
        }

        if (doorOpened == true && this.gameObject.name == "Study Door")
        {
            SceneManager.LoadScene("Study");
        }

        if (doorOpened == true && this.gameObject.name == "Living Room Door")
        {
            SceneManager.LoadScene("Livingroom");
        }

        if (doorOpened == true && this.gameObject.name == "Bathroom Door")
        {
            SceneManager.LoadScene("Bathroom");
        }

        if (doorOpened == true && this.gameObject.name == "AI Bedroom")
        {
            SceneManager.LoadScene("AIBedroom");
        }

        if (doorOpened == true && this.gameObject.name == "Missing Person Bedroom")
        {
            SceneManager.LoadScene("MPBedroom");
        }
    }
}
