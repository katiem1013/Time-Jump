using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour
{
    static public bool once_call;

    // Start is called before the first frame update
    void Awake()
    {
        // checks if the item is alrady in a scene
        if (!once_call)
        {
            DontDestroyOnLoad(gameObject); // keeps the item between scene changes 
            once_call = true;
        }

        // destroy it if it is
        else
        {
           Destroy(gameObject);
        }
    }


}
