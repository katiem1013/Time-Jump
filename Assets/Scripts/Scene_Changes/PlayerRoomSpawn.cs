using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRoomSpawn : MonoBehaviour
{
     public GameObject spawnPoint;
    public GameObject playerPos;

    // Start is called before the first frame update
    void Start()
    {
        // sets the player to the spawn point
        playerPos = GameObject.FindGameObjectWithTag("Player");
        playerPos.transform.position = spawnPoint.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
