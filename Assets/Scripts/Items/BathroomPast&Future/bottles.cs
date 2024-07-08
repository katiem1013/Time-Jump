using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bottles : MonoBehaviour
{
    [Header("Bottle Positions")]
    public GameObject bottle;
    public GameObject objectHolder;
    static public float holderDistance;
    public float dropDistance;
    
    [Header("Holders")]
    public bool isCode;
    public bool isLocked;
    public GameObject holderObj;
    public BathroomCupboard bathroomCupboard;
    public OpenCupboard openCupboard;

    // Start is called before the first frame update
    void Start()
    {
        bottle = gameObject; // calls the object the script is attached to as the bottle
    }

    public void DragBottle()
    {
        // if the bottle isn't lock it will follow the mouse when picked up
        if (!isLocked)
            bottle.transform.position = Input.mousePosition;

    }

    public void DropBottle()
    {
        // refrences the fridge object
        bathroomCupboard = holderObj.GetComponent<BathroomCupboard>();

        // finds the distance between the holders and the bottle
        holderDistance = Vector3.Distance(bottle.transform.position, objectHolder.transform.position);
        

        // checks the distance between where the player drops the magnet and holder number 1
        if (holderDistance < dropDistance)
        {
            if (bathroomCupboard.holdFull == false) // checks if something is already placed there 
            {
                bathroomCupboard.holdFull = true; // sets the holder as full
                bathroomCupboard.holdName = gameObject.ToString(); // sets holde to the name of the magnet currently in the holder
                bottle.transform.position = objectHolder.transform.position; // puts the magnet in the holder
                // if it is the correct answer it will get locked into place, and add one to correct number of magnets
                if (isCode == true && isLocked == false)
                {
                    isLocked = true;
                    openCupboard.AddCorrect();
                }
            }

        }

    }
}
