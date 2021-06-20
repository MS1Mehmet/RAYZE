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

        //Checken ob Spieler sich dreht
        player.CheckIfShouldFlip(xInput);

        if (!isExitingState)
        {
            //Switcht zur IdleState
            if (xInput == 0f)
            {
                stateMachine.ChangeState(player.IdleState);
            }
            //Hauptfunktion in der MoveState
            else
            {
                //Spielergeschwindigkeit
                player.SetVelocityX(playerData.movementVelocity * xInput);
                player.Anim.SetFloat("yInput", yInput);
                //Schießenabfrage
                if (attackInput)
                {
                    if (yInput == 1)
                    {
                        player.Shoot(true);
                    }
                    else if (!anispamstop)
                    {
                        player.Anim.SetFloat("attackInput", 1f);
                        player.Shoot(false);
                    }
                }
                else
                {
                    player.Anim.SetFloat("attackInput", 0f);
                    anispamstop = player.coolDownActiv;
                }
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
