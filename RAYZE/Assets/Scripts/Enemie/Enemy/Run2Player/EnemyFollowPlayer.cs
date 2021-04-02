using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Source https://www.youtube.com/watch?v=lHLZxd0O6XY ChronoAbi
public class EnemyFollowPlayer : MonoBehaviour
{   

    public float speed;
    private Transform player;
    public float lineOfSite;

    private bool facingLeft = false;

    //####### Der player wird hier als objekt intialisiert, aber über denn Tag Player, somit kann er anvisiert werden! ###########
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    
    //##### Wenn der Player in die Reichweite des Radiuses kommt, dann wird er sich in seine Richtung bewegen#######
    // Update is called once per frame
    void Update()
    {   
        float distanceFromplayer = Vector2.Distance(player.position, transform.position);
        if(distanceFromplayer < lineOfSite)
        {
        transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
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
    }
}
