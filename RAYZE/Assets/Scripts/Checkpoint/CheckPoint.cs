using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public Player player;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            // neuen Checkpoint abspeichern
            player.currentCheckpoint = gameObject;
        }
    }
}
