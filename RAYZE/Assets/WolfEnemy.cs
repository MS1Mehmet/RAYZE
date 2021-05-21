using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfEnemy : MonoBehaviour
{   
    // Problem Raycast flip und attack behaviour fehlt noch !

  

    [SerializeField]
    Transform playerDetector;

    [SerializeField]
    float aggroRange;  // Eingabe der Distanz

    [SerializeField]
    float moveSpeed;


    [SerializeField]
    Transform wakeUpDetectorPlayer;  // Player ins Feld ziehen

    [SerializeField]
    float wakeUpRange;

    
    float distToWakeUp;  // Eingabe der Distanz



    //Idle detect
    [SerializeField]
    Transform distance1;

    public float distanceCheck;
    public LayerMask whatAttacking;
       

    private bool canAttack;
    public bool isFacingRight;

    Rigidbody2D enemyRb;
    float distToPlayer;


    private GameObject wolf; // für spätere Effekte
    private GameObject detector;

    Animator EnemyAnimation;                                                                        // wakeUpDetectorPlayer     wakeUpRange              

    // Start is called before the first frame update                                                // 1x transform,            float 1x distanz speichern,  
    void Start()
    {
        // wolf = transform.Find("Alien_Wolf").gameObject;
        // player = GameObject.FindGameObjectWithTag("Player").transform;
        enemyRb = GetComponent<Rigidbody2D>();
        EnemyAnimation = GetComponent<Animator>();
        detector = transform.Find("attackCheck").gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        distToPlayer = Vector2.Distance(transform.position, playerDetector.position);

        distToWakeUp = Vector2.Distance(transform.position, wakeUpDetectorPlayer.position);

        if (distToPlayer < aggroRange && !canAttack) 
        {
            chasePlayer();
            EnemyAnimation.Play("Wolf-run");
        }

        else if (distToPlayer > aggroRange && distToWakeUp > wakeUpRange && !canAttack)
        {
            stopChasingPlayer();
           
        }
        else if (distToPlayer > aggroRange && distToWakeUp > wakeUpRange && !canAttack) 
        {
            EnemyAnimation.Play("Wolf-Sleep");
        }
        else if (canAttack) 
        {
            EnemyAnimation.Play("Wolf-bite");
        }
        else 
        {
            EnemyAnimation.Play("Wolf_idle");
        }





        canAttack = Physics2D.Raycast(distance1.position, transform.right, distanceCheck, whatAttacking);



        if (isFacingRight && distToWakeUp < wakeUpRange) 
        {
            distanceCheck *= -1;
            Flip();
           
        }




    }

    private void FixedUpdate()
    {
        
        
    }

    private void chasePlayer() 
    {
        if(transform.position.x < playerDetector.position.x) 
        {   //left side move
            enemyRb.velocity = new Vector2(moveSpeed, 0);
            transform.localScale = new Vector2(-1, 1);       // Flip Sprite

           
        }
        else if(transform.position.x > playerDetector.position.x)
        {   //right side move
            enemyRb.velocity = new Vector2(-moveSpeed, 0);
            transform.localScale = new Vector2(1, 1);
            
            
            
        }
    }

    private void stopChasingPlayer()
    {   // MoveSpeed auf 0 setzen
        enemyRb.velocity = new Vector2(0, 0);

    }
    
    private void Flip() 
    {

        
        wolf.transform.Rotate(0.0f, 180.0f, 0.0f);
        //detector.transform.Rotate(180.0f, 0.0f, 0.0f);

    }

    private void OnDrawGizmos()
    {
       
        Gizmos.DrawLine(distance1.position, new Vector2(distance1.position.x + distanceCheck, distance1.position.y));
    }

}


