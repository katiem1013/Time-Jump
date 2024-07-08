using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WireStretch : MonoBehaviour
{

    public float distance;
    public Vector3 direction;
    public GameObject startPosition, newPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // changes the direction of the wire
        direction = newPosition.transform.position - startPosition.transform.position;
        transform.right = direction;

        // scales the wire
        distance = Vector3.Distance(startPosition.transform.position, newPosition.transform.position);
        distance = Mathf.Abs(distance);
        gameObject.transform.localScale = new Vector2(distance/12, gameObject.transform.localScale.y);
    }
}
