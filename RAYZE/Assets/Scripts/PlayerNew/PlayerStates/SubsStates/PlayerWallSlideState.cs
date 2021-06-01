﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerTouchingWallState
{
    public PlayerWallSlideState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }


    public override void LogicUpdate()
    {
        base.LogicUpdate();
        player.WallDust();
        player.SetVelocityY(-playerData.wallSlideVelocity);
        
            if (grabInput && !isExitingState && isTouchingClimbWall)
            {
                
                stateMachine.ChangeState(player.WallGrabState);
            }
    }
}
