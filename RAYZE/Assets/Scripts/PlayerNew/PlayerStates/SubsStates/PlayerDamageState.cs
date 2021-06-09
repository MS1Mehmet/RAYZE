using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageState : PlayerState
{
    private int xInput;
    private int yInput;
    private bool isGrounded;

    public PlayerDamageState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isGrounded = player.CheckIfGrounded();
    }

    public override void Enter()
    {
        base.Enter();
        if (!player.isDeath && player.isHit)
        {
            // Hier Enemy Damage übergeben
            playerData.playerCurrentHealth -= 10;
            player.SetVelocity(playerData.hitVelocity, playerData.hitAngle, player.FacingDirection * -1);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (playerData.playerCurrentHealth <= 0)
        {
            stateMachine.ChangeState(player.DeathState);
        }
        else if (!player.isHit)
        {
            if (isGrounded && player.CurrentVelocity.y < 0.01f)
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
            else if (!isGrounded)
            {
                stateMachine.ChangeState(player.InAirState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
