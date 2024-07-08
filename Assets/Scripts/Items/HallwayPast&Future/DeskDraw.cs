using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeskDraw : MonoBehaviour
{
    [Header("Sprites")]
    public SpriteRenderer spriteRenderer;
    public Sprite newSprite;

    [Header("Draws")]
    static public bool openDraw = false;

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
        InventorySlots = GameObject.Find("Slots");
    }

    // Update is called once per frame
    void Update()
    {
        if (openDraw)
            spriteRenderer.sprite = newSprite;
    }

    private void OnMouseDown()
    {
        if (!openDraw)
        {
            // changes the sprite
            spriteRenderer.sprite = newSprite;
            openDraw = true;
            ItemPickUp();
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
        
        openDraw = false;
    }
}
