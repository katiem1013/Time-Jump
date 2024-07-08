using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FileInputs : MonoBehaviour
{
    [Header("Inputs")]
    public GameObject fileObj;
    static public float holderDistance;
    public float dropDistance;
    public bool alreadyAdded;

    [Header("Holders")]
    public bool isCode;
    public GameObject holderObj;
    public FileHolder fileHolder;
    public FileMenu fileMenu;
    public Vector2 startPos;

    // Start is called before the first frame update
    void Start()
    {
        fileObj = gameObject; // calls the object the script is attached to as the file
        startPos = gameObject.transform.position;
    }

    public void DragPiece()
    {
        // file will follow the mouse when picked up
        fileObj.transform.position = Input.mousePosition;
    }

    public void DropPiece()
    {
        // refrences the file
        fileHolder = holderObj.GetComponent<FileHolder>();

        // finds the distance between the holders and the file
        holderDistance = Vector3.Distance(fileObj.transform.position, holderObj.transform.position);


        // checks the distance between where the player drops the file
        if (holderDistance < dropDistance)
        {
            if (fileHolder.holdFull == false) // checks if something is already placed there 
            {
                fileHolder.holdFull = true; // sets the holder as full
                fileHolder.holdName = gameObject.ToString(); // sets holde to the name of the magnet currently in the holder
                fileObj.transform.position = holderObj.transform.position; // puts the file in the holder
                // if it is the correct answer it will add one to correct number of file
                if (isCode == true && !alreadyAdded)
                {
                    fileMenu.AddCorrect();
                    alreadyAdded = true; // makes sure the player can't keep adding them
                }

            }
            else
                fileObj.transform.position = startPos; // puts it back to the start
        }

        if (isCode && fileHolder.holdName == gameObject.ToString() && holderDistance > dropDistance) // makes sure that the code is removed if the item is taken out
        {
            fileMenu.RemoveCorrect();
            alreadyAdded = false;
        }

        if (fileHolder.holdName == gameObject.ToString() && holderDistance > dropDistance)
            fileHolder.holdFull = false;
    }

}
