using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory: MonoBehaviour
{
    public Sprite emptySlot;
    public GameObject currentSelectedSlot { get; set; }
    public GameObject previousSelectedSlot { get; set; }

    private GameObject slots;
    public GameObject itemDisplayer { get; private set; }
    public enum Property { usuable, displayable };
    public Property itemProperty;

    private void Start()
    {
        slots = GameObject.Find("Slots");
        itemDisplayer = GameObject.Find("ItemDisplayer");
        InitalizeInventory();
        currentSelectedSlot = slots;
    }

    private void Update()
    {
        SelectSlot();
    }

    void InitalizeInventory()
    {
        // sets the slots and sets all the slots to empty
        var slots = GameObject.Find("Slots");
        itemDisplayer.SetActive(false);
        foreach (Transform slot in slots.transform)
        {
            slot.transform.GetChild(0).GetComponent<Image>().sprite = emptySlot;
        }
    }

    // checks if a slot is selected and makes it a different colour
    void SelectSlot()
    {
        foreach(Transform slot in slots.transform)
        {
            if(slot.gameObject == currentSelectedSlot && slot.GetComponent<Slots>().ItemProperty == Slots.property.usuable)
                slot.GetComponent<Image>().color = new Color(.9f, .9f, .9f, 1);
                        
            else if(slot.gameObject == currentSelectedSlot && slot.GetComponent<Slots>().ItemProperty == Slots.property.displayable)
                slot.GetComponent<Slots>().DisplayItem();

            else
                slot.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
    }

    // stops items from displaying
    public void StopDisplayItem()
    {
        currentSelectedSlot = null;
        itemDisplayer.SetActive(false);
    }
}
