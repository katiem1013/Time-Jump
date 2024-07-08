using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagnetKey : MonoBehaviour
{
    static public bool missing = false;

    // inventory variables
    public enum Property { usuable, displayable };
    public Property itemProperty;
    public string DisplayImage;
    public Sprite inventorySprite;
    private GameObject InventorySlots;
    private GameObject inventory;


    private void Start()
    {
        // adds the inventory slots referenece to this scritp
        InventorySlots = GameObject.Find("Slots");
        inventory = GameObject.Find("Inventory");
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
        if (!missing && inventory.GetComponent<Inventory>().currentSelectedSlot.gameObject.transform.GetChild(0).GetComponent<Image>().sprite.name == "Magnet")
        {
            missing = true;
            ItemPickUp();
            Destroy(gameObject);
            LivingRoom.keyGot = true;
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
