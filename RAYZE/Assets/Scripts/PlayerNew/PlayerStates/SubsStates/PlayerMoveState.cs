using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
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

        player.CheckIfShouldFlip(xInput);

        if (!isExitingState)
        {
            if (xInput == 0f)
            {
                stateMachine.ChangeState(player.IdleState);
            }
            else
            {
                player.SetVelocityX(playerData.movementVelocity * xInput);
                if (yInput != 0f)
                {
                    player.Anim.SetFloat("yInput", yInput);
                }
                else
                {
                    player.Anim.SetFloat("yInput", 0f);
                }
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
