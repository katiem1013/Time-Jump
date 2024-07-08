using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.LowLevel;

public class LockedBox : MonoBehaviour
{
    [Header("UI Canvas")]
    static public bool interactable = true;
    public GameObject lockCanvas;
    public Text[] Text;
    public Sprite unlockedSprite;

    [Header("Lock Box")]
    public string password;
    public string[] lockCharacterChoices;
    public int[] lockCharacterNumber;
    public string insertedPassword;

    [Header("Inventory")]
    public string DisplayImage;
    public Sprite inventorySprite;
    private GameObject InventorySlots;
    private GameObject inventory;
    public enum Property { usuable, displayable };
    public Property itemProperty;


    public GameObject AI;


    // Start is called before the first frame update
    void Start()
    {
        InventorySlots = GameObject.Find("Slots");
        lockCharacterNumber = new int[password.Length];
        UpdateUI();
    }

    public void Update()
    {
        // checks if the puzzle has been completed
        if (!interactable)
            gameObject.GetComponent<SpriteRenderer>().sprite = unlockedSprite;
    }

    // changes what passoword has been entered
    public void ChangeInsertedPassword(int number)
    {
        lockCharacterNumber[number]++;
        if (lockCharacterNumber[number] >= lockCharacterChoices[number].Length)
            lockCharacterNumber[number] = 0;

        CheckPassword();
        UpdateUI();
    }

    // checks the entered password
    public void CheckPassword()
    {
        // checks what numbers the locks are currently on
        int passwordLength = password.Length;
        insertedPassword = "";
        for (int i = 0; i < passwordLength; i++)
            insertedPassword += lockCharacterChoices[i][lockCharacterNumber[i]].ToString();

        // unlocks the box
        if (password == insertedPassword)
        {
            Unlock();
            AI.SetActive(true);
        }
    }

    public void Unlock()
    {
        // gives the player an item
        interactable = false;
        StopInteract();
        gameObject.GetComponent<SpriteRenderer>().sprite = unlockedSprite;
        ItemPickUp();
    }

    public void UpdateUI()
    {
        // changes the numbers on screen
        int len = Text.Length;
        for(int i = 0;i < len;i++)
            Text[i].text = lockCharacterChoices[i][lockCharacterNumber[i]].ToString();
    }

    private void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            Interact();
        }
    }

    // enables canvas
    public void Interact()
    {
        if(interactable)
            lockCanvas.SetActive(true);
    }

    // disable canvas
    public void StopInteract()
    {
        lockCanvas.SetActive(false);    
    }

    // adds the item to the inventory
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
