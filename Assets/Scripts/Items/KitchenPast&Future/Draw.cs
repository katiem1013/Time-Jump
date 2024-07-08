using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Draw : MonoBehaviour
{
    [Header("Sprites")]
    public SpriteRenderer spriteRenderer;
    public Sprite handleOn;
    public Sprite emptySlot;

    [Header("Draw")]
    static public bool isHandleOn = false;
    static public bool itemPickUpable = false;
    public string doorHandle;

    [Header("Inventory")]
    public string DisplayImage;
    public Sprite inventorySprite;
    private GameObject InventorySlots;
    private GameObject inventory;
    public enum Property { usuable, displayable };
    public Property itemProperty;

    void Start()
    {
        inventory = GameObject.Find("Inventory");
        InventorySlots = GameObject.Find("Slots");
    }

    private void Update()
    {
        if (isHandleOn)
            spriteRenderer.sprite = handleOn;
    }

    void OnMouseDown()
    {
        // checks if the hanfle is already on
        if (!isHandleOn)
        {
            // checks if the player is using the door handle
            if (inventory.GetComponent<Inventory>().currentSelectedSlot.gameObject.transform.GetChild(0).GetComponent<Image>().sprite.name == doorHandle)
            {
                isHandleOn = true;
                inventory.GetComponent<Inventory>().currentSelectedSlot.GetComponent<Slots>().ClearItem(); // takes the items out of the inventory
                itemPickUpable = true;
            }
        }

        // checks if the item is pick upable
        if(itemPickUpable)
        {
            ItemPickUp();
            itemPickUpable = false;
        }           
    }

    // gives the player the item in the first avaliable empty slot
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
