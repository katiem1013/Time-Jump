using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Kitchen : MonoBehaviour
{
    [Header("Sprites")]
    public SpriteRenderer spriteRenderer;
    public Sprite newSprite;
    public Animator sceneTransition;

    [Header("Door Status")]
    // 0 is false, 1 is true
    static public float doorOpened = 0;
    static public float doorLocked = 1;

    public bool inRange = false;

    [Header("Key")]
    private GameObject inventory;
    public string unlockItem;

    [Header("Dialog Box")]
    public string popUp;

    void Start()
    {
        inventory = GameObject.Find("Inventory");
    }

    private void Update()
    {
        sceneTransition = GameObject.FindGameObjectWithTag("SceneTransition").GetComponent<Animator>();

        // changes the sprites
        if (doorOpened == 1)
            spriteRenderer.sprite = newSprite;
    }

    void OnMouseDown()
    {

        if (!EventSystem.current.IsPointerOverGameObject())
        {
            Scene currentScene = SceneManager.GetActiveScene();
            string sceneName = currentScene.name;

            // checks if the player is holding the correct item
            if (inventory.GetComponent<Inventory>().currentSelectedSlot.gameObject.transform.GetChild(0).GetComponent<Image>().sprite.name == unlockItem)
            {
                doorLocked = 0;
                inventory.GetComponent<Inventory>().currentSelectedSlot.GetComponent<Slots>().ClearItem(); // takes the item out of the inventory
            }

            if (doorLocked == 0)
                doorOpened = 1;

            // gives a hint if the item isn't correct
            if (doorLocked == 1 && inventory.GetComponent<Inventory>().currentSelectedSlot.gameObject.transform.GetChild(0).GetComponent<Image>().sprite.name != unlockItem)
            {
                DialogPopup pop = GameObject.FindGameObjectWithTag("Popups").GetComponent<DialogPopup>();
                pop.PopUp(popUp);
            }

            // loads the room
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

        // plays scene transition
        sceneTransition.SetTrigger("Start");
        yield return new WaitForSeconds(1);

        // checks which scene the player needs to go to
        if (sceneName == "Hallway")
            SceneManager.LoadScene("Kitchen");

        else if (sceneName == "FutHallway")
            SceneManager.LoadScene("FutKitchen"); 
    }


    // checks if the player is in front of the door
    void OnTriggerEnter2D (Collider2D col)
    {
        if (col.gameObject.tag == "Player")
            inRange = true;
    }

    // checks if the player is no longer in front of the door
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
            inRange = false;
    }

}
