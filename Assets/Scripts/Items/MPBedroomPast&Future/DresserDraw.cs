using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DresserDraw : MonoBehaviour
{
    [Header("Sprites")]
    public SpriteRenderer spriteRenderer;
    public Sprite openDraw;

    [Header("Canvas")]
    public GameObject evidenceBag;
    public bool watchFixed;
    

    // Start is called before the first frame update
    void Start()
    {
    }

    // shows the evidence bag when the draw is clicked on
    void OnMouseDown()
    {
        evidenceBag.SetActive(true);
        spriteRenderer.sprite = openDraw;
    }

    // shows the canvas
    public void Interact()
    {
        evidenceBag.SetActive(true);
    }

    // disables the canvas
    public void StopInteract()
    {
        evidenceBag.SetActive(false);
    }
}
