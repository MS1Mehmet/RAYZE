using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Source https://www.youtube.com/watch?v=lHLZxd0O6XY ChronoAbi
public class EnemyFollowPlayerShoot2 : MonoBehaviour
{
    public float speed;
    public float lineOfSite;
    public float shootingRange;
    public GameObject bullet;           //## the bullets which will be shoot ##  
    public GameObject bulletParent;     //## Position from the shooting enemy##
    private Transform player;
    public float fireRate = 1f;
    public float nextFireTime;
    private bool facingLeft = false;


    //####### Der player wird hier als objekt intialisiert, aber über denn Tag Player, somit kann er anvisiert werden! ###########
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    
    //##### Wenn der Player in die Reichweite des Radiuses kommt, dann wird er sich in seine Richtung bewegen#######
    // Update is called once per frame
    void FixedUpdate()
    {   
        float distanceFromplayer = Vector2.Distance(player.position, transform.position);
        if (distanceFromplayer < lineOfSite && distanceFromplayer>shootingRange)
        {
             transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
             FlipTowardsPlayer();
        }
        else if(distanceFromplayer <= shootingRange && nextFireTime < Time.time)
        {
            Instantiate(bullet, bulletParent.transform.position, Quaternion.identity);
            nextFireTime = Time.time + fireRate;
            FlipTowardsPlayer();
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


    //### Hier wird der Radius dargestellt und sobald sich der player() in diesem Radius befindet wird er angegriffen###!!
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position,lineOfSite);
        Gizmos.DrawWireSphere(transform.position,shootingRange);
    }



    
}
