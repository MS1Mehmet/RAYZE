using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    #region Variablen
    //Input Variablen
    protected int xInput;
    protected int yInput;
    protected bool attackInput;

    private bool jumpInput;
    private bool grabInput;

    //Check Variablen
    protected bool anispamstop;

    private bool isGrounded;
    private bool isTouchingClimbWall;
    #endregion

    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = player.CheckIfGrounded();
        isTouchingClimbWall = player.CheckIfTouchingClimbWall();
    }

    public override void Enter()
    {
        base.Enter();

        player.JumpState.ResetAmountOfJumpsLeft();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        xInput = player.InputHandler.NormInputX;
        yInput = player.InputHandler.NormInputY;
        jumpInput = player.InputHandler.JumpInput;
        grabInput = player.InputHandler.GrabInput;
        attackInput = player.InputHandler.AttackInput;

        player.SlopeCheck();

        //Switcht zur DamageState
        if (player.isHit)
        {
            stateMachine.ChangeState(player.DamageState);
        }
        //Switcht zur JumpState
        else if (jumpInput && player.JumpState.CanJump())
        {
            player.InputHandler.UseJumpInput();
            stateMachine.ChangeState(player.JumpState);
        }
        //Switcht zur InAirState
        else if (!isGrounded) 
        {
            player.JumpState.DecreaseAmountOfJumpsLeft();
            stateMachine.ChangeState(player.InAirState);
        }
        //Switcht zur WallGrabState
        else if (isTouchingClimbWall && grabInput)
        {
            stateMachine.ChangeState(player.WallGrabState);
        }
        if (player.isOnSlope && xInput == 0)
        {
            player.SetFullFriction();
        }
        else
        {
            player.SetZeroFriction();
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
