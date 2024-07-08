using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Computer : MonoBehaviour
{
    public Text input;
    public bool interactable;

    [Header("Game Objects")]
    public GameObject computerScreen;
    public GameObject mazeScreen, policeFile;
    public GameObject computerFile, computerFile2;
    public GameObject computerPassword;
    public GameObject hint;

    [Header("Maze & Player Bools")]
    public static bool mazeCompleted;
    public static bool playerUsingComputer;
    public GameObject fileNames;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // checks if the maze has been completed
        if (mazeCompleted)
        {
            mazeScreen.SetActive(false);
            playerUsingComputer = false;
        }
            
    }

    private void OnMouseDown()
    {
        // sets the computer to in use
        Interact();
        playerUsingComputer = true;
    }

    public void Interact()
    { 
        // brings up the computer screen
        if (!interactable)
            computerScreen.SetActive(true);
    }

    public void StopInteract()
    {
        // closes everything
        computerScreen.SetActive(false);
        mazeScreen.SetActive(false);
        playerUsingComputer = false;
    }

    public void ReadStringInput(string stringInput)
    {
        // checks the inputed text
        stringInput = input.text;

        // if the code isn't right it gives a hint
        if(stringInput != "i6H4") 
            hint.SetActive(true);

        // opens the other screens if the password is right
        else if(stringInput == "i6H4")
        {
            computerFile.SetActive(true);
            computerPassword.SetActive(false);
 
            // checks if the player has found the information in the diary
            if (Diary.unlocked)
                computerFile2.SetActive(true);
        }
    }

    public void OpenMaze()
    {
        // opens the maze if it hasn't been completed
        if (!mazeCompleted)
        {
            mazeScreen.SetActive(true);
            computerScreen.SetActive(false);
        }
    }

    public void OpenPoliceReport()
    {
        // opens the police report
        computerFile.SetActive(false);
        computerFile2.SetActive(false);

        policeFile.SetActive(true);

        // adds the suspects names to the file
        fileNames = FindInActiveObjectByTag("Suspect");
        fileNames.SetActive(true);
    }

    // used to find inactive objects
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
