using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : PlayerGroundedState
{
    private float runVelocity ;
    public PlayerRunState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, int hashID) : base(player, stateMachine, playerData, hashID)
    {
        runVelocity = playerData.runVelocity;
    }



    public override void Enter()
    {
        base.Enter();
        // ko cho nhay lan thu 2 khi Run
        player.JumpState.DecreaseAmountOfJumpsLeft();
    }


    public override void Exit()
    {
        base.Exit();
    }


    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if ( (xInput > 0 && core.SetMovement.FacingDirection< 0) ||( xInput<0 && core.SetMovement.FacingDirection>0) ){
            stateMachine.ChangeState(player.SlidingState);
        }
        else if (JumpInput){
            stateMachine.ChangeState(player.DoubleJumpState);
        }
        else if (DefenseInput){
            stateMachine.ChangeState(player.RollingState);
        }
        else if (AttackInput)
        {
            stateMachine.ChangeState(player.AttackState12 , AttackType.Attack3);
        }
       
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        core.SetMovement.SetVelocityRun(runVelocity);

    }


}
