using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunSliding : PlayerGroundedState
{
    private float _runSpeed;
    private float _gainDecreaseRunSpeed;
    public RunSliding(Player player, PlayerStateMachine stateMachine, PlayerData playerData, int hashID) : base(player, stateMachine, playerData, hashID)
    {
        _runSpeed =playerData.runVelocity;
        _gainDecreaseRunSpeed = playerData.GainDecreaseRunSpeed;
    }


    public override void LogicUpdate()
    {
        _runSpeed -= _runSpeed*Time.deltaTime*_gainDecreaseRunSpeed;
        core.SetMovement.SetVelocitySliding(_gainDecreaseRunSpeed , _runSpeed);
        if (_runSpeed <= 0.1f ){
            stateMachine.ChangeState(player.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void Exit()
    {
        base.Exit();
        ResetRunVelocity();
    }

    public void ResetRunVelocity(){
        _runSpeed = playerData.runVelocity;
    }
}
