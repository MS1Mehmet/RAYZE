using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

//  Code kopiert und modifiziert von Tony Bhimani, Link verfügbar unter: https://www.youtube.com/user/TonyBhimani“
public class PlayerController : MonoBehaviour
{
    
    Animator animator;
    BoxCollider2D box2d;
    Rigidbody2D rb2d;
    SpriteRenderer sprite;

    public SoundManager SoundManag;

    [SerializeField] float moveSpeed = 3f; //Bewegungs geschwindigkeit
    [SerializeField] float jumpSpeed = 3f; // Sprungkraft
    [SerializeField] float teleportSpeed = -10f;
    [SerializeField] int bulletDamage = 1;
    [SerializeField] float bulletSpeed = 5f;
    [SerializeField] Transform bulletShootPos;
    [SerializeField] GameObject bulletPrefab;

   // [SerializeField] GameObject explodeEffectPrefab;

    public enum TeleportState {Descending,Landed,Idle};
    [SerializeField] TeleportState teleportState;
    public float wallCheckDistance;
    public float wallSlidingSpeed;
    public float groundCheckRadius;
    public LayerMask whatIsGround;

    public Transform groundCheck;
    public Transform wallCheck;

    float keyVertical;
    float keyHorizontal;
    bool keyJump;
    bool keyShoot;
    
    

    bool isGrounded;
    bool isTouchingWall;
    bool isWallSliding;
    bool isShooting;
    bool isTeleporting;
    bool isTakingDamage;
    bool isInvincible;
    bool isFacingRight;   // In welcher Richtung wir gucken

    bool hitSideRight;      // Ob wir von der Rechten oder Linken Seite getroffen werden, für die Hit Animation

    bool freezeInput;
    bool freezePlayer;
    bool freezeBullets;

    float shootTime;
    bool keyShootRelease;

    RigidbodyConstraints2D rb2dConstraints;

    public int currentHealth;
    public int maxHealth = 28;

    void Awake()
    {
        animator = GetComponent<Animator>();
        box2d = GetComponent<BoxCollider2D>();
        rb2d = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

    }
    void Start()
    {

        isFacingRight = true; // Char guckt nach rechts

        currentHealth = maxHealth;
    }

    private void FixedUpdate()
    {
        
        CheckSurroundings();
      
    }

    private void CheckIfWallSliding()
    {
        if(isTouchingWall && !isGrounded && rb2d.velocity.y < 0)
        {
            isWallSliding = true;
        }
        else
        {
            isWallSliding = false;
        }
        

    }


