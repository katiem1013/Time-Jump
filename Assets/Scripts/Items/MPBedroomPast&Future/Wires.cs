using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wires : MonoBehaviour
{
    [Header("Starting Sizes")]
    public Vector2 startSize;
    public Vector3 startPosition, startRotation;

    [Header("Images")]
    public Image wireStart;
    public GameObject wireEnd;
    public WireStretch wireStretch;


    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        startSize = wireStart.rectTransform.sizeDelta;
        startRotation = transform.right;
    }

    public void DraggingWires()
    {
        // mouse position to world point
        Vector3 newPosition = Input.mousePosition;
        newPosition.z = 0;

        transform.position = newPosition; // updates position

        // changes the direction of the wire
        Vector3 direction = newPosition - startPosition;
        transform.right = direction;
        

        // checks for endPoint
        Collider2D[] colliders = Physics2D.OverlapBoxAll(newPosition, wireStart.rectTransform.sizeDelta, 0.2f);
        
        foreach (Collider2D collider in colliders)
        {
            // not the current game object
            if (collider.gameObject != gameObject)
            {
                transform.position = collider.transform.position;

                // checks wire colours are the same
                if (transform.name.Equals(collider.transform.name))
                {
                    OpenBag.watchFixed++;
                    Destroy(this);
                }

                else
                {
                    // returns them to starting position
                    gameObject.transform.position = startPosition;
                    wireStart.rectTransform.sizeDelta = startSize;
                    transform.right = startRotation;
                    wireStretch.distance = 0;
                    wireStretch.direction = new Vector3(0, 0, 0);
                }

                return;
            }
        }
    }

    // returns the wire to starting position
    public void DropWires()
    {
        gameObject.transform.position = startPosition;
        wireStart.rectTransform.sizeDelta = startSize;
        transform.right = startRotation;
        wireStretch.distance = 0;
        wireStretch.direction = new Vector3(0,0,0);
    }

    // stops the wires from being moved
    void CorrectWire()
    {
        Destroy(this);
    }
}
