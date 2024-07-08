using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("AI")]
    public GameObject AI;
    public AIMovement aiMove;

    [Header("Distractions")]
    static public bool alarmIsOn;
    public bool alarmOn;
    public bool playerSeen;

    [Header("Scene Management")]
    static public string sceneLocation;
    public string publicSceneLocation; // used for testing
    static bool playerDetected, enterRoom;
    static public string playerSceneLocation;
    public string publicPlayerSceneLocation; // used for testing

    [Header("UI")]
    public GameObject detectedImage;
    public GameObject notDetectedImage, searchingImage;
    public TextMeshProUGUI currentTense;

    static public bool once_call;

    // Start is called before the first frame update
    void Awake()
    {
        if (!once_call)
        {
            DontDestroyOnLoad(gameObject);
            once_call = true;
        }

        else
        {
            Destroy(gameObject);
        }

    }

    void Start()
    {
        AI = FindInActiveObjectByTag("AI");
    }

    // Update is called once per frame
    void Update()
    {
        // checks what scene the player is currently in
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        publicPlayerSceneLocation = playerSceneLocation;
        sceneLocation = publicSceneLocation;
        UIManager(); // starts the UI

        alarmIsOn = alarmOn;
        
        // sets the AI to public in the hallway
        if (publicPlayerSceneLocation == "Hallway" && publicSceneLocation == "Hallway")
            AI.SetActive(true);

        // if the AI is else where but cannot see the player sets as inactive
        else if (!aiMove.detected && sceneName != "Hallway")
            AI.SetActive(false);

        // spawns the player into the room if they are following the player
        else if (aiMove.detected && (sceneName != "Hallway" || sceneName != "FutHallway" || sceneName != "FutKitchen" || sceneName != "FutLivingroom" || sceneName != "FutBathroom" || sceneName != "FutStudy" || sceneName != "FutAIBedroom" || sceneName != "FutMPBedroom") && !enterRoom)
        {
            AI.SetActive(false);
            Invoke("SpawnInRoom", 5);
            enterRoom = true;
        }

        // adds a delay to the AI entering the hallway
        else if (sceneLocation != playerSceneLocation && playerSceneLocation == "Hallway")
            Invoke("EnterHallway", 2);

        else if (sceneLocation == playerSceneLocation && !aiMove.isSearching)
            Invoke("EnterHallway", 2);

        // sets the AI of not knowing where the player is
        else if (sceneName != "Hallway")
        {
            aiMove.detected = false;
            aiMove.isSearching = false;
        }
    }

    public void UIManager()
    {
        // changes the image based on if the player is detected or not
        if (aiMove.detected)
        {
            detectedImage.SetActive(true);
            searchingImage.SetActive(false);
            notDetectedImage.SetActive(false);
        }

        if (aiMove.isSearching && !aiMove.detected)
        {
            detectedImage.SetActive(false);
            searchingImage.SetActive(true);
            notDetectedImage.SetActive(false);
        }

        if (!aiMove.detected && !aiMove.isSearching)
        {
            detectedImage.SetActive(false);
            searchingImage.SetActive(false);
            notDetectedImage.SetActive(true);
        }

        // checks what scene the player is currently in
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        // sets the tense so the player knows where they are
        if (sceneName == "FutHallway" || sceneName == "FutKitchen" || sceneName == "FutLivingroom" || sceneName == "FutBathroom" || sceneName == "FutStudy" || sceneName == "FutAIBedroom" || sceneName == "FutMPBedroom")
        {
            currentTense.text = "Future";
            detectedImage.SetActive(false);
            searchingImage.SetActive(false);
            notDetectedImage.SetActive(true);
        }

        else
            currentTense.text = "Past";

    }

    public void SpawnInRoom()
    {
        // checks what scene the player is currently in
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        // spawns the player in the room, in the correct position
        if (sceneName != "Hallway" || sceneName != "FutHallway" || sceneName != "FutKitchen" || sceneName != "FutLivingroom" || sceneName != "FutBathroom" || sceneName != "FutStudy" || sceneName != "FutAIBedroom" || sceneName != "FutMPBedroom")
        {
            AI.SetActive(true);
            AI.transform.position = aiMove.spawnPoint.transform.position;
        }

        else
            enterRoom = false;
    }

    void EnterHallway()
    {
        // checks what scene the player is currently in
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        // puts the AI back in the hallway
        if(sceneName != "Hallway")
            AI.SetActive(false);

        if (sceneName == "Hallway")
            AI.SetActive(true);

    }

    // turns off the alarm
    public void TurnOffAlarm()
    {
        StartCoroutine(AlarmOff());
    }

    // delays the alarm turning off
    IEnumerator AlarmOff()
    {
        aiMove.moveSpeed = 0;
        yield return new WaitForSeconds(4);
        alarmOn = false;
        aiMove.moveSpeed = 2;
    }

    // finds inactive objects
    GameObject FindInActiveObjectByTag(string tag)
    {

        Transform[] objs = Resources.FindObjectsOfTypeAll<Transform>() as Transform[];
        for (int i = 0; i < objs.Length; i++)
        {
            if (objs[i].hideFlags == HideFlags.None)
            {
                if (objs[i].CompareTag(tag))
                {
                    return objs[i].gameObject;
                }
            }
        }
        return null;
    }

}
