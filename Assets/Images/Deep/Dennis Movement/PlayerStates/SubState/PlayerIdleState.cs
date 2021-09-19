using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    bool isAttack;
    float lastTimeAttack ;

    public PlayerIdleState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, int hashID) : base(player, stateMachine, playerData, hashID)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        player.JumpState.ResetAmountOfJumpsLeft();
        isAttack = false;
    }


    public override void Exit()
    {
        base.Exit();
    }


    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if  (player.InputHandler.IsAvailableToRun() && ((xInput > 0 && core.SetMovement.FacingDirection> 0) || (xInput < 0 && core.SetMovement.FacingDirection< 0))){
            // BUG : Attack 3 can spam  
            // to correct by disable IsAvailableToRun() when we already run
            stateMachine.ChangeState(player.RunState);
        }
        else if (IsMove){
            stateMachine.ChangeState(player.MoveState);
        }
        
        else if (JumpInput){
            player.InputHandler.UseJumpInput();
            core.SetMovement.SetVelocityY(playerData.jumpVelocity ,moveDir);
            stateMachine.ChangeState(player.JumpState);
        }
        else if (player.InputHandler.AttackInput && !isAttack ){
            // Error : Player stuck in State Attack , but animation is IDLE  
            stateMachine.ChangeState(player.AttackState12 , AttackType.Attack1);
            lastTimeAttack = Time.deltaTime;
        }
        else if (player.InputHandler.DefenseInput){
            stateMachine.ChangeState(player.DefenseState);
        }


    }



    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }


}
