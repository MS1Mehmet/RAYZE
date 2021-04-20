using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Code kopiert und modifiziert von Tony Bhimani, Link verfügbar unter: https://www.youtube.com/user/TonyBhimani
public class BulletScript : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb2d;
    SpriteRenderer sprite;
    float destroyTime;
   // public float destroyTime;
    bool freezeBullet;

    public float coolDown = 10.0f;
    float waitTime = 0.0f;
    bool coolDownActive = false;

    RigidbodyConstraints2D rb2dConstraints;
    public int damage = 1; //Schaden

    [SerializeField] float bulletSpeed;     //Geschwindigkeit
    [SerializeField] Vector2 bulletDirection;       //Richtung
    //[SerializeField] float destroayDelay;       //Wann unser Schuss zerstört wird
    
    // Start is called before the first frame update
    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
       if (freezeBullet) return;
        destroyTime -= Time.deltaTime;
        if(destroyTime < 0)
        {
            Destroy(gameObject,2.0f);  //Der Schuss wird nach einer Zeit zerstört
        }

        if (coolDownActive)
        {
            waitTime *= Time.deltaTime;
        }

        if (waitTime > coolDown)
        {
            Debug.Log(waitTime);
            waitTime = 0.0f;
            Debug.Log(waitTime);
            coolDownActive = false;
        }
    }

    public void SetBulletSpeed(float speed)
    {
        this.bulletSpeed = speed;
    }

    public void SetBulletDirection(Vector2 direction)
    {
        this.bulletDirection = direction;
    }

    public void SetDamageValue ( int damage)
    {
        this.damage = damage;
    }

    public void SetDestroyDelay(float delay)
    {
        //this.destroayDelay = delay;
    }

    public void Shoot()
    {
        if (coolDownActive == false)
        {
            sprite.flipX = (bulletDirection.x < 0);     // Den Sprite Flippen, wenn wir nach rechts oder links schießen
            rb2d.velocity = bulletDirection * bulletSpeed;
            coolDownActive = true;
            // destroyTime = destroayDelay;
        }
    }

    public void FreezeBullet(bool freeze)
    {
        if(freeze)
        {
            freezeBullet = true;
            rb2dConstraints = rb2d.constraints;
            animator.speed = 0;
            rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
            rb2d.velocity = Vector2.zero;
        }
        else
        {
            freezeBullet = false;
            animator.speed = 1;
            rb2d.constraints = rb2dConstraints;
            rb2d.velocity = bulletDirection * bulletSpeed;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Enemy")|| other.gameObject.CompareTag("OldSrctySystem"))
        {
            EnemyController enemy = other.gameObject.GetComponent<EnemyController>();
            EnemyController OldSrctySystem = other.gameObject.GetComponent<EnemyController>();
            if (enemy !=null || OldSrctySystem != null)
            {
                enemy.TakeDamage(this.damage);
                OldSrctySystem.TakeDamage(this.damage);

            }
            Destroy(gameObject, 0.01f);   //Wenn unser Schuss "enemy" berührt, dann wird der Schuss zerstört

        }


        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject,0.0f);
        }
    }
}
