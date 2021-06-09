using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TowerAI : MonoBehaviour
{
    // Detect variablen
   [SerializeField]
    private float hitRange;
    
    [SerializeField]
    private bool isDetected;

    [SerializeField]
    private Transform posDetect;

    [SerializeField]
    private LayerMask whatAttacking;


    // schießen Variabel

    [SerializeField]
    private Transform bullet;

    [SerializeField]
    private Transform bulletPos;

    [SerializeField]
    private float fireRate;

    [SerializeField]
    private float nextFireTime;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //isDetected = Physics2D.Raycast(posDetect.position, Vector2.down, hitRange, whatAttacking);
        isDetected = Physics2D.Raycast(posDetect.position, transform.right, hitRange, whatAttacking);

        shooting();


    }

    private void shooting() 
    {
        if (isDetected && nextFireTime < Time.time)
        {
            Instantiate(bullet, bulletPos.transform.position, Quaternion.identity);
            nextFireTime = Time.time + fireRate;
        }
    }






    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Gizmos.DrawLine(posDetect.position, new Vector2(posDetect.position.x, posDetect.position.y - hitRange)); Raycast up/down;
        Gizmos.DrawLine(posDetect.position, new Vector2(posDetect.position.x + hitRange, posDetect.position.y));
    }
}
