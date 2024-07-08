using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BathroomKeyCode : MonoBehaviour
{
    [Header("Code Input")]
    string passcode = "3942";
    string number = null;
    public int numberIndex = 0;
    public Text enteredCode = null;

    public Bathroom bathroom;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // checks if a number has been inputed
    public void CodeFunction(string numbers)
    {
        if (numberIndex < 4)
        {
            numberIndex++;
            number += numbers;
            enteredCode.text = number;
        }
    }

    // checks if the corrrect passowrd is entered
    public void Enter()
    {
        if (number == passcode)
        {
            Bathroom.correctKeyCode = true;
            print("Correct");
        }
    }

    // deletes typed in code
    public void Delete()
    {
        numberIndex = 0;
        number = null;
        enteredCode.text = number;
    }
}
