using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MicrowaveNew : MonoBehaviour
{
    [Header("Sprites")]
    public SpriteRenderer spriteRenderer;
    public Sprite microwaveOn;
    public Sprite microwaveNormal;
    public Sprite emptySlot;

    [Header("Mircowave")]
    static public bool mircowaveOn = false;
    public string microwaveableItem;

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
        // checks if the mircowave is on
        if (mircowaveOn)
            spriteRenderer.sprite = microwaveOn;
        else
            spriteRenderer.sprite = microwaveNormal;
    }

    void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            // checks if the mircowavable object is in the players inventory
            if (inventory.GetComponent<Inventory>().currentSelectedSlot.gameObject.transform.GetChild(0).GetComponent<Image>().sprite.name == microwaveableItem)
            {
                mircowaveOn = true;
                inventory.GetComponent<Inventory>().currentSelectedSlot.GetComponent<Slots>().ClearItem(); // takes object out of inventory
                Invoke("Microwaving", 3);
            }

            else
                print("Nothing To Microwave");
        }
    }

    void Microwaving()
    {
        mircowaveOn = false;
        ItemPickUp();
    }

    // adds object to inventory
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
