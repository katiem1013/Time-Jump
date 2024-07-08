using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LivingRoom : MonoBehaviour
{
    [Header("Sprites")]
    public SpriteRenderer spriteRenderer;
    public Sprite newSprite;
    public GameObject underDoor;
    public Animator sceneTransition;

    [Header("Door Status")]
    // 0 is false, 1 is true
    static public float doorOpened = 0;
    static public float doorLocked = 1;

    public bool inRange = false;

    [Header("Key")]
    private GameObject inventory;
    public string unlockItem;
    public string magnet;
    static public bool keyDropped = false;
    static public bool keyGot = false;

    [Header("Dialog Box")]
    public string popUp;

    [Header("Inventory")]
    public string DisplayImage;
    public Sprite inventorySprite;
    private GameObject InventorySlots;
    public enum Property { usuable, displayable };
    public Property itemProperty;

    void Start()
    {
        InventorySlots = GameObject.Find("Slots");
        inventory = GameObject.Find("Inventory");
    }

    private void Update()
    {
        sceneTransition = GameObject.FindGameObjectWithTag("SceneTransition").GetComponent<Animator>();

        // changes the sprite
        if (doorOpened == 1)
            spriteRenderer.sprite = newSprite;
    }

    void OnMouseDown()
    {

        if (!EventSystem.current.IsPointerOverGameObject())
        {
            Scene currentScene = SceneManager.GetActiveScene();
            string sceneName = currentScene.name;

            // checks if the player is holding the magnet
            if (inventory.GetComponent<Inventory>().currentSelectedSlot.gameObject.transform.GetChild(0).GetComponent<Image>().sprite.name == magnet && sceneName == "Hallway")
            {
                ItemPickUp();
                inventory.GetComponent<Inventory>().currentSelectedSlot.GetComponent<Slots>().ClearItem(); // removes item from inventory
            }

            // checks if the player is holding the key
            if (inventory.GetComponent<Inventory>().currentSelectedSlot.gameObject.transform.GetChild(0).GetComponent<Image>().sprite.name == unlockItem && sceneName == "Hallway")
            {
                doorLocked = 0;
                inventory.GetComponent<Inventory>().currentSelectedSlot.GetComponent<Slots>().ClearItem(); // removes item from inventory
            }

            // unlocks the door
            if (doorLocked == 0)
                doorOpened = 1;

            if (doorOpened == 1)
            {
                StartCoroutine(LoadRoom());
            }
        }
    }

    IEnumerator LoadRoom()
    {
        // checks what scene the player is currently in
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        sceneTransition.SetTrigger("Start"); // starts the scene transition
        yield return new WaitForSeconds(1);

        // checks what scene the player is in 
        if (sceneName == "Hallway")
            SceneManager.LoadScene("LivingRoom");

        else if (sceneName == "FutHallway")
            SceneManager.LoadScene("FutLivingRoom");
    }

    // gives the player the items
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
