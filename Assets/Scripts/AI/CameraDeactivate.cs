using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraDeactivate : MonoBehaviour
{
    [Header("UI Canvas")]
    static public bool interactable = true;
    public GameObject cameraCanvas;

    public bool thisCamera;

    private void Update()
    {
        cameraCanvas = FindInActiveObjectByTag("CamCanvas");
    }

    private void OnMouseDown()
    {
        // opens the puzzle if theres nothing else on screen
        if (!EventSystem.current.IsPointerOverGameObject())
            Interact();
    }

    public void Interact()
    {
        // shows the puzzle to deactivate the camera
        if (interactable)
        {
            cameraCanvas.SetActive(true);
            thisCamera = true;
        }
    }

    public void TurnOffCamera()
    {
        // deactivated the camera
        if (thisCamera)
        {
            gameObject.SetActive(false);
            thisCamera = false;
        }
    }

    public void StopInteract()
    {
        cameraCanvas.SetActive(false);
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
