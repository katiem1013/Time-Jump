using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Slots : MonoBehaviour, IPointerClickHandler
{    
    public GameObject inventory;

    [System.Serializable]
    public enum property { usuable, displayable, empty};
    public property ItemProperty { get; private set; }

    private string displayImage;
    

    private void Start()
    {
        inventory = GameObject.Find("Inventory");
    }

    // checks what slot is being clicked on and unselects the previous one
    public void OnPointerClick(PointerEventData eventData)
    {
        inventory.GetComponent<Inventory>().previousSelectedSlot = inventory.GetComponent<Inventory>().currentSelectedSlot;
        inventory.GetComponent<Inventory>().currentSelectedSlot = this.gameObject;
    }

    // assigns the type of object
    public void AssignProperty(int orderNumber, string displayImage)
    {
        ItemProperty = (property)orderNumber;
        this.displayImage = displayImage;
    }

    // pops up image if it is a display image
    public void DisplayItem()
    {
        inventory.GetComponent<Inventory>().itemDisplayer.SetActive(true);
        inventory.GetComponent<Inventory>().itemDisplayer.GetComponent<Image>().sprite = 
            Resources.Load<Sprite>("Display Items/" + displayImage);
    }

    // removes item from inventory
    public void ClearItem()
    {
        ItemProperty = Slots.property.empty;
        displayImage = "";
        transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("EmptySlot");
    }
}
