using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="newPlayerDate", menuName ="Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
    ////////////////////////////
    // Hauptstats vom Player

    [Header("Health")]
    public int playerMaxHealth = 100;
    public int playerCurrentHealth = 100;

    [Header("Damage and Death State")]
    public float hitVelocity = 40f;
    public Vector2 hitAngle = new Vector2(4, 4);
    public float deathTime = 10f;

    [Header("Move State")]
    public float movementVelocity = 10f;

    [Header("Jump State")]
    public float jumpVelocity = 15f;
    public int amountOfJumps = 3;

    [Header("Wall Jump State")]
    public float wallJumpVelocity = 20;
    public float wallJumpTime = 0.4f;
    public Vector2 wallJumpAngle = new Vector2(1, 2);

    [Header("In Air State")]
    public float variableJumpHeightMultiplier = 0.5f;

    [Header("Wall Slide State")]
    public float wallSlideVelocity = 3f;

    [Header("Check Variables")]
    public float groundCheckRadius = 0.3f;
    public float wallCheckDistance = 0.5f;
    public LayerMask whatIsGround;
    public LayerMask whatIsWall;
    public LayerMask whatIsClimbWall;
   
}
