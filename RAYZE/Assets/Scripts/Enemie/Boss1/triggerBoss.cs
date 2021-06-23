using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// By Mehmet Budak
public class triggerBoss : MonoBehaviour
{
    [SerializeField] Transform Boss1;
    [SerializeField] Boss1_SrctySystem Boss;

    Rigidbody2D rb;
    public bool isGrounded = false;
    public GameObject groundCheck;
    public LayerMask groundLayer;
    public float circleRadius;

    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)     // Kommt in den Bereich des Colliders; Dieser muss auf trigger sein!
    {
        if (collision.tag == "Player" )
        {
           
            Debug.Log("WTF TRIGGER");
            Boss.Istrigger();
            
        }
      
        if(Boss != null)
        {
            Destroy(gameObject);
        }

        if (collision.tag == "Enemy")
        {
           rb.isKinematic = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)  // Dieser Checkt nun die Kollision mit dem Jeweiligen GameObject!
    {
        if (collision.gameObject.CompareTag ("Player"))
        {
            DamagePlayer();
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            DamagePlayer();
            
        }


    }

    void CollisionCheckSpike()
    {
        if(isGrounded == true)
        {
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.transform.position, circleRadius);
    }

    public void DamagePlayer()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CollisionCheckSpike();
        isGrounded = Physics2D.OverlapCircle(groundCheck.transform.position, circleRadius, groundLayer);
    }
}
