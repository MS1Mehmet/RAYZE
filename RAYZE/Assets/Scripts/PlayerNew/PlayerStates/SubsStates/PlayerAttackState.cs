using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerAbilityState
{
    private float damage;
    private float coolDown;
    private GameObject bullet;
    public PlayerAttackState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
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
    }

    public override void Enter()
    {
        base.Enter();
        isAbilityDone = true;
        //GameObject bullet = Instantiate(bulletPrefab, bulletShootPos.position, Quaternion.identity);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public void SetWeapon(float damage, float coolDown)
    {
        this.damage = damage;
        this.coolDown = coolDown;
    }

    public void DefaultWeapon()
    {
        damage = playerData.damage;
        coolDown = playerData.coolDown;
    }
}
