using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePieces : MonoBehaviour
{
    [Header("Puzzle Positions")]
    public GameObject puzzlePiece;
    public GameObject objectHolder;
    static public float holderDistance;
    public float dropDistance;

    [Header("Holders")]
    public bool isCode;
    public bool isLocked;
    public GameObject holderObj;
    public PuzzleHolder puzzleHolder;
    public BirdCage birdCage;

    // Start is called before the first frame update
    void Start()
    {
        puzzlePiece = gameObject; // calls the object the script is attached to as the puzzle
    }

    public void DragPiece()
    {
        // if the puzxle isn't lock it will follow the mouse when picked up
        if (!isLocked)
            puzzlePiece.transform.position = Input.mousePosition;

    }

    public void DropPiece()
    {
        // refrences the puzzle holder object
        puzzleHolder = holderObj.GetComponent<PuzzleHolder>();

        // finds the distance between the holders and the puzzle piece
        holderDistance = Vector3.Distance(puzzlePiece.transform.position, objectHolder.transform.position);


        // checks the distance between where the player drops the puzzle piece 
        if (holderDistance < dropDistance)
        {
            if (puzzleHolder.holdFull == false) // checks if something is already placed there 
            {
                puzzleHolder.holdFull = true; // sets the holder as full
                puzzleHolder.holdName = gameObject.ToString(); // sets holde to the name of the puzzle piece currently in the holder
                puzzlePiece.transform.position = objectHolder.transform.position; // puts the puzzle piece in the holder
                // if it is the correct answer it will get locked into place, and add one to correct number of puzzle piece
                if (isCode == true && isLocked == false)
                {
                    isLocked = true;
                    birdCage.AddCorrect();
                }
            }
        }
    }
}
