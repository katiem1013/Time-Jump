using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryLabels : MonoBehaviour
{
    public TextMeshProUGUI itemText;

    // Start is called before the first frame update
    void Start()
    {
        // gives the label an outline
        itemText.outlineWidth = .2f;
        itemText.outlineColor = new Color32(0, 0, 0, 255);
    }

    // Update is called once per frame
    void Update()
    {
        // sets the text as blank if theres nothing in the inventory
        if (gameObject.GetComponentInParent<Image>().sprite.name == "EmptySlot")
            itemText.text = "";

        // sets the text as the item name
        else
            itemText.text = gameObject.GetComponentInParent<Image>().sprite.name;
    }
}
