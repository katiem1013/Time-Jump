using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Alarm : MonoBehaviour
{
    public GameManager gameManager;

    public void Start()
    {
        gameManager = GetComponent<GameManager>();
    }

    // Start is called before the first frame update
    private void OnMouseDown()
    {
        // checks what scene the player is currently in
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        // turns the alarm on if the player is in the past
        if (sceneName == "Hallway")
            gameManager.alarmOn = true;

    }
}
