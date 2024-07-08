using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Items : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite newSprite;
    public Sprite inventorySprite;

    static public bool missing = false;
    public bool canAddWaypoint; // not making it static so that it can work for multiple objects with the same script
                                // however the downside to this is that everytime the player leaves the room and comes back it will add another one 
    public AIMovement aIMovement;

    [Header("Inventory")]
    private GameObject InventorySlots;
    public enum Property { usuable, displayable };

    public Property itemProperty;

    public string DisplayImage;

    private void Start()
    {
        InventorySlots = GameObject.Find("Slots");
    }

    private void Update()
    {

        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        // checks if the item has been takem
        if (missing && sceneName == "Hallway")
            spriteRenderer.sprite = newSprite;
    }

    void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            // picks the item up and set it to missing 
            if (missing == false)
            {
                missing = true;
                ItemPickUp();
                Destroy(gameObject);
            }
        }
    }
   
    // the AI adds the area to it's patrol if it sees it's missing 
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "AI" && missing == true && canAddWaypoint)
        {
            canAddWaypoint = false; // stops it from adding multiple waypoints for the same object
            aIMovement.AddWaypoints();
        }
    }

    // picks up the items
    void ItemPickUp() 
    {
        foreach(Transform slot in InventorySlots.transform)
        {
            
            if(slot.transform.GetChild(0).GetComponent<Image>().sprite.name == "EmptySlot")
            {
                slot.transform.GetChild(0).GetComponent<Image>().sprite = inventorySprite;
                slot.GetComponent<Slots>().AssignProperty((int) itemProperty, DisplayImage);
                break;
            }
        }
    }
}
