using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    #region Variablen
    //Input Variablen
    protected int xInput;
    protected int yInput;

    private bool jumpInput;
    private bool grabInput;
    private bool attackInput;

    //Check Variablen
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

        if (attackInput)
        {
            stateMachine.ChangeState(player.AttackState);
        }
        else if (player.isHit)
        {
            stateMachine.ChangeState(player.DamageState);
        }
        else if (jumpInput && player.JumpState.CanJump())
        {
            player.CreateDust();
            player.InputHandler.UseJumpInput();
            stateMachine.ChangeState(player.JumpState);
        } else if (!isGrounded) 
        {
            player.JumpState.DecreaseAmountOfJumpsLeft();
            stateMachine.ChangeState(player.InAirState);
        } else if (isTouchingClimbWall && grabInput)
        {
            stateMachine.ChangeState(player.WallGrabState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
