using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diary : MonoBehaviour
{
    [Header("Canvas")]
    public GameObject fileNames;
    public GameObject diaryCanvas, simonSaysCanvas;
    static public bool unlocked;
    public bool interactable = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // opens the diary
        if (unlocked && !interactable)
        {
            diaryCanvas.SetActive(true);
            simonSaysCanvas.SetActive(false);
        }
    }

    private void OnMouseDown()
    {
        Interact();
    }

    // brings up simon says mini game or diary
    public void Interact()
    {
        interactable = false;

        // checks if the diary has been unlocked
        if (!unlocked)
        {
            // plays simon say mini game
            simonSaysCanvas.SetActive(true);
            diaryCanvas.SetActive(false);
        }
            
        
        if (unlocked)
        {
            // opens diary
            diaryCanvas.SetActive(true);
            simonSaysCanvas.SetActive(false);
        }
    }

    // closes everything 
    public void StopInteract()
    {
        interactable = true;
        diaryCanvas.SetActive(false);
        simonSaysCanvas.SetActive(false);
    }
}
