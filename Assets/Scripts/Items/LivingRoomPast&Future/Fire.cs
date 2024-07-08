using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Fire : MonoBehaviour
{
    [Header("Sprites")]
    public SpriteRenderer spriteRenderer;
    public Sprite fire1, fire2, soot;

    static public bool fireBurning = false; 
    public string matchbox;

    [Header("Inventory")]
    public Sprite inventorySprite;
    private GameObject inventory;

    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.Find("Inventory");

        // plays the fire animation
        if (fireBurning)
        {
            StartCoroutine(FireBurningAnim());
        }
    }

    // Update is called once per frame
    void Update()
    {
        // checks what scene the player is in
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        // sets the soot image so the player can see the code
        if (fireBurning && sceneName == "FutLivingroom")
        {
            spriteRenderer.sprite = soot;
        }
    }


    private void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            // checks what scene the player is in
            Scene currentScene = SceneManager.GetActiveScene();
            string sceneName = currentScene.name;

            // starts the fire if the matches are used
            if (inventory.GetComponent<Inventory>().currentSelectedSlot.gameObject.transform.GetChild(0).GetComponent<Image>().sprite.name == matchbox && sceneName == "Livingroom")
            {
                fireBurning = true;
                inventory.GetComponent<Inventory>().currentSelectedSlot.GetComponent<Slots>().ClearItem(); // removes the item from the inventory
                StartCoroutine(FireBurningAnim());
            }
        }
    }

    IEnumerator FireBurningAnim()
    {
        // changes the fire between two images so it moves
        spriteRenderer.sprite = fire1;
        yield return new WaitForSeconds(1);
        spriteRenderer.sprite = fire2;
        yield return new WaitForSeconds(1);
        StartCoroutine(FireBurningAnim());
    }
}
