using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
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
}
