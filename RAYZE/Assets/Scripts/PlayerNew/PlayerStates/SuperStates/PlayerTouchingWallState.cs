using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouchingWallState : PlayerState
{
    protected bool isGrounded;
    protected bool isPlattform;
    protected bool isTouchingWall;
    protected bool isTouchingClimbWall;
    protected int xInput;
    protected int yInput;
    protected bool grabInput;

    public PlayerTouchingWallState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isGrounded = player.CheckIfGrounded();
        isPlattform = player.CheckIfPlattform();
        isTouchingWall = player.CheckIfTouchingWall();
        isTouchingClimbWall = player.CheckIfTouchingClimbWall();
    }

    public override void Enter()
    {
        base.Enter();
        player.SetZeroFriction();
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
        grabInput = player.InputHandler.GrabInput;

        if (player.isHit)
        {
            stateMachine.ChangeState(player.DamageState);
        }
        else if ((isGrounded || isPlattform) && !grabInput)
        {
            stateMachine.ChangeState(player.IdleState);
        }
        else if ((!isTouchingWall && !isTouchingClimbWall) || (xInput != player.FacingDirection && !grabInput))
        {
            stateMachine.ChangeState(player.InAirState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
