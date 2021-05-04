﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathState : PlayerState
{
    public PlayerDeathState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Bin in der DeathState");
        player.SetDeath(true);
        player.SetVelocityX(0f);
        player.SetVelocityY(0f);
        
    }

    public override void Exit()
    {
        Debug.Log("Bin in der DeathState raus vor Exit");
        base.Exit();
        player.SetDeath(false);
        playerData.playerHealth = 100;
        Debug.Log("Bin in der DeathState raus nach Exit");
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Time.time >= startTime + playerData.deathTime)
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
