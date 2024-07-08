using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Camera Position")]
    public Transform player;
    private Camera mainCamera;
    public Vector2 margin = new Vector2(1, 1); // If the player stays inside this margin, the camera won't move.
    public Vector2 smoothing = new Vector2(3, 3); // The bigger the value, the faster the camera.
    public BoxCollider2D cameraBounds;
    private Vector3 min, max;
 
    public bool isFollowing;
    static public bool once_call;

    // Start is called before the first frame update
    void Awake()
    {
        // gets the starting variables
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
    }
 
    void Start()
    {
        min = cameraBounds.bounds.min;
        max = cameraBounds.bounds.max;
        isFollowing = true;
        mainCamera = GetComponent<Camera>();
 
    }
 
    void Update()
    {
        var x = transform.position.x;
        var y = transform.position.y;

        // checks if the camer is following the player and gives a little room before it follows 
        if (isFollowing)
        {
            if (Mathf.Abs(x - player.position.x) > margin.x)
                x = Mathf.Lerp(x, player.position.x, smoothing.x * Time.deltaTime);
 
        }
 
        // this is to find half the height of the camera
        var cameraHalfWidth = mainCamera.orthographicSize * ((float)Screen.width / Screen.height);
 
        x = Mathf.Clamp(x, min.x + cameraHalfWidth, max.x - cameraHalfWidth);
        y = Mathf.Clamp(y, min.y + mainCamera.orthographicSize, max.y - mainCamera.orthographicSize); // despite the y axis not changing, this is to keep it where it is rather than centre the player. 
 
        // moves the caemra
        transform.position = new Vector3(x, y, transform.position.z);
    }
 
    public void UpdateBounds()
    { 
        // updates the camera bounds
        min = cameraBounds.bounds.min;
        max = cameraBounds.bounds.max;
    }
}