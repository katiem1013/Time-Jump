using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MPDoor : MonoBehaviour
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

            // checks if the right item is used
            if (inventory.GetComponent<Inventory>().currentSelectedSlot.gameObject.transform.GetChild(0).GetComponent<Image>().sprite.name == unlockItem)
            {
                doorLocked = 0;
                inventory.GetComponent<Inventory>().currentSelectedSlot.GetComponent<Slots>().ClearItem();
            }

            if (doorLocked == 0)
                doorOpened = 1;

            // gives a hint if the right object is not used
            if (doorLocked == 1 && inventory.GetComponent<Inventory>().currentSelectedSlot.gameObject.transform.GetChild(0).GetComponent<Image>().sprite.name != unlockItem)
            {
                DialogPopup pop = GameObject.FindGameObjectWithTag("Popups").GetComponent<DialogPopup>();
                pop.PopUp(popUp);
            }

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

        sceneTransition.SetTrigger("Start"); // plays scene transition
        yield return new WaitForSeconds(1);

        // loads the player into a room based on where they are
        if (sceneName == "Hallway")
            SceneManager.LoadScene("AIBedroom");

        else if (sceneName == "FutHallway")
            SceneManager.LoadScene("FutMPBedroom");
    }
}