    private void CheckSurroundings()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);
    }

    void Update()
    {
        if(isTeleporting)
        {
            switch (teleportState)
            {
                case TeleportState.Descending:
                    if(isGrounded)
                    {
                        teleportState = TeleportState.Landed;
                    }
                    break;
                case TeleportState.Landed:
                    animator.speed = 1;
                    break;
                case TeleportState.Idle:
                    Teleport(false);
                    break;
            }
            return;
        }
       
        //keyHorizontal = Input.GetAxisRaw("Horizontal");
        //keyJump = Input.GetKeyDown(KeyCode.Space);
        //keyShoot = Input.GetKey(KeyCode.Mouse0);

        //isShooting = keyShoot;

        if(isTakingDamage)
        {
            animator.Play("Ryze_Hit");
            return;
        }

        PlayerDebugInput();
        PlayerDirectionInput();
        PlayerJumpInput();
        PlayerShootInput();
        PlayerMovement();
        CheckIfWallSliding();
    }

    void PlayerDebugInput()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
            if(bullets.Length > 0)
            {
                freezeBullets = !freezeBullets;
                foreach(GameObject bullet in bullets)
                {
                    bullet.GetComponent<BulletScript>().FreezeBullet(freezeBullets);
                }


            }
            Debug.Log("Freeze Bullets:" + freezeBullets);
        }
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            Defeat();
            Debug.Log("Defeat()");
        }

        if(Input.GetKeyDown(KeyCode.I))
        {
            Invicible(!isInvincible);
            Debug.Log("Invicible:" + isInvincible);

        }

        if(Input.GetKeyDown(KeyCode.K))
        {
            FreezeInput(!freezeInput);
            Debug.Log("Freeze Input;" + freezeInput);
            
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            FreezePlayer(!freezePlayer);
            Debug.Log("Freeze Player;" + freezePlayer);

        }

        //T für Teleport
        if (Input.GetKeyDown(KeyCode.T))
        {
            Teleport(true);
            Debug.Log("Teleport");
        }
    }

    void PlayerDirectionInput()
    {
        keyHorizontal = Input.GetAxisRaw("Horizontal");
        keyVertical = Input.GetAxisRaw("Vertical");


        if (!freezeInput)
        {
            keyHorizontal = Input.GetAxisRaw("Horizontal");
        }
    }

    void PlayerJumpInput()
    {
        if(!freezeInput)
        {
            keyJump = Input.GetKeyDown(KeyCode.Space);

        }

    }

    // Schießfunktion
    void PlayerShootInput()
    {
        if (!freezeInput)
        {
            keyShoot = Input.GetKey(KeyCode.Mouse0);
        }

        float shootTimeLenght = 0f;
        float keyShootReleaseTimeLenght = 0f;

        if(keyShoot && keyShootRelease)
        {
            isShooting = true;
            keyShootRelease = false;
            shootTime = Time.time;
            // Den Schuss abfeuern
            Invoke("ShootBullet", 0.3f);
        }
        if(!keyShoot && !keyShootRelease)
        {
            keyShootReleaseTimeLenght = Time.time - shootTime;
            keyShootRelease = true;
        }
        if(isShooting)
        {
            shootTimeLenght = Time.time - shootTime;
            if(shootTimeLenght >= 0.25f || keyShootReleaseTimeLenght >= 0.15f) // Die Zeit, wie lange wir schießen können
            {
                isShooting = false;          
            }
        }
    }

    void PlayerMovement()
    {
        if (keyHorizontal < 0)
        {
            if (isFacingRight)
            {
                Flip();
            }
            if (isGrounded)
            {
                if (isShooting)
                {
                    animator.Play("Ryze_RunShoot");
                }
                else
                {
                    animator.Play("Ryze_Run");

                }
            }
            rb2d.velocity = new Vector2(-moveSpeed, rb2d.velocity.y);
        }
        else if (keyHorizontal > 0)
        {
            if (!isFacingRight)
            {
                Flip();
            }
            if (isGrounded)
            {
                if (isShooting)
                {
                    animator.Play("Ryze_RunShoot");
                }
                else
                {
                    animator.Play("Ryze_Run");

                }
            }
            rb2d.velocity = new Vector2(moveSpeed, rb2d.velocity.y);
        }
        else
        {
            if (isGrounded)
            {
                if (isShooting)
                {
                    animator.Play("Ryze_Shoot");
                }
                else
                {
                    animator.Play("Ryze_Idle");

                }
            }

            rb2d.velocity = new Vector2(0f, rb2d.velocity.y);
        }
        if (keyJump && isGrounded)
        {
            if (isShooting)
            {
                animator.Play("Ryze_JumpShoot");
            }
            else
            {
                animator.Play("Ryze_Jump");

            }
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpSpeed);

        }

        if (!isGrounded)
        {
            if (isShooting)
            {
                animator.Play("Ryze_JumpShoot");

            }
            else
            {
                animator.Play("Ryze_Jump");
            }

        }

        if(keyVertical > 0)
        {
            Input.GetKeyDown(KeyCode.UpArrow);
            animator.Play("Ryze_ShootUp");
        }

        if(isWallSliding)
        {
            if(rb2d.velocity.y < wallSlidingSpeed)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, -wallSlidingSpeed);
            }
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0f, 180f, 0);
    }

    public void ApplyLifeEnergy(int amount)
    {
        if(currentHealth < maxHealth)
        {
            int healthDiff = maxHealth - currentHealth;
            if(healthDiff > amount) healthDiff = amount;
            StartCoroutine(AddLifeEnergy(healthDiff));
        }
    }

    private IEnumerator AddLifeEnergy(int amount)
    {
        for(int i = 0; i < amount; i++)
        {
            currentHealth++;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
            yield return new WaitForSeconds(0.05f);
        }
    }

    public void ApplyWeaponEnergy(int amount)
    {

    }

    void ShootBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletShootPos.position, Quaternion.identity);
        bullet.name = bulletPrefab.name;
        bullet.GetComponent<BulletScript>().SetDamageValue(bulletDamage);
        bullet.GetComponent<BulletScript>().SetBulletSpeed(bulletSpeed);
        bullet.GetComponent<BulletScript>().SetBulletDirection((isFacingRight) ? Vector2.right : Vector2.left);
        bullet.GetComponent<BulletScript>().SetDestroyDelay(5f);
        bullet.GetComponent<BulletScript>().Shoot();
     }

    public void HitSide(bool rightSide)
    {
        hitSideRight = rightSide;
    }

    public void Invicible(bool invincibility)
    {
        isInvincible = invincibility;
    }

    public void TakeDamage(int damage)
    {
        if (!isInvincible)
        {
            currentHealth -= damage;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Die Range, dass es niemals unter 0 geht und über den maxHealth
            UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);    // Wie viel leben wir noch übrig haben
            if (currentHealth <= 0)
            {
                Defeat();
            }
            else
            {
                StartDamageAnimation();
            }
        }
    }

    void StartDamageAnimation()
    {
        if(!isTakingDamage)
        {
            isTakingDamage = true;   //Wenn wir noch keinen schaden nehmen, dann können wir schaden nehmen
            Invicible(true);    //In der Zeit, wo wir Schaden nehmen, sind wir invicible
            FreezeInput (true);
            float hitForceX = 0.50f;
            float hitForceY = 1.5f;

            if (hitSideRight) hitForceX = -hitForceX;       // In Welcher Richtung wir bouncen, wenn wir getroffen werden
            rb2d.velocity = Vector2.zero;
            rb2d.AddForce(new Vector2(hitForceX, hitForceY), ForceMode2D.Impulse);
        }
    }

    void StopDamageAnimation()
    {
        isTakingDamage = false;
        //Invicible(false);
        FreezeInput(false);
        animator.Play("Ryze_Hit", -1, 0f);
        StartCoroutine(FlashAfterDamage());
    }

    private IEnumerator FlashAfterDamage()
    {
        float flashDelay = 0.0833f;
        for(int i= 0; i < 10; i++)
        {
            //sprite.enabled = false;
            //sprite.material = null;
            sprite.color = new Color(1, 1, 1, 0);
            yield return new WaitForSeconds(flashDelay);
            //sprite.enabled = true;
            //sprite.material = new Material(Shader.Find("Sprites/Default"));
            sprite.color = new Color(1, 1, 1, 1);
            yield return new WaitForSeconds(flashDelay);
        }
        Invicible(false);
    }

    void StartDefeatAnimation()
    {
        //FreezeInput(true);
        //FreezePlayer(true);
       // GameObject explodeEffect = Instantiate(explodeEffectPrefab);
        //explodeEffect.name = explodeEffectPrefab.name;
        //explodeEffect.transform.position = sprite.bounds.center;
        //Destroy(gameObject);        //Rayze wird zerstört
    }
    
    void StopDefeatAnimation()
    {
       // FreezeInput(false);
        //FreezePlayer(false);
       // GameObject explodeEffect = Instantiate(explodeEffectPrefab);
        //explodeEffect.name = explodeEffectPrefab.name;
        //explodeEffect.transform.position = sprite.bounds.center;
       // Destroy(gameObject);        //Rayze wird zerstört
    }

    void Defeat()
    {
        GameManager.Instance.PlayerDefeated(); // Spiel ist vorbei/Spiel wird neu geladen
       // Invoke("StartDefeatAnimation", 0.5f);
    }

    public void FreezeInput(bool freeze)
    {
        freezeInput = freeze;

    }

    public void FreezePlayer(bool freeze)
    {
        if (freeze)
        {
            freezePlayer = true;
            rb2dConstraints = rb2d.constraints;
            animator.speed = 0;
            rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else
        {
            freezePlayer = false;
            animator.speed = 1;
            rb2d.constraints = rb2dConstraints;

        }
    }

    public void Teleport(bool teleport)
    {
        if(teleport)
        {
            isTeleporting = true;
            FreezeInput(true);
            animator.Play("Ryze_Teleport");
            animator.speed = 0;
            teleportState = TeleportState.Descending;
            rb2d.velocity = new Vector2(rb2d.velocity.x, teleportSpeed);
        }
        else
        {
            isTeleporting = false;
            FreezeInput(false);
        }
    }

    void TeleportAnimationEnd()
    {
        teleportState = TeleportState.Idle;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position,groundCheckRadius);
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y, wallCheck.position.z));
    }

   
  
}
