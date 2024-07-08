using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Letters : MonoBehaviour
{
    [Header("Puzzle Positions")]
    public GameObject letter;
    public GameObject objectHolder;
    static public float holderDistance;
    public float dropDistance;

    [Header("Holders")]
    public bool isCode;
    public bool isLocked;
    public GameObject holderObj;
    public MailHolder letterHolder;
    public Mail mail;

    // Start is called before the first frame update
    void Start()
    {
        letter = gameObject; // calls the object the script is attached to as the bottle
    }

    public void DragLetter()
    {
        // if the bottle isn't lock it will follow the mouse when picked up
        if (!isLocked)
            letter.transform.position = Input.mousePosition;

    }

    public void DropLetter()
    {
        // refrences the fridge object
        letterHolder = holderObj.GetComponent<MailHolder>();

        // finds the distance between the holders and the bottle
        holderDistance = Vector3.Distance(letter.transform.position, objectHolder.transform.position);


        // checks the distance between where the player drops the magnet and holder number 1
        if (holderDistance < dropDistance)
        {
            if (letterHolder.holdFull == false) // checks if something is already placed there 
            {
                letterHolder.holdFull = true; // sets the holder as full
                letterHolder.holdName = gameObject.ToString(); // sets holde to the name of the magnet currently in the holder
                letter.transform.position = objectHolder.transform.position; // puts the magnet in the holder
                // if it is the correct answer it will get locked into place, and add one to correct number of magnets
                if (isCode == true && isLocked == false)
                {
                    isLocked = true;
                    mail.AddCorrect();
                }
            }

        }

    }
}
