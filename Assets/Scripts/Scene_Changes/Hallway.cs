using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement; 

public class Hallway : MonoBehaviour
{

    public Animator sceneTransition;

    private void Update()
    {
        sceneTransition = GameObject.FindGameObjectWithTag("SceneTransition").GetComponent<Animator>();
    }

    void OnMouseDown()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (!EventSystem.current.IsPointerOverGameObject() && !MicrowaveNew.mircowaveOn && !Oven.saucepanOnBoiling)
        {
            // loads the hallway
            StartCoroutine(LoadRoom());
        }
    }

    IEnumerator LoadRoom()
    {
        // checks what scene the player is currently in
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        sceneTransition.SetTrigger("Start"); // starts the scene transition
        yield return new WaitForSeconds(1);

        // checks which hallway to load based on where the player is 
        if (sceneName == "Kitchen" || sceneName == "Livingroom" || sceneName == "Bathroom" || sceneName == "Study" || sceneName == "AIBedroom" || sceneName == "MPBedroom")
            SceneManager.LoadScene("Hallway");

        else
            SceneManager.LoadScene("FutHallway");
    }
}
