using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AIRoom : MonoBehaviour
{
    [Header("Sprites")]
    public SpriteRenderer spriteRenderer;
    public Sprite newSprite;
    public Animator sceneTransition;

    [Header("Door Status")]
    // 0 is false, 1 is true
    static public float doorOpened = 0;
    static public float doorLocked = 1;

    [Header("Key")]
    private GameObject inventory;
    public string unlockItem;
    public AIMovement aiMovement;

    [Header("Dialog Box")]
    public string popUp;

    void Start()
    {
        inventory = GameObject.Find("Inventory");
    }

    private void Update()
    {
        sceneTransition = GameObject.FindGameObjectWithTag("SceneTransition").GetComponent<Animator>();

        if (doorOpened == 1)
            spriteRenderer.sprite = newSprite;
    }

    void OnMouseDown()
    {

        if (!EventSystem.current.IsPointerOverGameObject())
        {
            // checks if the player is holding the correct item
            if (inventory.GetComponent<Inventory>().currentSelectedSlot.gameObject.transform.GetChild(0).GetComponent<Image>().sprite.name == unlockItem)
            {
                doorLocked = 0;
                inventory.GetComponent<Inventory>().currentSelectedSlot.GetComponent<Slots>().ClearItem(); // removes the item from their inventory
            }
                

            if (doorLocked == 0) // opens the door
                doorOpened = 1;

            // if the player is not holiding the correct item
            if (doorLocked == 1 && inventory.GetComponent<Inventory>().currentSelectedSlot.gameObject.transform.GetChild(0).GetComponent<Image>().sprite.name != unlockItem)
            {
                DialogPopup pop = GameObject.FindGameObjectWithTag("Popups").GetComponent<DialogPopup>(); // gives a hint on how to unlock the door
                pop.PopUp(popUp);
            }

            if (doorOpened == 1) // loads the room
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

        sceneTransition.SetTrigger("Start");
        yield return new WaitForSeconds(1);

        // loads a room based on if the player is in the past or future
        if (sceneName == "Hallway")
            SceneManager.LoadScene("MPBedroom");

        else if (sceneName == "FutHallway")
            SceneManager.LoadScene("FutAIBedroom");
    }
}
