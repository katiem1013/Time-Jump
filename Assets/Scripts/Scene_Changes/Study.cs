using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Study : MonoBehaviour
{
    [Header("Sprites")]
    public SpriteRenderer spriteRenderer;
    public Sprite newSprite; 
    public Animator sceneTransition;

    [Header("Door Status")]
    // 0 is false, 1 is true
    static public float doorOpened = 0;
    static public float doorLocked = 1;
    static public float futDoorOpened = 0;

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

        // changes the sprite
        if (doorOpened == 1)
            spriteRenderer.sprite = newSprite;
    }

    void OnMouseDown()
    {

        if (!EventSystem.current.IsPointerOverGameObject())
        {
            // checks what scene the player is in 
            Scene currentScene = SceneManager.GetActiveScene();
            string sceneName = currentScene.name;

            // gives a hint if the right item isn't being held
            if (doorLocked == 1 && inventory.GetComponent<Inventory>().currentSelectedSlot.gameObject.transform.GetChild(0).GetComponent<Image>().sprite.name != unlockItem && sceneName == "Hallway")
            {
                DialogPopup pop = GameObject.FindGameObjectWithTag("Popups").GetComponent<DialogPopup>();
                pop.PopUp(popUp);
            }

            // opens the door in the future
            if (futDoorOpened == 0 && sceneName == "FutHallway")
            {
                DialogPopup pop = GameObject.FindGameObjectWithTag("Popups").GetComponent<DialogPopup>();
                pop.PopUp(popUp);
                Invoke("OpenDoor", 1);
            }

            // checks if the player is holding the right item
            if (inventory.GetComponent<Inventory>().currentSelectedSlot.gameObject.transform.GetChild(0).GetComponent<Image>().sprite.name == unlockItem && sceneName == "Hallway")
            {
                doorLocked = 0;
                inventory.GetComponent<Inventory>().currentSelectedSlot.GetComponent<Slots>().ClearItem(); // removes item from inventory
            }

            // changes the sprite
            if (futDoorOpened == 1 && sceneName == "FutHallway")
                spriteRenderer.sprite = newSprite;

            // checks if the door is open
            if (doorLocked == 0 && sceneName == "Hallway")
                doorOpened = 1;

            // loads room
            if (doorOpened == 1 && sceneName == "Hallway")
            {
                StartCoroutine(LoadRoom());
            }

            if (futDoorOpened == 1 && sceneName == "FutHallway")
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

        sceneTransition.SetTrigger("Start"); // plays scene transition
        yield return new WaitForSeconds(1);

        // loads room based on what scene the player is in
        if (sceneName == "Hallway")
        {
            Destroy(GameObject.FindGameObjectWithTag("Inventory"));
            SceneManager.LoadScene("Study");
        }

        else if (sceneName == "FutHallway")
            SceneManager.LoadScene("FutStudy");
    }

    // opens the door
    void OpenDoor()
    {
        futDoorOpened = 1;
    }

}
