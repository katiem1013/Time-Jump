using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PuzzleBox : MonoBehaviour
{
    static public bool completePuzzle = false;
    static public bool matchboxGained = false;
    public GameObject puzzleBoxCanvas;

    public enum Property { usuable, displayable };
    public Property itemProperty;
    public string DisplayImage;
    public Sprite inventorySprite;
    private GameObject InventorySlots;

    private void Start()
    {
        InventorySlots = GameObject.Find("Slots");
    }

    // Start is called before the first frame update
    void Update()
    {
        // checks if its been completed before
        if (completePuzzle && !matchboxGained)
        {
            StopInteract();
            matchboxGained = true;
            ItemPickUp();
        }

    }

    // Update is called once per frame
    void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            Interact();
        }
    }

    // sets the canvas as enabled
    public void Interact()
    {
        if (!completePuzzle)
            puzzleBoxCanvas.SetActive(true);
    }

    // disables the canvas
    public void StopInteract()
    {
        puzzleBoxCanvas.SetActive(false);
    }

    // gives the player the item
    void ItemPickUp()
    {
        foreach (Transform slot in InventorySlots.transform)
        {

            if (slot.transform.GetChild(0).GetComponent<Image>().sprite.name == "EmptySlot")
            {
                slot.transform.GetChild(0).GetComponent<Image>().sprite = inventorySprite;
                slot.GetComponent<Slots>().AssignProperty((int)itemProperty, DisplayImage);
                break;
            }
        }
    }
}

