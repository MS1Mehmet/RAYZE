using System.Collections;
using System.Collections.Generic;
//using UnityEditorInternal;
using UnityEngine;

public class PlayerInAirState : PlayerState
{

    private bool isGrounded;
    private int xInput;
    private int yInput;
    private bool jumpInput;
    private bool jumpInputStop;
    private bool isJumping;
    private bool isTouchingWall;
    private bool isTouchingWallBack;
    private bool isTouchingClimbWall;
    private bool isTouchingClimbWallBack;
    private bool grabInput;
    public PlayerInAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = player.CheckIfGrounded();
        isTouchingWall = player.CheckIfTouchingWall();
        isTouchingWallBack = player.CheckIfTouchingWallBack();
        isTouchingClimbWall = player.CheckIfTouchingClimbWall();
        isTouchingClimbWallBack = player.CheckIfTouchingClimbWallBack();
    }

    public override void Enter()
    {
        base.Enter();
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
        jumpInputStop = player.InputHandler.JumpInputStop;
        grabInput = player.InputHandler.GrabInput;

        checkJumpMultiplier();

        if (player.InputHandler.AttackInput)
        {
            stateMachine.ChangeState(player.AttackState);
        }
        if(player.isHit)
            {
                stateMachine.ChangeState(player.DamageState);
            }
        else if (isGrounded && player.CurrentVelocity.y < 0.01f)
        {
            if (xInput != 0)
            {
                stateMachine.ChangeState(player.MoveState);
            }
            else
            {
                stateMachine.ChangeState(player.IdleState);
            }
        }
        else if (jumpInput && (isTouchingWall || isTouchingWallBack))
        {
            player.WallJumpState.DetermineWallJumpDirection(isTouchingWall);
            stateMachine.ChangeState(player.WallJumpState);
        }
        else if (jumpInput && (isTouchingClimbWall || isTouchingClimbWallBack))
        {
            player.WallJumpState.DetermineWallJumpDirection(isTouchingClimbWall);
            stateMachine.ChangeState(player.WallJumpState);
        }
        else if (jumpInput && player.JumpState.CanJump())
        {
            player.InputHandler.UseJumpInput();
            stateMachine.ChangeState(player.JumpState);
        }
        else if (isTouchingClimbWall && grabInput)
        {
            stateMachine.ChangeState(player.WallGrabState);
        }
        else if ((isTouchingWall || isTouchingClimbWall) && xInput == player.FacingDirection && player.CurrentVelocity.y <= 0)
        {
            stateMachine.ChangeState(player.WallSlideState);
        }
        else
        {
            player.CheckIfShouldFlip(xInput);
            player.SetVelocityX(playerData.movementVelocity * xInput);
            
            player.Anim.SetFloat("yVelocity", player.CurrentVelocity.y);
            player.Anim.SetFloat("yInput", yInput);
        }
    }
    
    private void checkJumpMultiplier()
    {
        if (isJumping)
        {
            if (jumpInputStop)
            {
                player.SetVelocityY(player.CurrentVelocity.y * playerData.variableJumpHeightMultiplier);
                isJumping = false;
            }
            else if (player.CurrentVelocity.y <= 0f)
            {
                isJumping = false;
            }
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public void SetIsJumping() => isJumping = true;
}
