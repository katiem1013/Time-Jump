using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPanel : MonoBehaviour
{
    [Header("Game Objects")]
    public GameObject controlPanel;
    public ControlPanelInteract controlPanelInteract;
    public GameObject numberText;
    public GameObject[] controlBars;

    [Header("Numbers")]
    public int totalBars = 0;
    public int correctBars;
    static bool barsCompleted;

    private void Start()
    {
        totalBars = controlPanel.transform.childCount - 1; // the minus 1 is to account for the text

        controlBars = new GameObject[totalBars];

        // checks the amount of controll bars
        for (int i = 0; i < controlBars.Length; i++)
        {
            controlBars[i] = controlPanel.transform.GetChild(i).gameObject;
        }
    }

    public void Update()
    {
        // checks if the bars are in the right positon
        if (barsCompleted)
            numberText.SetActive(true);
    }

    // adds to the correct amount of bars
    public void CorrectBar()
    {
        correctBars++;

        if (correctBars == totalBars)
            barsCompleted = true;
    }

    // removes one from the wrong bars
    public void WrongBar()
    {
        correctBars--;
    }
}
