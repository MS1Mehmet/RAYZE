using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfEnemy : MonoBehaviour
{
    private Transform player; // not used



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
    Transform attackRadius;

    [SerializeField]
    Transform attackPos;

    

    public float distanceCheck;
    public LayerMask whatAttacking;

    public float attackY, attackX;
    public float Hitdamage;

    public float timeBtwAttack;
    public float startAttack;

    private bool canAttack;
    public bool isFacingRight;

    Rigidbody2D enemyRb;
    float distToPlayer;


    private GameObject wolf; // für spätere Effekte
   

    Animator EnemyAnimation;                                                                                    

                                                
    void Start()
    {
        //  detector = transform.Find("attackCheck").gameObject;
        // wolf = transform.Find("Alien_Wolf").gameObject;
        
        
        enemyRb = GetComponent<Rigidbody2D>();
        EnemyAnimation = GetComponent<Animator>();
      

    }

    // Update is called once per frame
    void Update()
    {
        if (playerDetector && wakeUpDetectorPlayer != null)
        {
            distToPlayer = Vector2.Distance(transform.position, playerDetector.position);


            distToWakeUp = Vector2.Distance(transform.position, wakeUpDetectorPlayer.position);






            // canAttack = Physics2D.Raycast(distance1.position, transform.right, distanceCheck, whatAttacking); old Raycast
            canAttack = Physics2D.OverlapCircle(attackRadius.transform.position, distanceCheck, whatAttacking);



            if (isFacingRight && distToWakeUp < wakeUpRange)
            {
                distanceCheck *= -1;
                Flip();

            }


            if (distToPlayer < aggroRange && !canAttack)
            {
                chasePlayer();

            }

            else if (distToPlayer > aggroRange && distToWakeUp > wakeUpRange && !canAttack)
            {
                stopChasingPlayer();

            }
            else if (distToPlayer > aggroRange && distToWakeUp > wakeUpRange && !canAttack)
            {
                wolfSleepState();
            }
            else if (canAttack)
            {
                wolfAttackState();

            }
            // else if ()
            else
            {
                wolfIdleState();
            }


        }

        deathState();
    }
    void wolfAttackState()
    {
        
        
            if (canAttack && timeBtwAttack <=0 )
            {
                Collider2D[] playerToDmg = Physics2D.OverlapBoxAll(attackPos.position, new Vector2(attackX, attackY), 0, whatAttacking);
                for (int i = 0; i < playerToDmg.Length; i++)
                {
                    playerToDmg[i].GetComponent<Player>().TakeDamege(Hitdamage);
                    EnemyAnimation.Play("Wolf-bite");
                }
                timeBtwAttack = startAttack;

            }
            else 
            {
                timeBtwAttack -= Time.deltaTime;
               // wolfIdleState();
            }
           
    }


    private void chasePlayer() 
    {
        
        
            if (transform.position.x < playerDetector.position.x)
            {   //left side move
                enemyRb.velocity = new Vector2(moveSpeed, 0);
                transform.localScale = new Vector2(-1, 1);       // Flip Sprite
                EnemyAnimation.Play("Wolf-run");

            }
            else if (transform.position.x > playerDetector.position.x)
            {   //right side move
                enemyRb.velocity = new Vector2(-moveSpeed, 0);
                transform.localScale = new Vector2(1, 1);

                EnemyAnimation.Play("Wolf-run");

            }
        
    }

    private void wolfSleepState() 
    {
        EnemyAnimation.Play("Wolf-Sleep");
    }

    private void wolfIdleState()
    {
        
        EnemyAnimation.Play("Wolf_idle");
    }

    private void stopChasingPlayer()
    {   // MoveSpeed auf 0 setzen
        enemyRb.velocity = new Vector2(0, 0);

    }

    private void deathState()
    {

    }


    private void Flip() 
    {

        
        transform.Rotate(0.0f, 180.0f, 0.0f);
        

    }

    private void OnDrawGizmos()
    {
        if (playerDetector && wakeUpDetectorPlayer != null)
        {
            // Gizmos.DrawLine(distance1.position, new Vector2(distance1.position.x + distanceCheck, distance1.position.y)); old Raycast
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(attackRadius.transform.position, distanceCheck);  // Angriffszone

            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(playerDetector.transform.position, aggroRange);

            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(attackPos.transform.position, new Vector3(attackX, attackY));
        }
    }



}


