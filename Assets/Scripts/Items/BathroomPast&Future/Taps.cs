using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Taps : MonoBehaviour
{
    Animator animator;

    [Header("Inventory")]
    private GameObject inventory;
    public string DisplayImage;
    public Sprite inventorySprite;
    private GameObject InventorySlots;
    public enum Property { usuable, displayable };
    public Property itemProperty;

    [Header("Inventory Items")]
    public string wrench;
    public string wateringCan;

    static public bool wrenchAdded = false;
    static public bool tapsRunning = false;

    public bool tapsRunningSaved = false;
    static public bool fillWatercan;

    private void Start()
    {
        InventorySlots = GameObject.Find("Slots");
        inventory = GameObject.Find("Inventory");
        animator = GetComponent<Animator>();
    }

    private void Update()
    {        
        tapsRunningSaved = tapsRunning;

        // fills the watering can
        if (fillWatercan)
        {
            ItemPickUp();
            fillWatercan = false;
        }
    }

    void OnMouseDown()
    {
        // checks what scene the player is currently in
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        // checks if the wrench is already added and if the player has the wrench
        if (wrenchAdded == false && inventory.GetComponent<Inventory>().currentSelectedSlot.gameObject.transform.GetChild(0).GetComponent<Image>().sprite.name == wrench)
        {
            Invoke("WrenchAttached", 1);
            inventory.GetComponent<Inventory>().currentSelectedSlot.GetComponent<Slots>().ClearItem(); // removes wrench from inventory
            animator.SetTrigger("AddedWrench");
        }

        // checks which scene the player is in
        if (wrenchAdded == true && sceneName == "FutBathroom")
        {
            animator.SetTrigger("TurnOn"); // turns the tap on
            tapsRunning = true;
        }

        // checks if the player is holidng a watering can
        if (sceneName == "Bathroom" && inventory.GetComponent<Inventory>().currentSelectedSlot.gameObject.transform.GetChild(0).GetComponent<Image>().sprite.name == wateringCan)
        {
            inventory.GetComponent<Inventory>().currentSelectedSlot.GetComponent<Slots>().ClearItem(); // removes watering can from inventory
            fillWatercan = true;   
        }

        // checks if the player is holding a watering can
        if (wrenchAdded && sceneName == "FutBathroom" && inventory.GetComponent<Inventory>().currentSelectedSlot.gameObject.transform.GetChild(0).GetComponent<Image>().sprite.name == wateringCan)
        {
            inventory.GetComponent<Inventory>().currentSelectedSlot.GetComponent<Slots>().ClearItem(); // removes watering can from inventory
            fillWatercan = true;
        }

    }
    
    // adds wrench
    void WrenchAttached()
    {
        wrenchAdded = true;
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
