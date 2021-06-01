using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    //aktuelle Checkpoit
    public GameObject currentCheckpoint;
    // player gameobjekt
    public GameObject player;
    
    public void RespawnPlayer()
    {
        //spieler an die position des checkpoints bringen
        player.transform.position = currentCheckpoint.transform.position;
    }
}
