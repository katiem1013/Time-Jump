using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeScript : MonoBehaviour
{
    public CorrectPositions correctPositions;

    [Header("Rotations")]
    float[] rotations = { 0, 90, 180, 270};
    public float[] correctRotation;
    public bool isPlaced;
    int possibleRotations;

    private void Start()
    {
        possibleRotations = correctRotation.Length;
        int rand = Random.Range(0, rotations.Length);
        transform.eulerAngles = new Vector3(0, 0, rotations[rand]);

        // check if they start in the right position
        if (possibleRotations > 1)
        {
            // checks the pipes position
            if (Mathf.Round(transform.eulerAngles.z) == correctRotation[0] || Mathf.Round(transform.eulerAngles.z) == correctRotation[1] && !isPlaced)
            {
                correctPositions.CorrectPipe();
                isPlaced = true;
            }

            else if (isPlaced)
            {
                correctPositions.WrongPipe();
                isPlaced = false;
            }
        }

        else
        {
            // checks the pipes rotation
            if (transform.eulerAngles.z == correctRotation[0] && !isPlaced)
            {
                correctPositions.CorrectPipe();
                isPlaced = true;
            }

            else if (isPlaced)
            {
                correctPositions.WrongPipe();
                isPlaced = false;
            }
        }
    }

    // rotates the pipes when they are clicked on
    public void PipeRotate()
    {
        transform.Rotate(new Vector3(0, 0, 90));

        if (possibleRotations > 1)
        {
            // checks if they are in the right position
            if (Mathf.Round(transform.eulerAngles.z) == correctRotation[0] || Mathf.Round(transform.eulerAngles.z) == correctRotation[1] && !isPlaced)
            {
                correctPositions.CorrectPipe();
                isPlaced = true;
            }
                
            else if (isPlaced)
            {
                correctPositions.WrongPipe();
                isPlaced = false;
            }
        }

        else
        {
            // checks if they are in the right position
            if (transform.eulerAngles.z == correctRotation[0] && !isPlaced)
            {
                correctPositions.CorrectPipe();
                isPlaced = true;
            }

            else if (isPlaced)
            {
                correctPositions.WrongPipe();
                isPlaced = false;
            }
        }

    }
}
