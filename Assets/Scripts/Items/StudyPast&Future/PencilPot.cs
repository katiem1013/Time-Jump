using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PencilPot : MonoBehaviour
{
    [Header("Pencils")]
    static public bool missing = false;
    public Erasing erasing;

    [Header("Sprites")]
    public SpriteRenderer spriteRenderer;
    public Sprite missingPencil;

    [Header("Inventory")]
    public string DisplayImage;
    public Sprite inventorySprite;
    private GameObject InventorySlots;
    private GameObject inventory;
    public enum Property { usuable, displayable };
    public Property itemProperty;

    private void Start()
    {
        InventorySlots = GameObject.Find("Slots");
    }

    private void Update()
    {
        // checks if the pencil has been taken and changes the sprite
        if (missing == true)
        {
            erasing.pencil = true;
            spriteRenderer.sprite = missingPencil;
        }
    }

    void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            // picks up the pencil
            if (missing == false)
            {
                missing = true;
                erasing.pencil = true;
                ItemPickUp();
                spriteRenderer.sprite = missingPencil;
            }
        }
    }

    // puts the pencil in the players inventory
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
