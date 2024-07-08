using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BathroomMirror : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite newSprite;

    public Taps taps;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if the taps are running fog up the mirror
        if (taps.tapsRunningSaved == true)
            Invoke("FogUpMirror", 2);
    }

    // changes the sprite
    void FogUpMirror()
    {
        spriteRenderer.sprite = newSprite;
    }
}
