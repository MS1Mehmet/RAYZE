using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyFollowPlayer : MonoBehaviour
{

    public float speed, jump, jumpVertical;
    private Transform player;
    public float lineOfSite;
    public GameObject wallCheck;
    public GameObject groundCheck;
   
    public float circleRadius; 
    public float circleRadius2;
    public LayerMask groundLayer;
    public LayerMask wallLayer;
    Rigidbody2D enemyRb;
    Animator EnemyAnim;
    


    //Boolean
    private bool facingLeft = false;
    public bool isWall;
    public bool isGrounded;

    //####### Der player wird hier als objekt intialisiert, aber über denn Tag Player, somit kann er anvisiert werden! ###########
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemyRb = GetComponent<Rigidbody2D>();
        EnemyAnim = GetComponent<Animator>();
    }

    private void Update()
    {
        isWall = Physics2D.OverlapCircle(wallCheck.transform.position, circleRadius, wallLayer);
        isGrounded = Physics2D.OverlapCircle(groundCheck.transform.position, circleRadius2, groundLayer);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        runTowardsPlayer();
        jumpOver();
    }

    //##### Wenn der Player in die Reichweite des Radiuses kommt, dann wird er sich in seine Richtung bewegen#######
    private void runTowardsPlayer() 
    {
        if (isGrounded) 
            {
                 float distanceFromplayer = Vector2.Distance(player.position, transform.position);



                  if (distanceFromplayer < lineOfSite)
                   {
                     transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
                     FlipTowardsPlayer();

                   }
             }
    }


    void FlipTowardsPlayer()
    {
        float playerDirection = player.position.x - transform.position.x;

        if (playerDirection > 0 && facingLeft)
        {
            Flip();
        }
        else if (playerDirection < 0 && !facingLeft)
        {
            Flip();
        }
    }

    void Flip()
    {
        facingLeft = !facingLeft;
        transform.Rotate(0, 180, 0);
    }

    public void jumpOver() 
    {
        if (isWall == true && !facingLeft) 
        {
            enemyRb.AddForce(Vector2.up * jump + Vector2.left * -jumpVertical);
            EnemyAnim.Play("Jumper-Jump");

        }
        else if(isWall == true && facingLeft) 
        {
            enemyRb.AddForce(Vector2.up * jump + Vector2.left * jumpVertical);
            EnemyAnim.Play("Jumper-Jump");
            //enemyRb.velocity(Vector2.up * jump + Vector2.left * 20f);
        }
        
    }

    /*
    void OnTriggerEnter2D(Collider2D col)
    {

        switch (col.tag)
        {

            case "Wall":
                jumpOver();
                break;
            
                
        }
    }
    */

    //### Hier wird der Radius dargestellt und sobald sich der player() in diesem Radius befindet wird er angegriffen###!!
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position,lineOfSite);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(wallCheck.transform.position, circleRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.transform.position, circleRadius2);
    }
}
