using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Unity.VisualScripting.Member;

public class Movement : MonoBehaviour
{
    [Header("Movement")]
    private float horizontal;
    private float speed = 4.0f;
    private bool isFacingRight = true;

    [Header("Animations")]
    public Animator animator;
    public Animator sceneTransition;

    [Header("Hiding")]
    public bool canHide;
    public bool isHiding;
    public bool isMoving;
    public SpriteRenderer spriteRenderer;
    public AIMovement ai;

    [Header("Canvas")]
    public GameObject fileMenu;

    [Header("Audio")]
    AudioSource audioSource;
    public AudioClip[] clickSounds;
    public float soundVolume;


    [SerializeField] private Rigidbody2D rb;
    static public bool once_call;

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

    void Start()
    {
        // gets the variables needed in the scene
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            PlaySound();

        // checks what scene the player is currently in
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        GameManager.playerSceneLocation = sceneName;

        sceneTransition = GameObject.FindGameObjectWithTag("SceneTransition").GetComponent<Animator>();

        horizontal = Input.GetAxisRaw("Horizontal"); // gets the players left and right input keys 
        animator.SetFloat("Speed", Mathf.Abs(horizontal)); // sets the animation speed to the player current speed so that the animations can change

        // stops the player from teleporting if they are using the computer and if they are hiding
        if (Input.GetKeyDown(KeyCode.E) && !Computer.playerUsingComputer && !isHiding && !Phone.phoneIsOn && !MicrowaveNew.mircowaveOn && !Oven.saucepanOnBoiling && !ai.detected)
            StartCoroutine(LoadRoom());

        if (Input.GetKeyDown(KeyCode.Q) && !Computer.playerUsingComputer && !isHiding && !Phone.phoneIsOn)
            fileMenu.SetActive(true);

        // hides the player behind the object by changing the layer order
        if (canHide && Input.GetKeyDown(KeyCode.W))
        {
            spriteRenderer.sortingOrder = 4;
            isHiding = true; // this allows for checks elsewhere if the player is hiding

            if (!ai.detected && !ai.isSearching)
               gameObject.GetComponent<BoxCollider2D>().enabled = false; // stops the AI from seeing the player
        }

        // unhides the player by putting them back to their original layer
        if (isHiding && Input.GetKeyDown(KeyCode.S))
        {
            spriteRenderer.sortingOrder = 11;
            isHiding = false; // this allows for checks elsewhere if the player isn't hiding
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }

        // runs the code that allows the player to change directions 
        Flip();
    }

    IEnumerator LoadRoom()
    {
        // checks what scene the player is currently in
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        sceneTransition.SetTrigger("Start");
        yield return new WaitForSeconds(1);

        // if the player is in the past, allows them to time travel to the future hallway
        if (sceneName == "Hallway" || sceneName == "Kitchen" || sceneName == "Livingroom" || sceneName == "Bathroom" || sceneName == "Study" || sceneName == "AIBedroom" || sceneName == "MPBedroom")
             SceneManager.LoadScene("FutHallway");

        // if the player is in the future, time-travels them to the past hallway
        else if (sceneName == "FutHallway" || sceneName == "FutKitchen" || sceneName == "FutLivingroom" || sceneName == "FutBathroom" || sceneName == "FutStudy" || sceneName == "FutAIBedroom" || sceneName == "FutMPBedroom")
            SceneManager.LoadScene("Hallway");

        sceneTransition.SetTrigger("End");
    }

    private void FixedUpdate()
    {
        // checks if the player is using the computer or is hiding
        if (!Computer.playerUsingComputer && !isHiding && !Phone.phoneIsOn)
        {
            // gets the player moving
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

            // checks which way the player is moving
            if (rb.velocity.x < 0)
                isFacingRight = false;
            if (rb.velocity.x > 0)
                isFacingRight = true;

            // makes the player run
            if (Input.GetKey(KeyCode.LeftShift))
            {
                animator.SetBool("IsRunning", true);
                speed = 8f;
            }

            // slows the players speed and makes the character crounch
            else if (Input.GetKey(KeyCode.LeftControl))
            {
                GetComponent<BoxCollider2D>().size = new Vector2(3f, 4f);
                animator.SetBool("IsCrouching", true);
                speed = 2.4f;
            }

            // sets the player back to walking when nothing else is being done 
            else
            {
                speed = 4.0f;
                GetComponent<BoxCollider2D>().size = new Vector2(3f, 6.3f);
                animator.SetBool("IsCrouching", false);
                animator.SetBool("IsRunning", false);
            }
        }

        else
            rb.velocity = new Vector2(0, 0);

    }

    private void Flip()
    {
        // flips the way the player is facing depending on the direction they are moving
        if (transform.localEulerAngles.y != 180 && !isFacingRight)
            transform.Rotate(0f, 180f, 0f);
        else if (transform.localEulerAngles.y != 0 && isFacingRight)
            transform.Rotate(0f, -180f, 0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // checks if the player is overlapping an object they can hide with
        if(collision.gameObject.CompareTag("Hide"))
            canHide = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // checks if the player has stopped overlapping an object they can hide with
        if (collision.gameObject.CompareTag("Hide"))
            canHide = false;
    }

    void PlaySound()
    {
        // chooses a random sound from the list
        int soundChance = Random.Range(1, clickSounds.Length);
        audioSource.clip = clickSounds[soundChance];
        audioSource.PlayOneShot(audioSource.clip, soundVolume); // plays the sound once

        // moves the played sound to index 0 to stop it being repeated
        clickSounds[soundChance] = clickSounds[0];
        clickSounds[0] = audioSource.clip;
    }
}