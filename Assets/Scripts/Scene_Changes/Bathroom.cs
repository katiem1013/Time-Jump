using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Bathroom : MonoBehaviour
{
    [Header("Sprites")]
    public SpriteRenderer spriteRenderer;
    public Sprite newSprite;
    public Animator sceneTransition;

    [Header("UI Canvas")]
    static public bool interactable = true;
    public GameObject keycodeCanvas;
    static public bool correctKeyCode;

    [Header("Door Status")]
    // 0 is false, 1 is true
    static public float doorOpened = 0;
    static public float doorLocked = 1;
    static public float futDoorOpened = 0;

    [Header("Dialog Box")]
    public string popUp;

    void Start()
    {
        
    }

    private void Update()
    {
        sceneTransition = GameObject.FindGameObjectWithTag("SceneTransition").GetComponent<Animator>();

        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        // checks if the door is open and what scene the player is in
        if (doorOpened == 1 && sceneName == "Hallway")
            spriteRenderer.sprite = newSprite;

        if (futDoorOpened == 1 && sceneName == "FutHallway")
            spriteRenderer.sprite = newSprite;

        if (sceneName == "FutHallway")
            futDoorOpened = 1;

        // checks if the correct keycode is inputeds
        if (correctKeyCode && sceneName == "Hallway")
        {
            doorLocked = 0;
            StopInteract();
            interactable = false;
        }
    }

    void OnMouseDown()
    {

        if (!EventSystem.current.IsPointerOverGameObject())
        {
            Scene currentScene = SceneManager.GetActiveScene();
            string sceneName = currentScene.name;

            // checks if the door is locked or open and changes the result
            if (doorLocked == 0 && sceneName == "Hallway")
                doorOpened = 1;

            if (doorLocked == 1 && sceneName == "Hallway")
            {
                Interact();
            }

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

        sceneTransition.SetTrigger("Start"); // plays transition for loading scenes
        yield return new WaitForSeconds(1);

        // loads the room based on if the player is in the past or future
        if (sceneName == "Hallway")
            SceneManager.LoadScene("Bathroom");

        else if (sceneName == "FutHallway")
            SceneManager.LoadScene("FutBathroom");
    }

    // brings up the canvas
    public void Interact()
    {
        if (interactable)
            keycodeCanvas.SetActive(true);
    }

    // closes the canvas
    public void StopInteract()
    {
        keycodeCanvas.SetActive(false);
    }

}
