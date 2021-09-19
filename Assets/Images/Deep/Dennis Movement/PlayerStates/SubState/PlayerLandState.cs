using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLandState : PlayerGroundedState
{
    public PlayerLandState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, int hashID) : base(player, stateMachine, playerData, hashID)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.InputHandler.ResetRun();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (JumpInput && player.JumpState.CanJump()){
            
            stateMachine.ChangeState(player.DoubleJumpState);
        }
        else if (DefenseInput)
        {
            stateMachine.ChangeState(player.RollingState);
        }
        else if(isAnimationFinished) {
            stateMachine.ChangeState(player.IdleState);
        }
    }


}
