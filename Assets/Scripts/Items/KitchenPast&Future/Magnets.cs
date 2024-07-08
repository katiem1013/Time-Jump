using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnets : MonoBehaviour
{
    public GameObject magnet;
    public GameObject objectHolder1, objectHolder2, objectHolder3, objectHolder4;
    static public float holder1Distance, holder2Distance, holder3Distance, holder4Distance;
    public float dropDistance;
    public bool isCode1, isCode2, isCode3, isCode4;
    public bool isLocked;
    Vector2 magnetInitalPos;
    Vector2 magnetPos;

    public GameObject fridgeObj;
    public Fridge fridge;

    // Start is called before the first frame update
    void Start()
    {
        magnet = gameObject; // calls the object the script is attached to as the magnet
        magnetInitalPos = gameObject.transform.position; // sets the inital position of the magnet
    }

    private void Update()
    {
    }

    public void DragMagnets()
    {
        // refrences the fridge object
        fridge = fridgeObj.GetComponent<Fridge>();
        // if the magnet isn't lock it will follow the mouse when picked up
        if (!isLocked)
            magnet.transform.position = Input.mousePosition;

        // checks if they are picking up the object currently in the holder
        if (fridge.hold1Name == gameObject.ToString())
            fridge.hold1Full = false;

        if (fridge.hold2Name == gameObject.ToString())
            fridge.hold2Full = false;

        if (fridge.hold3Name == gameObject.ToString())
            fridge.hold3Full = false;

        if (fridge.hold4Name == gameObject.ToString())
            fridge.hold4Full = false;

    }

    public void DropMagnet()
    {
        // refrences the fridge object
        fridge = fridgeObj.GetComponent<Fridge>();

        // finds the distance between all four holders and the magnet
        holder1Distance = Vector3.Distance(magnet.transform.position, objectHolder1.transform.position);
        holder2Distance = Vector3.Distance(magnet.transform.position, objectHolder2.transform.position);
        holder3Distance = Vector3.Distance(magnet.transform.position, objectHolder3.transform.position);
        holder4Distance = Vector3.Distance(magnet.transform.position, objectHolder4.transform.position);

        // checks the distance between where the player drops the magnet and holder number 1
        if (holder1Distance < dropDistance)
        {
            if (fridge.hold1Full == false) // checks if something is already placed there 
            {
                fridge.hold1Full = true; // sets the holder as full
                fridge.hold1Name = gameObject.ToString(); // sets holde to the name of the magnet currently in the holder
                magnet.transform.position = objectHolder1.transform.position; // puts the magnet in the holder
                // if it is the correct answer it will get locked into place, and add one to correct number of magnets
                if (isCode1 == true && isLocked == false)
                {
                    isLocked = true;
                    fridge.AddCorrect();
                }
            }

            // here as a fail safe to make sure it goes back to inital position if it can't be placed
            else
                magnet.transform.position = magnetInitalPos;
        }

        // repeat of holder 1 with holder 2
        else if (holder2Distance < dropDistance)
        {
            if (fridge.hold2Full == false)
            {
                // checks if something is in the holder and if it is correct
                fridge.hold2Full = true;
                fridge.hold2Name = gameObject.ToString();
                magnet.transform.position = objectHolder2.transform.position;
                if (isCode2 == true && isLocked == false)
                {
                    isLocked = true;
                    fridge.AddCorrect();
                }
            }

            else
                magnet.transform.position = magnetInitalPos;
        }

        // repeat of holder 1 with holder 3
        else if (holder3Distance < dropDistance)
        {
            if (fridge.hold3Full == false)
            {
                // checks if something is in the holder and if it is correct
                fridge.hold3Full = true;
                fridge.hold3Name = gameObject.ToString();
                magnet.transform.position = objectHolder3.transform.position;
                if (isCode3 == true && isLocked == false)
                {
                    isLocked = true;
                    fridge.AddCorrect();
                }
            }

            else
                magnet.transform.position = magnetInitalPos;
        }

        // repeat of holder 1 with holder 4
        else if (holder4Distance < dropDistance)
        {
            if (fridge.hold4Full == false)
            {
                // checks if something is in the holder and if it is correct
                fridge.hold4Full = true;
                fridge.hold4Name = gameObject.ToString();
                magnet.transform.position = objectHolder4.transform.position;
                if (isCode4 == true && isLocked == false)
                {
                    isLocked = true;
                    fridge.AddCorrect();
                }
            }

            else
                magnet.transform.position = magnetInitalPos;
        }

        // if the holder is full or they drop it else where the magnet will return to it's original position
        else
            magnet.transform.position = magnetInitalPos;

    }
}
