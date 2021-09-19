using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDoubleJumpState : PlayerState
{
    private int xInput;

    public PlayerDoubleJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, int hashID) : base(player, stateMachine, playerData, hashID)
    {
    }

    private bool AttackInput ;

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        core.SetMovement.SetVolocityDoubleJump(playerData.DoublejumpVelocity);
        player.JumpState.DecreaseAmountOfJumpsLeft();
    }


    public override void Exit()
    {
        base.Exit();
    }


    public override void LogicUpdate()
    {
        base.LogicUpdate();
        xInput = player.InputHandler.NormInputX;
        AttackInput = player.InputHandler.AttackInput;
        if (player.CheckrGounded() && Mathf.Abs(player.currentVelocity.y) < 0.01f){
            stateMachine.ChangeState(player.LandState);
        }
        else if (AttackInput)
        {
            stateMachine.ChangeState(player.AttackState12 , AttackType.Attack4);
        }
        else{
            core.SetMovement.SetFallingDown();
            if (xInput != 0 && xInput !=core.SetMovement.FacingDirection){
                player.Anim.Play("DoubleJump2_Deep_anim");
                core.SetMovement.Flip();
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override string ToString()
    {
        return base.ToString();
    }
}