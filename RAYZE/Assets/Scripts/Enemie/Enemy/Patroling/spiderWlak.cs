using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spiderWlak : MonoBehaviour 
{   /*
     * 
     * IST FEHLERHAFT!
    private Rigidbody2D rb;

    //float Variablen
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float wallDist;
    [SerializeField]
    private float groundDist;
    private float rotateZ;


    // Booleans
    private bool groundCheck;
    private bool wallCheck;
    private bool hasFlipped;

    //Transfrom
    [SerializeField]
    private Transform groundPosCheck; // Raycast
    [SerializeField]
    private Transform wallPosCheck;   //Raycast

    //Layer
    public LayerMask whatIsGround;
    //public LayerMask whatIsWall;




    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        groundWallCheck();
    }


    void FixedUpdate()
    {
        movement();
    }

    void movement() 
    {
        rb.velocity = transform.right * moveSpeed;
    }

    void groundWallCheck() 
    {
        groundCheck = Physics2D.Raycast(groundPosCheck.position, - transform.up, groundDist, whatIsGround); // Durch das - geht der Raycast nach unten
        wallCheck = Physics2D.Raycast(wallPosCheck.position, transform.right, wallDist, whatIsGround);

        if (!groundCheck) 
        {
            if(hasFlipped == false) 
            {
                rotateZ -= 90;
                transform.eulerAngles = new Vector3(0, 0, rotateZ);  // x,y,z Achse werden manipuliert.
                transform.position = new Vector2(transform.position.x + 0.2f, transform.position.y + 0.2f); // Insekt wird beim routieren in die Richtung geschoben, damit es authentischer Aussieht
                hasFlipped = true;
            }
        }

        if (groundCheck) 
        {
            hasFlipped = false;
            
        }
      
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(groundPosCheck.position, new Vector2(groundPosCheck.position.x, groundPosCheck.position.y - groundDist));
        Gizmos.DrawLine(wallPosCheck.position, new Vector2(wallPosCheck.position.x + wallDist, wallPosCheck.position.y));
    }
    */ 
}
