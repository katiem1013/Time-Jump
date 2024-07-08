using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BirdCage : MonoBehaviour
{
    static public float correctCodes = 0;
    static public bool interactable = true;
    public GameObject puzzleCanvas;
    static public bool cageUnlocked;
    static public bool cageOpen, keyGotten;

    public bool cageUnlock2, cageOpen2;

    [Header("Sprites")]
    public SpriteRenderer spriteRender;
    public Sprite unlockedCage, cageOpened, cageOpenNoBird, cageOpenNoBirdNoKey;

    // inventory variables
    public enum Property { usuable, displayable };
    public Property itemProperty;
    public string DisplayImage;
    public Sprite inventorySprite;
    private GameObject InventorySlots;

    public void AddCorrect()
    {
        correctCodes++;
    }

    private void Start()
    {
        InventorySlots = GameObject.Find("Slots");
    }

    public void Update()
    {
        cageUnlock2 = cageUnlocked;
        cageOpen2 = cageOpen;
        // checks what scene the player is currently in
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        // makes the puzzle go away
        if (cageUnlocked)
        {
            spriteRender.sprite = unlockedCage;
            StopInteract();
        }

        // checks if everything is in the right place
        if (correctCodes == 14)
        {
            correctCodes += 1;
        }

        if (correctCodes == 15)
        {
            cageUnlocked = true;
        }

        // changes the sprites depending on what state the cage is in
        if (cageOpen && sceneName == "MPBedroom")
            spriteRender.sprite = cageOpened;

        if (cageOpen && sceneName == "FutAIBedroom") // account for using the wrong scene
            spriteRender.sprite = cageOpenNoBird;

        if (keyGotten && sceneName == "FutAIBedroom") // account for using the wrong scene
            spriteRender.sprite = cageOpenNoBirdNoKey;
    }

    private void OnMouseDown()
    {
        // checks what scene the player is currently in
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        
        
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            // makes the puzzle pop up if it hasn't been completed
            if (!cageUnlocked)
                Interact();

            // opens the cage when the puzzle is completed
            if (cageUnlocked)
                cageOpen = true;

            // allows the key to be grabbed if the cage is unlocked and in the future
            if (cageOpen && sceneName == "FutAIBedroom" && !keyGotten)
            {
                keyGotten = true;
                ItemPickUp();
            }
                
        }

        
    }

    // brings up the canvas
    public void Interact()
    {
        if (interactable)
            puzzleCanvas.SetActive(true);
    }

    // closes the canvas
    public void StopInteract()
    {
        puzzleCanvas.SetActive(false);
    }

    void ItemPickUp()
    {
        // finds the first empty space in the inventory and add its to the players inventory
        foreach (Transform slot in InventorySlots.transform)
        {

            if (slot.transform.GetChild(0).GetComponent<Image>().sprite.name == "EmptySlot")
            {
                // allows the item to be set to either displayable or usuable and adds the item sprite to the inventory 
                slot.transform.GetChild(0).GetComponent<Image>().sprite = inventorySprite;
                slot.GetComponent<Slots>().AssignProperty((int)itemProperty, DisplayImage);
                break;
            }
        }

        spriteRender.sprite = cageOpenNoBirdNoKey; // changes the sprite
    }
}
