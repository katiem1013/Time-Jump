using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FileMenu : MonoBehaviour
{
    [Header("Canvas")]
    public GameObject fileCanvas;
    public float correctCodes = 0;
    public Animator sceneTransition;

    public void OnEnable()
    {
        // pauses the game
        Time.timeScale = 0;
    }

    public void OnDisable()
    {
        // resumes the game
        Time.timeScale = 1;
    }

    public void Update()
    {
        // closes the file
        if (Input.GetKeyDown(KeyCode.Q))
            fileCanvas.SetActive(false);

        // checks if the codes are correct
        if (correctCodes == 6)
            StartCoroutine(LoadRoom());
    }

    // adds 1 to the correct code 
    public void AddCorrect()
    {
        correctCodes++;
    }

    // minuses 1 to the correct code
    public void RemoveCorrect()
    {
        correctCodes--;
    }

    IEnumerator LoadRoom()
    {
        // loads the final scene if the game is won
        sceneTransition.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Game Won");
        sceneTransition.SetTrigger("End");
    }
}
