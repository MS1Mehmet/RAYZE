using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        //Spieler anhalten
        player.SetVelocityX(0f);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isExitingState) 
        {
            //Switcht zur MoveState
            if (xInput != 0f)
            {
                stateMachine.ChangeState(player.MoveState);
            }
            //Hauptfunktion in der IdleState
            else
            {
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
