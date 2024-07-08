using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TrainTicket : MonoBehaviour
{
    static public bool ticketTaken;

    [Header("Inventory")]
    private GameObject inventory;
    public string DisplayImage;
    public Sprite inventorySprite;
    private GameObject InventorySlots;
    public enum Property { usuable, displayable };
    public Property itemProperty;

    private void Start()
    {
        InventorySlots = GameObject.Find("Slots");
    }

    private void Update()
    {
        // destroys ticket if taken
        if (ticketTaken)
        {
            Destroy(gameObject);
        }
    }

    void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            // picks up ticket if it hasn't been
            if (ticketTaken == false)
            {
                ticketTaken = true;
                ItemPickUp();
            }
        }
    }

    // adds item to inventory
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
