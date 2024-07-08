using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenDraw : MonoBehaviour
{

    [Header("Sprites")]
    public SpriteRenderer spriteRenderer;
    public Sprite drawOpen;
    public Sprite emptySlot;

    [Header("Draw")]
    public bool giveKey;
    public bool limitKey;

    [Header("Inventory")]
    public string DisplayImage;
    public Sprite inventorySprite;
    private GameObject InventorySlots;
    private GameObject inventory;
    public enum Property { usuable, displayable };
    public Property itemProperty;

    // Start is called before the first frame update
    void Start()
    {
        // adds the inventory slots referenece to this script
        InventorySlots = GameObject.Find("Slots");
    }

    // Update is called once per frame
    void Update()
    {
        if (Computer.mazeCompleted)
        {
            // checks if the maze is completed and gives key
            spriteRenderer.sprite = drawOpen;
            giveKey = true;
        }
    }

    void OnMouseDown()
    {
        // stops the player from getting unlimited keys
        if (giveKey && !limitKey)
        {
            ItemPickUp();
            giveKey = false;
            limitKey = true;
        }
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
