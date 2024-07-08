using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide : MonoBehaviour
{
    public Movement player;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // checks if the player is overlapping an object they can hide with
        if (collision.gameObject.CompareTag("Hide"))
            player.canHide = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // checks if the player has stopped overlapping an object they can hide with
        if (collision.gameObject.CompareTag("Hide"))
            player.canHide = false;
    }
}
