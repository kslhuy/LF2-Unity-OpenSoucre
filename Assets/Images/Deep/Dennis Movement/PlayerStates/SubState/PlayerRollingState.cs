using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRollingState : PlayerGroundedState
{
    float rollingSpeed;
    float distanceRolling;
    private float distanceRollingTravelled;

    public PlayerRollingState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, int hashID) : base(player, stateMachine, playerData, hashID)
    {
        rollingSpeed = playerData.rollingVelocity;
        distanceRolling = playerData.distanceRolling;
    }
    public override void Enter()
    {
        base.Enter();
    }

    public override void LogicUpdate()
    {
        
        base.LogicUpdate();
        core.SetMovement.SetVelocityRolling(rollingSpeed);
        distanceRollingTravelled += rollingSpeed * Time.deltaTime;
        if (distanceRollingTravelled > distanceRolling){
            distanceRollingTravelled = 0;
            stateMachine.ChangeState(player.IdleState);
        }
    }
}
