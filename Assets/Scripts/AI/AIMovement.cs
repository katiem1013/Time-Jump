using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AIMovement : MonoBehaviour
{
    [Header("AI Movement")]
    public bool isFollowing = false;
    public bool hearingRange = false; 
    public bool isSearching;
    bool isFacingLeft;
    public bool detected = false;
    [SerializeField] public float moveSpeed = 1f;
    public bool inFrontOfDoor;

    [Header("Waypoints")]
    [SerializeField] public List<Transform> waypoints;
    [SerializeField] public Transform castPoint;
    public int waypointIndex = 0;

    [Header("Transforms and Rigidbodies")]
    public Movement player;
    public Transform playerTransform;
    public Transform alarmTransform, kitchenTransform, kitchenAlarmTransform;
    Rigidbody2D rb2d;
    public GameObject spawnPoint;

    public Vector3 temphold;

    [Header("Animations and Audios")]
    public AudioSource audioSource;
    public Animator animator;
    public AudioSource alertSound;
    public AudioClip audioClip;
    public bool soundPlayed;

    [Header("Cameras")]
    public GameManager gameManager;
    public GameObject kitchenDoor, studyDoor, livingroomDoor, bathroomDoor, aiDoor, mpDoor;

    [Header("Camera Placement")]
    public bool placeCam;
    public bool placeKitchenCam, placeStudyCam, placeLivingroomCam, placeBathroomCam, placeAICam, placeMPCam;

    [SerializeField] private Rigidbody2D rb;
    static public bool once_call;

    [Header("Hearing Range")]
    public CircleCollider2D hearingRangeCollider;
    public float startingRangeSize;

    private void OnEnable()
    {
        spawnPoint = FindInActiveObjectByTag("SpawnPoint");
    }

    private void Awake()
    {
        // makes sure there is only one version of the player in each scene
        if (!once_call)
        {
            // creates a player if there isn't one
            DontDestroyOnLoad(gameObject);
            once_call = true;
        }

        // destroys the player if there is already a player in scene
        else
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {   
        // gets all the starting variables
        rb2d = GetComponent<Rigidbody2D>();
        transform.position = waypoints[waypointIndex].transform.position;    
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        alarmTransform = GameObject.FindGameObjectWithTag("Alarm").transform;

        hearingRangeCollider = gameObject.GetComponent<CircleCollider2D>();
        startingRangeSize = hearingRangeCollider.radius;
    }

    // Update is called once per frame
    void Update()
    {
        // finds all the cameras
        kitchenDoor = FindInActiveObjectByTag("KitCam");
        studyDoor = FindInActiveObjectByTag("StudCam");
        livingroomDoor = FindInActiveObjectByTag("LRCam");
        bathroomDoor = FindInActiveObjectByTag("BRCam");
        aiDoor = FindInActiveObjectByTag("AICam");
        mpDoor = FindInActiveObjectByTag("MPCam");

        // changes the hearing range based on the 
        if (Input.GetKey(KeyCode.LeftControl))
            hearingRangeCollider.radius = startingRangeSize - 3;

        else if (Input.GetKey(KeyCode.LeftShift))
            hearingRangeCollider.radius = startingRangeSize + 3;

        else
            hearingRangeCollider.radius = startingRangeSize;

        // checks what scene the player is currently in
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        // finds the spawn point inside the rooms
        if (sceneName != "Hallway" || sceneName != "FutHallway" || sceneName != "FutKitchen" || sceneName != "FutLivingroom" || sceneName != "FutBathroom" || sceneName != "FutStudy" || sceneName != "FutAIBedroom" || sceneName != "FutMPBedroom")
            spawnPoint = FindInActiveObjectByTag("SpawnPoint");

        else
            spawnPoint = null;

        // if the scene is in the future sets everything to false
        if (sceneName == "FutHallway" || sceneName == "FutKitchen" || sceneName == "FutLivingroom" || sceneName == "FutBathroom" || sceneName == "FutStudy" || sceneName == "FutAIBedroom" || sceneName == "FutMPBedroom")
        {
            detected = false;
            isSearching = false;
            gameObject.SetActive(false);
        }

        animator.SetFloat("Speed", Mathf.Abs(moveSpeed)); // plays the animations 
        Flip(); // flips the AI to face the other way

        // AI starts following the player once they have been seen
        if (playerDetected(15))
            isFollowing = true;

        // plays a sound if the player is a detected
        if (playerDetected(15) && !soundPlayed)
        {
            alertSound.PlayOneShot(audioClip);
            soundPlayed = true;
            moveSpeed = 3f;
        }

        // checks if the player is scene
        if (gameManager.playerSeen)
            isFollowing = true;

        // AI starts following the player if they can hear them
        if (!audioSource.isPlaying && hearingRange == true)
        {
            isFollowing = true;
        }

        // if the player is being followed the AI will keep searching for the player
        else
        {
            if(isFollowing)
            {
                if(!isSearching && sceneName == "Hallway")
                {
                    isSearching = true;
                    StartCoroutine(StopChasingPlayer()); // starts the function to stop chasing the player once the player is out of sight for 5 secondsd
                }
            }
        }

        // doesn't play a sound if the player isn't detected
        if (!detected)
            soundPlayed = false;

        // keeps the AI moving
        Move();
    }

    // stops the AI from following the player
    public IEnumerator StopChasingPlayer()
    {
        yield return new WaitForSeconds(5);
        isFollowing = false;
        isSearching = false;
        rb2d.velocity = new Vector2(0, 0);
        moveSpeed = 1f;

        temphold = this.GetComponent<Rigidbody2D>().position; // holds the last position the AI saw the player
        AddWaypoints(); // adds the last know position of the player to the AIs patrol 
    }

    // tells the AI how to move
    public void Move()
    {
        // checks what scene the player is currently in
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (isFollowing)
        {
            // sets the AI facing in the direction it is walking while following the player
            if (transform.position.x < playerTransform.position.x)
            {
                rb2d.velocity = new Vector2(moveSpeed, 0);
                isFacingLeft = true;
            }

            // sets the AI facing in the direction it is walking while following the player
            else
            {
                rb2d.velocity = new Vector2(-moveSpeed, 0);
                isFacingLeft = false;
            }
        }

        // checks which camera should be placed
        else if (placeCam)
        {
            if (placeKitchenCam)
            {
                Vector2.MoveTowards(transform.position, kitchenDoor.transform.position, moveSpeed * Time.deltaTime);
                // sets the AI facing in the direction it is walking while following the player
                if (transform.position.x < kitchenDoor.transform.position.x)
                {
                    rb2d.velocity = new Vector2(moveSpeed, 0);
                    isFacingLeft = true;
                }

                // sets the AI facing in the direction it is walking while following the player
                else
                {
                    rb2d.velocity = new Vector2(-moveSpeed, 0);
                    isFacingLeft = false;
                }
            }

            if (placeStudyCam)
            {
                Vector2.MoveTowards(transform.position, studyDoor.transform.position, moveSpeed * Time.deltaTime);
                // sets the AI facing in the direction it is walking while following the player
                if (transform.position.x < studyDoor.transform.position.x)
                {
                    rb2d.velocity = new Vector2(moveSpeed, 0);
                    isFacingLeft = true;
                }

                // sets the AI facing in the direction it is walking while following the player
                else
                {
                    rb2d.velocity = new Vector2(-moveSpeed, 0);
                    isFacingLeft = false;
                }
            }

            if (placeLivingroomCam)
            {
                Vector2.MoveTowards(transform.position, livingroomDoor.transform.position, moveSpeed * Time.deltaTime);
                // sets the AI facing in the direction it is walking while following the player
                if (transform.position.x < livingroomDoor.transform.position.x)
                {
                    rb2d.velocity = new Vector2(moveSpeed, 0);
                    isFacingLeft = true;
                }

                // sets the AI facing in the direction it is walking while following the player
                else
                {
                    rb2d.velocity = new Vector2(-moveSpeed, 0);
                    isFacingLeft = false;
                }
            }

            if (placeBathroomCam)
            {
                Vector2.MoveTowards(transform.position, bathroomDoor.transform.position, moveSpeed * Time.deltaTime);
                // sets the AI facing in the direction it is walking while following the player
                if (transform.position.x < bathroomDoor.transform.position.x)
                {
                    rb2d.velocity = new Vector2(moveSpeed, 0);
                    isFacingLeft = true;
                }

                // sets the AI facing in the direction it is walking while following the player
                else
                {
                    rb2d.velocity = new Vector2(-moveSpeed, 0);
                    isFacingLeft = false;
                }
            }
        }

        // check if the alarm is on 
        else if (GameManager.alarmIsOn && !isSearching)
        {
            // moces towards the alarm
            Vector2.MoveTowards(transform.position, alarmTransform.position, moveSpeed * Time.deltaTime);

            // sets the AI facing in the direction it is walking while following the player
            if (transform.position.x < alarmTransform.position.x)
            {
                rb2d.velocity = new Vector2(moveSpeed, 0);
                isFacingLeft = true;
            }

            // sets the AI facing in the direction it is walking while following the player
            else
            {
                rb2d.velocity = new Vector2(-moveSpeed, 0);
                isFacingLeft = false;
            }
        }
        
        // when not following the player the AI will patrol by going through all the waypoints 
        else
        {
            if (sceneName == "Hallway")
            {
                if (waypointIndex <= waypoints.Count - 1)
                {
                    // moves the waypoint towards the next waypoint
                    transform.position = Vector2.MoveTowards(transform.position, waypoints[waypointIndex].transform.position, moveSpeed * Time.deltaTime);

                    // when the player gets to a waypoint it adds one to the index so that it can head to the next one 
                    if (transform.position == waypoints[waypointIndex].transform.position)
                    {
                        if (isFacingLeft)
                            moveSpeed = 0.01f;
                        if (!isFacingLeft)
                            moveSpeed = -0.01f;
                        Invoke("NextWaypoint", 3); // starts heading to the next waypoint
                    }
                }

                // sends the AI back to the beginning if it reaches the end
                if (waypointIndex == waypoints.Count)
                    waypointIndex = 0;

                // sets the AI facing in the direction it is walking patrolling
                if (transform.position.x < waypoints[waypointIndex].position.x)
                {
                    rb2d.velocity = new Vector2(moveSpeed, 0);
                    isFacingLeft = true;
                }

                // sets the AI facing in the direction it is walking patrolling
                else
                {
                    rb2d.velocity = new Vector2(-moveSpeed, 0);
                    isFacingLeft = false;
                }
            }
        }  

    }

    // checks if the player is in line of sight
    bool playerDetected(float distance)
    {
        var castDist = distance;

        // sets the direction of the line of sight to the direct the AI is walking
        if (isFacingLeft == true)
            castDist = -distance;
        // projects a line from the AI to act as line of sight
        Vector2 endPos = castPoint.position + Vector3.left * castDist;
        RaycastHit2D hit = Physics2D.Linecast(castPoint.position, endPos, 1 << LayerMask.NameToLayer("Action"));
        // checks if the line of sight is hitting anything 
        if (hit.collider != null)
        {
            // checks if it is hitting the player specifically
            if (hit.collider.gameObject.tag == "Player")
                detected = true; 

            else
                detected = false;

            // draws a visible line for debugging, turns yellow when hitting something
            Debug.DrawLine(castPoint.position, hit.point, Color.yellow);
        }

        else
        {
            detected = false; // stops the AI from following the player
            // draws a visible line for debugging, is blue when hitting nothing
            Debug.DrawLine(castPoint.position, endPos, Color.blue);
        }

        return detected;
    }

    // adds a waypoint to the list of places the AI patrols
    public void AddWaypoints()
    {
       waypoints.Add(new GameObject("Waypoint").transform);
       waypoints[waypoints.Count - 1].transform.position = gameObject.GetComponent<Rigidbody2D>().position;
    }   

    // checks if the player is in the hearing range
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
            hearingRange = true;
    }

    // checks if the player has left the hearing range 
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
            hearingRange = false;
    }

    // flips the AI so it is facing the direction it is walking 
    private void Flip()
    {
        if (transform.localEulerAngles.y != 180 && !isFacingLeft)
            transform.Rotate(0f, 180f, 0f);
        else if (transform.localEulerAngles.y != 0 && isFacingLeft)
            transform.Rotate(0f, -180f, 0f);
    }

    // makes the AI walk towards the next waypoint 
    public void NextWaypoint()
    {
        waypointIndex ++;
        moveSpeed = 2f;
        CancelInvoke();
    }

    // finds in active game objects 
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