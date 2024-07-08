using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepSounds : MonoBehaviour
{

    public AudioSource audioSource;

    // Update is called once per frame
    void Update()
    {
        // plays sound when the player is moving
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            // changes the volume of the footsteps based on if the player is sprinting, crouching or walking
            audioSource.enabled = true;
            if (Input.GetKey(KeyCode.LeftShift))
                audioSource.volume = 0.75f;

            else if (Input.GetKey(KeyCode.LeftControl))
                audioSource.volume = 0.25f;

            else
                audioSource.volume = 0.5f;
        }
           

        else
            audioSource.enabled = false;
    }
}
