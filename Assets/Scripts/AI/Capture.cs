using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Capture : MonoBehaviour
{
    public GameManager gameManager;
    public AIMovement aiMovement;
    public Movement player;
    public GameObject AI;

    [Header("Camera Data")]
    static public float kitchenCameraData;
    static public float studyCameraData;
    static public float livingroomCameraData;
    static public float bathroomCameraData;
    static public float aiBedroomCameraData;
    static public float mpBedroomCameraData;

    [Header("Camera Placement")]
    public GameObject kitchenCamera;
    public GameObject studyCamera;
    public GameObject livingroomCamera;
    public GameObject bathroomCamera;
    public GameObject aiBedroomCamera;
    public GameObject mpBedroomCamera;

    public void Update()
    {
        // finds all the cameras
        kitchenCamera = FindInActiveObjectByTag("KitCam");
        studyCamera = FindInActiveObjectByTag("StudCam");
        livingroomCamera = FindInActiveObjectByTag("LRCam");
        bathroomCamera = FindInActiveObjectByTag("BRCam");
        aiBedroomCamera = FindInActiveObjectByTag("AICam");
        mpBedroomCamera = FindInActiveObjectByTag("MPCam");

        // allows the cameras to be placed one the player has been caught 5 times 
        if (kitchenCameraData == 5 || studyCameraData == 5 || livingroomCameraData == 5 || bathroomCameraData == 5 || aiBedroomCameraData == 5 || mpBedroomCameraData == 5)
            aiMovement.placeCam = true;

        if (kitchenCameraData == 5)
            aiMovement.placeKitchenCam = true;

        if (studyCameraData == 5)
            aiMovement.placeStudyCam = true;

        if (livingroomCameraData == 5)
            aiMovement.placeLivingroomCam = true;

        if (bathroomCameraData == 5)
            aiMovement.placeBathroomCam = true;

        if (aiBedroomCameraData == 5)
            aiMovement.placeAICam = true;

        if (mpBedroomCameraData == 5)
            aiMovement.placeMPCam = true;
    }

    // checks if the player is colliding with the AI
    private void OnTriggerEnter2D(Collider2D other)
    {
        // checks if the player is hiding
        if (other.gameObject.tag == "Player")
        {
            Invoke("OnCaught", 0); // if the player isn't hiding, the AI catches them
        }

        if (other.gameObject.name == "Alarm")
            gameManager.TurnOffAlarm();
       
        // checks if the player has been caught in front of the doors while being followed
        if (other.gameObject.name == "Kitchen Col" && aiMovement.isFollowing)
            kitchenCameraData++;

        if (other.gameObject.name == "Study Col" && aiMovement.isFollowing)
            studyCameraData++;

        if (other.gameObject.name == "Living Room Col" && aiMovement.isFollowing)
            livingroomCameraData++;

        if (other.gameObject.name == "Bathroom Col" && aiMovement.isFollowing)
            bathroomCameraData++;

        if (other.gameObject.name == "AI Bedroom Col" && aiMovement.isFollowing)
            aiBedroomCameraData++;

        if (other.gameObject.name == "MP Bedroom Col" && aiMovement.isFollowing)
            mpBedroomCameraData++;

        // lets the AI place the camera
        if (other.gameObject.name == "Kitchen Col" && aiMovement.placeKitchenCam)
        {
            kitchenCamera.gameObject.SetActive(true);
            kitchenCameraData = 0;
            aiMovement.placeKitchenCam = false;
            aiMovement.placeCam = false;
        }

        if (other.gameObject.name == "Study Col" && aiMovement.placeStudyCam)
        {
            studyCamera.gameObject.SetActive(true);
            studyCameraData = 0;
            aiMovement.placeStudyCam = false;
            aiMovement.placeCam = false;
        }

        if (other.gameObject.name == "Living Room Col" && aiMovement.placeLivingroomCam)
        {
            livingroomCamera.gameObject.SetActive(true);
            livingroomCameraData = 0;
            aiMovement.placeLivingroomCam = false;
            aiMovement.placeCam = false;
        }

        if (other.gameObject.name == "Bathroom Col" && aiMovement.placeBathroomCam)
        {
            bathroomCamera.gameObject.SetActive(true);
            bathroomCameraData = 0;
            aiMovement.placeBathroomCam = false;
            aiMovement.placeCam = false;
        }

        if (other.gameObject.name == "AI Bedroom Col" && aiMovement.placeAICam)
        {
            aiBedroomCamera.gameObject.SetActive(true);
            aiBedroomCameraData = 0;
            aiMovement.placeAICam = false;
            aiMovement.placeCam = false;
        }

        if (other.gameObject.name == "MP Bedroom Col" && aiMovement.placeMPCam)
        {
            mpBedroomCamera.gameObject.SetActive(true);
            mpBedroomCameraData = 0;
            aiMovement.placeMPCam = false;
            aiMovement.placeCam = false;
        }
    }

    void OnCaught()
    {
        // checks what scene the player is currently in
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        aiMovement.detected = false;
        aiMovement.isSearching = false;
        
        // sends the player to the future if they are caught
        if (sceneName == "Hallway" || sceneName == "Kitchen" || sceneName == "Livingroom" || sceneName == "Bathroom" || sceneName == "Study" || sceneName == "AIBedroom" || sceneName == "MPBedroom")
        {
            gameManager.publicSceneLocation = "Hallway";
            SceneManager.LoadScene("FutHallway");
        }

        // unhides the player when they are sent back to the future 
        if (player.isHiding)
        {
            player.isHiding = false;
            player.GetComponent<BoxCollider2D>().enabled = true;
            player.spriteRenderer.sortingOrder = 11;
        }

    }

    // finds inactive game objects
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
