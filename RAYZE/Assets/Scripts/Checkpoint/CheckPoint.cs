using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public Player player;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            anim.Play("Pflanze_idle");
            // neuen Checkpoint abspeichern
            player.currentCheckpoint = gameObject;
        }
    }
}
