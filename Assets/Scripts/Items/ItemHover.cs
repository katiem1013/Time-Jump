using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHover : MonoBehaviour
{
    public Renderer objRenderer;
    private Color startcolor;

    // checks when the mouse is hovering 
    void OnMouseEnter()
    {
        // changes the colour to green
        startcolor = objRenderer.material.color;
        objRenderer.material.color = Color.green;
    }
    void OnMouseExit()
    {
        // resets the colour
        objRenderer.material.color = startcolor;
    }

    // Start is called before the first frame update
    void Start()
    {
        startcolor = objRenderer.material.color;
        objRenderer = gameObject.GetComponent<Renderer>();
    }
}
