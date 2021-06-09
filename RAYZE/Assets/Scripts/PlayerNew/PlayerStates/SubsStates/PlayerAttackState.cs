using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XInput;

public class PlayerAttackState : PlayerAbilityState
{
    private int xInput;
    private int yInput;
    public PlayerAttackState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        player.Shoot();
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
        player.CheckIfShouldFlip(xInput);

        if (coolDown())
        {
            player.Shoot();
        }

        if (!attackInput)
        {
            stateMachine.ChangeState(player.IdleState);
        }
        else if (attackInput)
        {
            if (xInput == 0)
            {
                player.SetVelocityX(0f);
                player.Anim.SetFloat("xInput", xInput);
            }
            else if (xInput != 0)
            {
                player.SetVelocityX(playerData.movementVelocity * xInput);
                player.Anim.SetFloat("xInput", 1);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public bool coolDown()
    {
        if (Time.time >= startTime + 0.3f)
        {
            startTime = Time.time + 0.3f;
            return true;
        }
        else
        {
            return false;
        }
    }
}
