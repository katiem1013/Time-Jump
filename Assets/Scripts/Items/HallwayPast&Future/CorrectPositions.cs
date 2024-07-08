using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorrectPositions : MonoBehaviour
{
    public CameraDeactivate cameraDeactivate;

    [Header("Game Objects")]
    public GameObject pipeHolder;
    public GameObject[] pipes;

    [Header("Numbers")]
    public int totalPipes = 0;
    public int correctPipes;

    private void Start()
    {
        // counts the amount of pipes
        totalPipes = pipeHolder.transform.childCount;

        pipes = new GameObject[totalPipes];

        // gets the children of all the pipes
        for(int i = 0; i < pipes.Length; i++)
        {
            pipes[i] = pipeHolder.transform.GetChild(i).gameObject;
        }
    }

    public void CorrectPipe()
    {
        // checks if all the pipes are in the right position
        correctPipes++;

        if (correctPipes == totalPipes)
        {
            cameraDeactivate.StopInteract();
            cameraDeactivate.TurnOffCamera();
        }
    }

    // removes one from the correct pipes if its changed
    public void WrongPipe()
    {
        correctPipes--;
    }
}
