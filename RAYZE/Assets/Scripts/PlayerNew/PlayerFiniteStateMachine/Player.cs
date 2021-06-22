using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Player : MonoBehaviour
{
    #region State Variables
    public PlayerStateMachine StateMachine { get; private set; }

    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set;}
    public PlayerInAirState InAirState { get; private set; }
    public PlayerWallSlideState WallSlideState { get; private set; }
    public PlayerWallGrabState WallGrabState { get; private set; }
    //public PlayerWallClimbState WallClimbState { get; private set; } //[Animationen fehlen]
    public PlayerWallJumpState WallJumpState { get; private set; }
    public PlayerDamageState DamageState { get; private set; }
    public PlayerDeathState DeathState { get; private set; }

    [SerializeField]
    private PlayerData playerData;
    #endregion

    #region Components
    public Animator Anim { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public Rigidbody2D RB { get; private set; }
    public BoxCollider2D BC { get; private set; }
    public Renderer Rend { get; private set; }
    public WeaponInventory WI { get; private set; }
    public ParticleSystem dust;
    public ParticleSystem walldust;
    #endregion

    #region Check Transform

    [SerializeField]
    private Transform groundCheck;

    [SerializeField]
    private Transform wallCheck;

    [SerializeField]
    private Transform gunPoint;

    [SerializeField]
    private Transform gunPointUp;

    public GameObject currentCheckpoint;
    #endregion

    #region Other Variables
    public Vector2 CurrentVelocity { get; private set; }
    public int FacingDirection { get; private set; }

    private Vector2 workspace;
    private Vector2 colliderSize;
    
    public bool isOnSlope;
    private float slopeDownAngleOld;
    private float slopeDownAngle;

    private GameObject bullet;
    private Color defaultcolor;
    public bool coolDownActiv;

    public bool isHit { get; private set; }
    public bool isDeath { get; private set; }
    public bool isInvincibil { get; private set; }
    #endregion

    #region Unity Callback Function
    private void Awake()
    {
        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, playerData, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "move");
        JumpState = new PlayerJumpState(this, StateMachine, playerData, "inAir");
        InAirState = new PlayerInAirState(this, StateMachine, playerData, "inAir");
        WallSlideState = new PlayerWallSlideState(this, StateMachine, playerData, "wallSlide");
        WallGrabState = new PlayerWallGrabState(this, StateMachine, playerData, "wallSlide");
        //WallClimbState = new PlayerWallClimbState(this, StateMachine, playerData, "wallClimb"); // Siehe oben
        WallJumpState = new PlayerWallJumpState(this, StateMachine, playerData, "inAir");
        DamageState = new PlayerDamageState(this, StateMachine, playerData, "damage");
        DeathState = new PlayerDeathState(this, StateMachine, playerData, "death");
    }

    private void Start()
    {
        Anim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        RB = GetComponent<Rigidbody2D>();
        BC = GetComponent<BoxCollider2D>();
        Rend = GetComponent<Renderer>();
        WI = GetComponent<WeaponInventory>();

        colliderSize = BC.size;
        FacingDirection = 1;
        StateMachine.Initialize(IdleState);
        defaultcolor = Rend.material.color;
        isHit = false;
        isDeath = false;
        coolDownActiv = false;
        isOnSlope = false;
        isInvincibil = false;
    }

    private void Update()
    {
        CurrentVelocity = RB.velocity;
        StateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && isHit == false && !isDeath)
        {
            SetTakenHit(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IUpgradeables upgradeables = collision.GetComponent<IUpgradeables>();
        if (upgradeables != null)
        {
            upgradeables.Upgrade();
        }
    }
    #endregion

    #region Set Functions
    public void SetVelocityX(float velocity)
    {
        workspace.Set(velocity, CurrentVelocity.y);
        RB.velocity = workspace;
        CurrentVelocity = workspace;
    }

    public void SetVelocityY(float velocity)
    {
        workspace.Set(CurrentVelocity.x, velocity);
        RB.velocity = workspace;
        CurrentVelocity = workspace;
    }

    public void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        workspace.Set(angle.x * velocity * direction, angle.y * velocity);
        RB.velocity = workspace;
        CurrentVelocity = workspace;
    }

    public void SetVelocityZero()
    {
        RB.velocity = Vector2.zero;
        CurrentVelocity = Vector2.zero;
    }

    public void SetZeroFriction() => RB.sharedMaterial = playerData.noFriction;
    public void SetFullFriction() => RB.sharedMaterial = playerData.fullFriction;

    public void SetTakenHit(bool state) => isHit = state;

    public void SetDeath(bool state) => isDeath = state;
    #endregion

    #region Check Functions

    public bool CheckIfGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, playerData.groundCheckRadius, playerData.whatIsGround);
    }

    public bool CheckIfPlattform()
    {
        return Physics2D.OverlapCircle(groundCheck.position, playerData.groundCheckRadius, playerData.whatIsPlattform);
    }

    public bool CheckIfTouchingWall()
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.right * FacingDirection, playerData.wallCheckDistance, playerData.whatIsWall);
    }

    public bool CheckIfTouchingWallBack()
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.right * -FacingDirection, playerData.wallCheckDistance, playerData.whatIsWall);
    }
    public bool CheckIfTouchingClimbWall()
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.right * FacingDirection, playerData.wallCheckDistance, playerData.whatIsClimbWall);
    }
    public bool CheckIfTouchingClimbWallBack()
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.right * -FacingDirection, playerData.wallCheckDistance, playerData.whatIsClimbWall);
    }
    public void CheckIfShouldFlip(int xInput)
    {
        if(xInput != 0 && xInput != FacingDirection)
        {
            Flip();
        }
    }

    public void Respawn()
    {
        transform.position = currentCheckpoint.transform.position;
    }

    public void SlopeCheck()
    {
        Vector2 checkPos = transform.position - new Vector3(0.0f, colliderSize.y / 2);
        RaycastHit2D hit = Physics2D.Raycast(checkPos, Vector2.down, playerData.slopeCheckDistance, playerData.whatIsGround);
        if (hit)
        {
            slopeDownAngle = Vector2.Angle(hit.normal, Vector2.up);

            if (slopeDownAngle != 0.0f)
            {
                isOnSlope = true;
            }
            else
            {
                isOnSlope = false;
            }
        }
    }
    #endregion

    #region Other Functions
    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();

    private void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();
    private void Flip()
    {
        FacingDirection *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }
    public void Shoot(bool UpLooking)
    {
        if (!coolDownActiv)
        {
            if (UpLooking)
            {
                Vector2 vector2 = new Vector2(0, 1);
                bullet = Instantiate(WI.defaultBullet, gunPointUp.position, Quaternion.identity);
                bullet.transform.Rotate(0.0f, 0.0f, 90.0f);
                bullet.GetComponent<Rigidbody2D>().velocity = vector2 * playerData.weaponSpeed;
            }
            else
            {
                Vector2 vector2 = new Vector2(1, 0);
                bullet = Instantiate(WI.defaultBullet, gunPoint.position, Quaternion.identity);
                if (FacingDirection == -1)
                {
                    bullet.transform.Rotate(0.0f, 180.0f, 0.0f);
                }
                bullet.GetComponent<Rigidbody2D>().velocity = (vector2 * FacingDirection) * playerData.weaponSpeed;
            }
            StartCoroutine("CoolDown");
        }
    }
    public void CreateDust()
    {
        dust.Play();
    }
    public void CreateWallDust()
    {
        walldust.Play();
    }

    public void PlayerIgnoreLayer(int IgnoreLayer, bool isIngnoring)
    {
        Physics2D.IgnoreLayerCollision(8, IgnoreLayer, isIngnoring);
    }
    #endregion

    #region IEnumerator
    private IEnumerator Invincibil()
    {
        isInvincibil = true;
        PlayerIgnoreLayer(10, true);
        defaultcolor.a = 0.5f;
        Rend.material.color = defaultcolor;
        yield return new WaitForSeconds(playerData.invincibilTime);
        for (int x = 0; x <= 3; x++)
        {
            defaultcolor.a = 1f;
            Rend.material.color = defaultcolor;
            yield return new WaitForSeconds(0.1f);
            defaultcolor.a = 0.5f;
            Rend.material.color = defaultcolor;
            yield return new WaitForSeconds(0.1f);
        }
        isInvincibil = false;
        PlayerIgnoreLayer(10, false);
        defaultcolor.a = 1f;
        Rend.material.color = defaultcolor;
    }

    private IEnumerator CoolDown()
    {
        coolDownActiv = true;
        yield return new WaitForSeconds(playerData.weaponCoolDown);
        coolDownActiv = false;
    }

    private IEnumerator IgnorePlattformTimer()
    {
        PlayerIgnoreLayer(14, true);
        yield return new WaitForSeconds(1f);
        PlayerIgnoreLayer(14, false);
    }
    #endregion
}
