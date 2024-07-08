using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class SimonSays : MonoBehaviour
{
    [Header("Game Objects")]
    public GameObject[] buttons;
    public GameObject[] lights;
    public GameObject gameCanvas;

    [Header("Lights")]
    public int[] lightOrder;
    public int level = 0;
    public int buttonsClicked = 0;
    public int colourOrder = 0;
    public bool passed = false;
    public bool gameWon = false;

    [Header("Colours")]
    Color32 red = new Color32(225, 39, 0, 255);
    Color32 green = new Color32(4, 204, 0, 255);
    Color32 invisable = new Color32(4, 204, 0, 0);
    Color32 white = new Color32(225, 255, 255, 255);
    public float lightSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // resets info on enable
    public void OnEnable()
    {
        level = 0;
        buttonsClicked = 0;
        colourOrder = -1;
        gameWon = false;

        for(int i = 0; i < lightOrder.Length; i++)
        {
            lightOrder[i] = (Random.Range(0, 8)); // chooses a random order for the lights
        }

        level = 1;

        StartCoroutine(ColourOrder());
    }
    
    // checks which order the player is pressing the buttons in 
    public void ButtonClickOrder(int button)
    {
        buttonsClicked++;
        if(button == lightOrder[buttonsClicked - 1]) 
        {
            passed = true;
        }

        else
        {
            gameWon = false;
            passed = false;
            StartCoroutine(ColourBlink(red)); // blinks red if the order is wrong
        }


        // checks if they have reched 5 rounds yet
        if (buttonsClicked == level && passed == true && buttonsClicked != 5)
        {
            level++;
            passed = false;
            StartCoroutine(ColourOrder()); 
        }

        // checks if the player has won
        if (buttonsClicked == level && passed == true && buttonsClicked == 5)
        {
            gameWon = true;
            StartCoroutine(ColourBlink(green));
        }
    }

    // makes the colour blink depending on if they won or not
    IEnumerator ColourBlink(Color32 colorToBlink)
    {
        DisableButtons(); // disable buttons 
        for (int x = 0; x < 3; x++)
        {
            for (int i = 0; i < buttons.Length; i++)
                buttons[i].GetComponent<Image>().color = colorToBlink;

            yield return new WaitForSeconds(0.5f);

            for (int i = 0; i < buttons.Length; i++)
                buttons[i].GetComponent<Image>().color = white;

            yield return new WaitForSeconds(0.5f);
        }

        if (gameWon)
            Diary.unlocked = true;

        EnableButtons();
        OnEnable();
    }

    // blinks the colour green on if the player is need to press its
    IEnumerator ColourOrder()
    {
        buttonsClicked = 0;
        colourOrder++;
        DisableButtons();

        for (int i = 0; i <= colourOrder; i++)
        {
            if (level >= colourOrder)
            {
                lights[lightOrder[i]].GetComponent<Image>().color = invisable;
                yield return new WaitForSeconds(lightSpeed);
                lights[lightOrder[i]].GetComponent<Image>().color = green;
                yield return new WaitForSeconds(lightSpeed);
                lights[lightOrder[i]].GetComponent<Image>().color = invisable;
                if (gameWon)
                    Diary.unlocked = true;
            }
        }

        EnableButtons();
    }

    // stops the player from being able to press the buttons
    public void DisableButtons()
    {
        for (int i = 0; i < buttons.Length; i++)
            buttons[i].GetComponent<Button>().interactable = false;
    }

    // lets the player press the buttons
    public void EnableButtons()
    {
        for (int i = 0; i < buttons.Length; i++)
            buttons[i].GetComponent<Button>().interactable = true;
    }
}
