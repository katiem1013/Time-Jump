using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Map : MonoBehaviour
{
    static public bool interactable = true;
    public GameObject mapCanvas;
    public GameObject fileNames;


    private void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            Interact();
        }
    }

    // activates canvas
    public void Interact()
    {
        mapCanvas.SetActive(true);
        fileNames = FindInActiveObjectByTag("Location"); 
        fileNames.SetActive(true); // adds locations to file
    }

    // deactivates canvas
    public void StopInteract()
    {
        mapCanvas.SetActive(false);
    }

    // finds inactive game objects
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
