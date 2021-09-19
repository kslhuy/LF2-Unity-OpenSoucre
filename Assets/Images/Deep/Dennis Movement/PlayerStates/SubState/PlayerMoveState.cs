using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, int hashID) : base(player, stateMachine, playerData, hashID)
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
        core.SetMovement.CheckIfShouldFlip(xInput);
        if (!IsMove){
            stateMachine.ChangeState(player.IdleState);
        }
        else if (JumpInput){
            player.InputHandler.UseJumpInput();
            core.SetMovement.SetVelocityY(playerData.jumpVelocity ,moveDir);
            stateMachine.ChangeState(player.JumpState);
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        core.SetMovement.SetVelocityXZ(playerData.movementVelocityX * xInput, playerData.movementVelocityZ * zInput);
    }


}
