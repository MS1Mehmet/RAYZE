using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderFallFix : MonoBehaviour
{
    [SerializeField]
    Transform wallCheck;

    [SerializeField]
    float moveSpeed;
    [SerializeField]
    float wallCircle;
    [SerializeField]
    LayerMask groundLayer;

    [SerializeField]
    Transform groundCheck;

    [SerializeField]
    float fallSpeed;

    [SerializeField]
    float groundCircle;

    [SerializeField]
    LayerMask wallLayer;

    Rigidbody2D rb;

    public bool isGrounded;
    public bool isWall;
    public bool isFacingRight;
    

  


    // Start is called before the first frame update
    void Start()
    {
       rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.transform.position, groundCircle, groundLayer);
        isWall = Physics2D.OverlapCircle(wallCheck.transform.position,wallCircle,wallLayer);


    }

    private void FixedUpdate()
    {
        kinematicWalk();
        dynamicBody();
        movementSpeed();
        dashDown();
    }

    private void movementSpeed() 
    {
        rb.velocity = Vector2.right * moveSpeed * Time.deltaTime;
    }
    private void dynamicBody() 
    {
    
        if(rb.isKinematic == false) 
        {

            LayerMask groundLayer = LayerMask.GetMask("Ground");

            if (isWall || !isGrounded && isFacingRight)
            {
                Flip();
            }
            else if (isWall || !isGrounded && !isFacingRight)
            {
                Flip();
            }

        }

    }

    private void kinematicWalk() 
    {
        
        if (rb.isKinematic == true)
        {

           

             if (isWall  || !isGrounded && isFacingRight)
              {
                Flip();
              }
             else if(isWall || !isGrounded && !isFacingRight) 
             {
                Flip();
             }


            

        }

        
    }

    private void Flip() 
    {
        
        isFacingRight = !isFacingRight;
        transform.Rotate(new Vector3(0, 180, 0));
        moveSpeed *= -1;
    }


    private void dashDown()
    {

        if (rb.isKinematic == false && !isGrounded && !isWall)
        {

          // transform.eulerAngles = new Vector3(0, 0, 180);  // x,y,z Achse werden manipuliert.
           // transform.localRotation = Quaternion.Euler(0, 0, 180);
            transform.position += Vector3.down * fallSpeed * Time.deltaTime;
           


        }
      
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (isFacingRight) 
            {
                Flip();
            }

            rb.isKinematic = false;
            transform.eulerAngles = new Vector3(0, 0, 180);
            //EnemyRB.velocity = Vector2.right * speed * Time.deltaTime;





        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.transform.position,groundCircle);
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(wallCheck.transform.position, wallCircle);
    }
}
