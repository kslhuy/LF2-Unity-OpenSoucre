using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerState
{
    private bool isGrounded;
    private int xInput;
    private int zInput;

    private bool AttackInput;
    public PlayerAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, int hashID) : base(player, stateMachine, playerData, hashID)
    {
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
        AttackInput = player.InputHandler.AttackInput;
        xInput = player.InputHandler.NormInputX;
        if (player.CheckrGounded() && Mathf.Abs(player.currentVelocity.y) < 0.01f){
            stateMachine.ChangeState(player.LandState);
        }
        else if (AttackInput)
        {
            // Can cai thien 
            // Co the de AttackState 5 lam child AirState , for better Jump
            stateMachine.ChangeState(player.AttackState12 , AttackType.Attack5);
        }
        else{
            core.SetMovement.SetFallingDown();
            core.SetMovement.CheckIfShouldFlip(xInput);

        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}
