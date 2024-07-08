using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Erasing : MonoBehaviour
{

    public GameObject pencilEraser;
    public bool pencil = false;
    static public bool savePencil = false;

    public GameObject canvas;
    public GameObject eraser;
    public Image eraserImage;

    public Vector2 eraserPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        savePencil = pencil;
            
        // if the mouse button is pressed spawn the eraser
        if (Input.GetMouseButton(0))
        {
            if (savePencil == true)
            {
                pencilEraser.SetActive(true);
            }

        }
    }
}
