using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WateringCan : MonoBehaviour
{

    static public bool missing;

    [Header("Inventory")]
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
        // destorys the gameobject if it has been picked up
        if (missing)
            Destroy(gameObject);
    }

    void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            // checks if the item has already been picked up
            if (!missing)
            {
                missing = true;
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
