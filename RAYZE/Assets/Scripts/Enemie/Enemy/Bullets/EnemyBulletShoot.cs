using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyBulletShoot : MonoBehaviour
{
    GameObject target;
    public float speed;
    Rigidbody2D bulletRB;
    public int hitDamage;

    // Startfunktion aus dem Grund damit der player ausweiche kann// 
    // Start is called before the first frame update
    void Start()
    {
        bulletRB = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
        Vector2 moveDir = (target.transform.position - transform.position).normalized * speed;
        bulletRB.velocity = new Vector2(moveDir.x, moveDir.y);


        Destroy(this.gameObject, 3);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            collision.gameObject.GetComponent<Player>().SetTakenHit(true);
            collision.gameObject.GetComponent<Player>().DamageState.PlayerTakeDamage(hitDamage);
            Destroy(gameObject);
        }
    }


}



