using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Post_It : MonoBehaviour
{
    static public bool interactable = true;
    public GameObject postItCanvas;

   
    private void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            Interact();
            print("open");
        }
    }

    // sets canvas as active
    public void Interact()
    {
         postItCanvas.SetActive(true);
    }

    // sets the canvas as inactive
    public void StopInteract()
    {
        postItCanvas.SetActive(false);
    }

}
