using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FallingObjects : MonoBehaviour
{

    [SerializeField]
    public Player playerScr;

    public int Hitdamage = 10;
    Rigidbody2D rb;
    
    Animator animator;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)     // Kommt in den Bereich des Colliders; Dieser muss auf trigger sein!
    {
        if (collision.tag == "Player")
        {
            rb.isKinematic = false;
          
            

        }

      

    }


    public void SpikeHitSet()
    {
        animator.Play("fallingSpike_destroy");
    }

    private void OnCollisionEnter2D(Collision2D collision)  // Dieser Checkt nun die Kollision mit dem Jeweiligen GameObject!
    {   
        
        if (collision.gameObject.CompareTag("Ground")|| collision.gameObject.CompareTag("Platform"))
        {
            SpikeHitSet();
            Destroy(gameObject,0.3f);
        }
        else if (collision.gameObject.CompareTag("Player")) 
        {
            playerScr.DamageState.PlayerTakeDamage(Hitdamage);
            playerScr.SetTakenHit(true);
            SpikeHitSet();
            Destroy(gameObject, 0.3f);
        }

      


    }
  
    void Update()
    {


        
    }
}
