using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour,IDamageable
{
    Rigidbody2D rb2d;
   
   
    public Player playerScr;
    public int hitDamage;

    public int health;
    BoxCollider2D box2d;
    [SerializeField]
    GameObject deathExplosion;

    SpriteRenderer sprite;
    GameObject explodeEffect;

    // Start is called before the first frame update
    void Start()
    {

       
        box2d = GetComponent<BoxCollider2D>();
        rb2d = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage(int amount)
    {
        health -= amount;

        if (health <= 0)
        {
            Destroy(gameObject);
            explodeEffect = Instantiate(deathExplosion);
            explodeEffect.name = deathExplosion.name;
            explodeEffect.transform.position = sprite.bounds.center;
            Destroy(explodeEffect, 1.2f);
        }
        /*else
        {
            EnemyAnimation.Play("hurt");
        }*/
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerScr.DamageState.PlayerTakeDamage(hitDamage);
            playerScr.SetTakenHit(true);
            Debug.Log(hitDamage);
        }
    }

    /*
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerScr.DamageState.PlayerTakeDamage(hitDamage);
            playerScr.SetTakenHit(true);
            Debug.Log(hitDamage);
        }
    }

    */


}
