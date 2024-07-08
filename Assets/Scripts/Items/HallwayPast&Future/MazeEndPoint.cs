using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeEndPoint : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // sets the maze to completed whne the player reaches the end
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("End");
        Computer.mazeCompleted = true; 
    }
}
