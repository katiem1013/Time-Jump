using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FlowerPot : MonoBehaviour
{
    static public bool plantWatered;
    public string wateringCan;

    [Header("Sprites")]
    public SpriteRenderer spriteRenderer;
    public Sprite tallPlant;

    [Header("Inventory")]
    private GameObject inventory;
    public enum Property { usuable, displayable };
    public Property itemProperty;

    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.Find("Inventory");
    }

    // Update is called once per frame
    void Update()
    {
        // checks what scene the player is currently in
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        // changes the plant to taller
        if (plantWatered && sceneName == "FutStudy")
            spriteRenderer.sprite = tallPlant;

    }

    private void OnMouseDown()
    {
        // checks what scene the player is currently in
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        // waters the plant
        if (inventory.GetComponent<Inventory>().currentSelectedSlot.gameObject.transform.GetChild(0).GetComponent<Image>().sprite.name == wateringCan && sceneName == "Study")
        {
            plantWatered = true;
            inventory.GetComponent<Inventory>().currentSelectedSlot.GetComponent<Slots>().ClearItem(); // removes the watering can from the players inventory
        }
    }
}
