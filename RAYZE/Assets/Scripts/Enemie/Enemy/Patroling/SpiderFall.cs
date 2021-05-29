using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderFall : MonoBehaviour
{

    // Script Fehlerhaft
    public float speed, fallSpeed;

    public float circleRadius;
    public float circleRadiusWall;
    public GameObject groundCheck;
    public LayerMask groundLayer; 
    public bool isFacingRight;
    public bool isGrounded;
    private Rigidbody2D EnemyRB;

    public GameObject groundCheck2;
    public LayerMask groundLayer2;
    public bool isGrounded2;
    public float circleRadius2;

    private bool falling;



    // wall

    public GameObject WallCheck;
    public bool isWall;
    public LayerMask WallLayer;

    // Start is called before the first frame update
    void Start()
    {
        EnemyRB = GetComponent<Rigidbody2D>();
       
    }

    // Update is called once per frame
    void Update()
    {

    }


    void Flip()
    {
        speed *= -1f;
       
        transform.Rotate(new Vector3(0, 180, 0));
        
    }

   

    void FixedUpdate()
    {
        dynamicRun();
        kinematicRun();
        groundWallCheck();
    }

    private void dynamicRun()
    {
        if(EnemyRB.isKinematic == false && !falling) 
        {
            speed = 150f;
            EnemyRB.velocity = Vector2.right * speed * Time.deltaTime;

            if (isWall )
            {
                Flip();
            }
            
            if (!isGrounded)
            {
                Flip();
            }
           
            
        }
        dashDown();
    }

    private void kinematicRun() 
    {
        if (EnemyRB.isKinematic == true)
        {

            EnemyRB.velocity = Vector2.right * speed * Time.deltaTime;


            if (isWall || !isGrounded)
            {
                Flip();
            }


           

        }
        
    }

    private void dashDown() 
    {   

        if (EnemyRB.isKinematic == false && !isGrounded && !isWall)
        {

           // transform.eulerAngles = new Vector3(0, 0, 180);  // x,y,z Achse werden manipuliert.
            transform.localRotation = Quaternion.Euler(0, 0, 180);
            transform.position += Vector3.down * fallSpeed * Time.deltaTime;
            Flip();
            speed *= -1f;
            falling = true;
           

        }
        else 
        {
            falling = false;
        }
    }

    

    public void groundWallCheck() 
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.transform.position, circleRadius, groundLayer);
        isGrounded2 = Physics2D.OverlapCircle(groundCheck2.transform.position, circleRadius2, groundLayer2);
        isWall = Physics2D.OverlapCircle(WallCheck.transform.position, circleRadiusWall, WallLayer);
    }

   
    private void OnTriggerEnter2D(Collider2D collision)     // Kommt in den Bereich des Colliders; Dieser muss auf trigger sein!
    {
        if (collision.tag == "Player")
        {
            speed = 0f;
            EnemyRB.isKinematic = false;

            EnemyRB.velocity = Vector2.right * speed * Time.deltaTime;





        }
    }
    private void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.transform.position, circleRadius);
        Gizmos.DrawWireSphere(groundCheck2.transform.position, circleRadius2);
        Gizmos.DrawWireSphere(WallCheck.transform.position, circleRadiusWall);

    }


}
