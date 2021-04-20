using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class PlayerInAirState : PlayerState
{

    private bool isGrounded;
    private int xInput;
    private bool jumpInput;
    private bool jumpInputStop;
    private bool isJumping;
    private bool isTouchingWall;
    private bool isTouchingWallBack;
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
        jumpInput = player.InputHandler.JumpInput;
        jumpInputStop = player.InputHandler.JumpInputStop;
        grabInput = player.InputHandler.GrabInput;

        checkJumpMultiplier();

        if (isGrounded && player.CurrentVelocity.y < 0.01f)
        {
            // Wenn LandeAnimation vorhanden dann statt IdleState zu LandState
            //stateMachine.ChangeState(player.IdleState);
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
        else if (jumpInput && player.JumpState.CanJump())
        {
            player.InputHandler.UseJumpInput();
            stateMachine.ChangeState(player.JumpState);
        }
        else if (isTouchingWall && grabInput)
        {
            stateMachine.ChangeState(player.WallGrabState);
        }
        else if (isTouchingWall && xInput == player.FacingDirection && player.CurrentVelocity.y <= 0)
        {
            stateMachine.ChangeState(player.WallSlideState);
        }
        else
        {
            player.CheckIfShouldFlip(xInput);
            player.SetVelocityX(playerData.movementVelocity * xInput);

            player.Anim.SetFloat("yVelocity", player.CurrentVelocity.y);
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
