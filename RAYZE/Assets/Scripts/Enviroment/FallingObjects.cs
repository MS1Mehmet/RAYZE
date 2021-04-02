using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// By Mehmet Budak
public class FallingObjects : MonoBehaviour
{


    Rigidbody2D rb;
    public int contactDamage;
    


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)     // Kommt in den Bereich des Colliders; Dieser muss auf trigger sein!
    {
        if (collision.tag == "Player")
        {
            rb.isKinematic = false;
            //Debug.Log("WTF TRIGGER");
            

        }

      

       // if (collision.tag == "Enemy")
        //{
         //   rb.isKinematic = false;
       // }
    }

    private void OnCollisionEnter2D(Collision2D collision)  // Dieser Checkt nun die Kollision mit dem Jeweiligen GameObject!
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();

            player.HitSide(transform.position.x > player.transform.position.x);     // Auf welcher Seiter der Player getrpffen wird
            player.TakeDamage(this.contactDamage);
            Destroy(gameObject, 1f);
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }

        


    }
    // Update is called once per frame
    void Update()
    {
        
        

    }
}
