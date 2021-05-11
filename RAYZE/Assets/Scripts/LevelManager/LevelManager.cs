using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // aktuelle Checkpoint
    public GameObject currentCheckpoint;
    // spieler gameonjekt
    public GameObject player;

   public void RespawnPlayer()
    {
        // spieler an die position des checkpoints bringen
        player.transform.position = currentCheckpoint.transform.position;
    }
}
