using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityState : PlayerState
{
    protected bool isAbilityDone;
    public PlayerAbilityState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, int hashID) : base(player, stateMachine, playerData, hashID)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
 
    }

    public override void Enter()
    {
        base.Enter();
        isAbilityDone = false;
    }


    public override void Exit()
    {
        base.Exit();
    }


    public override void LogicUpdate()
    {
        base.LogicUpdate();
        float currentVelocity = player.Rigidbody.velocity.y;
        if (isAbilityDone){
            if(player.CheckrGounded() && currentVelocity < 0.01f){
                stateMachine.ChangeState(player.IdleState);
            }else{
                stateMachine.ChangeState(player.AirState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}
