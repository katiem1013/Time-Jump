using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlBar : MonoBehaviour
{

    public ControlPanel controlPanel;

    [Header("Rotations")]
    float[] rotations = { 0, 180 };
    public float correctRotation;
    public bool isPlaced;

    private void Start()
    {
        // checks if it is in the correct place on start
        if (Mathf.Round(transform.eulerAngles.z) == correctRotation && !isPlaced)
        {
            controlPanel.CorrectBar();
            isPlaced = true;
        }

        else if (isPlaced)
        {
            controlPanel.WrongBar();
            isPlaced = false;
        }
    }

    public void BarRotate()
    {
        // changes the switch from left to right by rotating them
        transform.Rotate(new Vector3(0, 0, 180));

        // checks if it is the right position
        if (Mathf.Round(transform.eulerAngles.z) == correctRotation && !isPlaced)
        {
            controlPanel.CorrectBar();
            isPlaced = true;
        }

        // if it is and gets rotated again it changes it to false
        else if (isPlaced)
        {
            controlPanel.WrongBar();
            isPlaced = false;
        }
    }
}
