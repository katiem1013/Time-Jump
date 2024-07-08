using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Mail : MonoBehaviour
{
    [Header("Canvas")]
    static public float correctCodes = 0;
    static public bool interactable = true;
    public GameObject letterCanvas;
    public GameObject fileNames, completeText;

    // adds to the correct codes
    public void AddCorrect()
    {
        correctCodes++;
    }

    public void Update()
    {

        fileNames = FindInActiveObjectByTag("MPNames");

        if (correctCodes == 4)
        {
            fileNames.SetActive(true); // adds the potential names to the file
            completeText.SetActive(true);
        }

    }

    private void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            Interact();
        }
    }

    // shows letter canvas
    public void Interact()
    {
        if (interactable)
            letterCanvas.SetActive(true);
    }

    // disables letters canvas
    public void StopInteract()
    {
        letterCanvas.SetActive(false);
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
