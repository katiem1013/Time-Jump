using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [Header("Canvas")]
    public GameObject mainMenu;
    public GameObject controlMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // starts game
    public void StartGame()
    {
        SceneManager.LoadScene("Hallway");
    }

    // shows controls
    public void ControlMenu()
    {
        controlMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    // return to main menu
    public void ReturnMenu()
    {
        controlMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    // exit game
    public void ExitGame()
    {
        Application.Quit();
    }
}
