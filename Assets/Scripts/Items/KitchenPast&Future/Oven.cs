using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Oven : MonoBehaviour
{
    [Header("UI Canvas")]
    public GameObject waterCanvas;

    [Header("Sprites")]
    public SpriteRenderer spriteRenderer;
    public Sprite saucePanOnStove;
    public Sprite saucePanBoiling;
    public Sprite emptySlot;

    [Header("Mircowave")]
    static public bool saucepanOn = false;
    static public bool saucepanOnBoiling = false;
    public string saucepan;
    public string can;

    [Header("Inventory")]
    public string DisplayImage;
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
        if (saucepanOnBoiling)
            spriteRenderer.sprite = saucePanBoiling;
    }

    void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            // checks what item the player is holding 
            if (inventory.GetComponent<Inventory>().currentSelectedSlot.gameObject.transform.GetChild(0).GetComponent<Image>().sprite.name == saucepan)
            {
                saucepanOn = true;
                inventory.GetComponent<Inventory>().currentSelectedSlot.GetComponent<Slots>().ClearItem(); // removes it from the inventory
                spriteRenderer.sprite = saucePanOnStove;
            }

            // checks if the sauce pan is already on the stove and if the player is holding the right item
            if (saucepanOn = true && inventory.GetComponent<Inventory>().currentSelectedSlot.gameObject.transform.GetChild(0).GetComponent<Image>().sprite.name == can)
            {
                inventory.GetComponent<Inventory>().currentSelectedSlot.GetComponent<Slots>().ClearItem(); // removes it from the inventory
                saucepanOnBoiling = true;
                Invoke("Microwaving", 3);
            }
        }
    }

    // allows the canvas to appear
    void Microwaving()
    {
        saucepanOn = false;
        Interact();
    }

    // enables the canvas
    public void Interact()
    {
        waterCanvas.SetActive(true);
    }

    // disables the canvas
    public void StopInteract()
    {
        waterCanvas.SetActive(false);
        saucepanOnBoiling = false;
    }
}
