using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IceBlock : MonoBehaviour
{
     static public bool missing = false;

    // inventory variables
    public enum Property { usuable, displayable };
    public Property itemProperty;
    public string DisplayImage;
    public Sprite inventorySprite;
    private GameObject InventorySlots;

    private void Start()
    {
        // adds the inventory slots referenece to this script
        InventorySlots = GameObject.Find("Slots");
    }

    private void Update()
    {
        // this makes sure the item stays deleted when the player reenters a scene
        if (missing)
            Destroy(gameObject);
    }

    public void OnInteract()
    {
        // if the item hasn't been taken when clicked on it will pick up and set it to missing 
        if(!missing)
        {
            missing = true;
            ItemPickUp();
        }
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
    }
}
