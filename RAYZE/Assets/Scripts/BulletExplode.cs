using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletExplode : MonoBehaviour
{
    

    Animator animator;
    BoxCollider2D box2d;
    Rigidbody2D rb2d;
    SpriteRenderer sprite;

    bool isInvincible;      // Ob man schaden nimmt oder nicht

    GameObject explodeEffect;

    RigidbodyConstraints2D rb2dConstraints;

    public bool freezeEnemy;

    public int scorePoints = 500;

    public int currentHealth;
    public int maxHealth = 1;
    public int contactDamage = 1;       //Wenn Rayze auf ein Gegner trifft, dann kriegen wir schade
    public int explosionDamage = 0;

    [Header("Bonus Item Setting")]

    public ItemScript.ItemTypes bonusItemType;
    public float bonusDestroyDelay = 5f;
    public Vector2 bonusVelocity = new Vector2(0, 3f);

    [SerializeField] GameObject explodeEffectPrefab;
    // Start is called before the first frame update
    void Start()
    {

        animator = GetComponent<Animator>();
        box2d = GetComponent<BoxCollider2D>();
        rb2d = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

        currentHealth = maxHealth;
    }

   
    public void Invincible(bool inviciblity)
    {
        isInvincible = inviciblity;
    }

    public void TakeDamage(int damage)
    {
        if (!isInvincible)
        {
            currentHealth -= damage;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Die Range, dass es niemals unter 0 geht und über den maxHealth
            if (currentHealth <= 0)
            {
                Defeat();
            }
        }
    }

    void StartDefeatAnimation()
    {
        explodeEffect = Instantiate(explodeEffectPrefab);
        explodeEffect.name = explodeEffectPrefab.name;
        explodeEffect.transform.position = sprite.bounds.center;
        explodeEffect.GetComponent<ExplosionScript>().SetDamageValue(this.explosionDamage);
        Destroy(explodeEffect, 1.2f);

        GameObject bonusItemPrefab = GameManager.Instance.GetBonusItem(bonusItemType);
        if (bonusItemPrefab != null)
        {
            GameObject bonusItem = Instantiate(bonusItemPrefab);
            bonusItem.name = bonusItemPrefab.name;
            bonusItem.transform.position = explodeEffect.transform.position;
            bonusItem.GetComponent<ItemScript>().Animate(true);
            bonusItem.GetComponent<ItemScript>().SetDestroyDelay(bonusDestroyDelay);
            //bonusItem.GetComponent<ItemScript>().SetBonusBallColor(bonusBallColor);
            bonusItem.GetComponent<Rigidbody2D>().velocity = bonusVelocity;
        }



    }

    void StopDefeatAnimation()
    {
        Destroy(explodeEffect);
    }

    void Defeat()
    {
        
        StartDefeatAnimation();
        Destroy(gameObject);
        GameManager.Instance.AddScorePoints(this.scorePoints);
    }

    public void FreezeEnemy(bool freeze)
    {
        if (freeze)
        {
            freezeEnemy = true;
            animator.speed = 0;
            rb2dConstraints = rb2d.constraints;
            rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else
        {
            freezeEnemy = false;
            animator.speed = 1;
            rb2d.constraints = rb2dConstraints;

        }


    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();

            player.HitSide(transform.position.x > player.transform.position.x);     // Auf welcher Seiter der Player getrpffen wird
            player.TakeDamage(this.contactDamage);
        }
    }
}
