using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class KitchenCupboard : MonoBehaviour
{
    [Header("Sprites")]
    public SpriteRenderer spriteRenderer;
    public Sprite newSprite;
    public Sprite emptyCupboard;

    [Header("Cupboards")]
    static public bool openCupboard = false;
    static public bool saucePanTaken = false;

    [Header("Inventory")]
    public string DisplayImage;
    public Sprite inventorySprite;
    public Sprite inventorySprite2;
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
        // changes the sprites
        if (openCupboard)
            spriteRenderer.sprite = newSprite;

        if (saucePanTaken)
            spriteRenderer.sprite = emptyCupboard;

    }

    private void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            // opens the cupboard
            if (!openCupboard)
            {
                Invoke("OpenDraw", 0.5f);
                spriteRenderer.sprite = newSprite;
            }
            
            // checks if everything has been taken and takes it if not
            if (openCupboard && !saucePanTaken)
            {
                ItemPickUp();
                ItemPickUp2();
                saucePanTaken = true;
                spriteRenderer.sprite = emptyCupboard;
            }
        }
    }

    // opens the cupboard
    void OpenDraw()
    {
        openCupboard = true;
    }

    // picks up the first item
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

    // picks up the second item
    void ItemPickUp2()
    {
        foreach (Transform slot in InventorySlots.transform)
        {

            if (slot.transform.GetChild(0).GetComponent<Image>().sprite.name == "EmptySlot")
            {
                slot.transform.GetChild(0).GetComponent<Image>().sprite = inventorySprite2;
                slot.GetComponent<Slots>().AssignProperty((int)itemProperty, DisplayImage);
                break;
            }
        }
    }
}
