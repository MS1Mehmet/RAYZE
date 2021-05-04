using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//By Mehmet Budak

public class MovingPlatform : MonoBehaviour
{
    public Transform pos1, pos2;
    public float speed;
    public Transform startPos;
    Vector2 nextPos;
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        nextPos = pos2.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position == pos2.position)
        {
            nextPos = pos1.position;
        }
        
        if (transform.position == pos1.position) 
        {
            nextPos = pos2.position;
        }

        transform.position = Vector2.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(pos1.position, pos2.position);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == Player)
        {
            Player.transform.parent = transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        {
            if (collision.gameObject == Player)
            {
                Player.transform.parent = null;
            }
        }

    }
}
