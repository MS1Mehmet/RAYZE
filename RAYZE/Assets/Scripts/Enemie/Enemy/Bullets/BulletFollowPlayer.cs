using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Source https://www.youtube.com/watch?v=lHLZxd0O6XY ChronoAbi
public class BulletFollowPlayer : MonoBehaviour
{
    public float speed = 5f;
    Transform player;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Destroy(this.gameObject, 4);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }
}
