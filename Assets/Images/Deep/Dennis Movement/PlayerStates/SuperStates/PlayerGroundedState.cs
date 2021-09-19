using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected int xInput;
    protected int zInput;

    protected Vector3 moveDir;

    protected bool IsMove;
    protected bool JumpInput;

    protected bool DefenseInput {get ; private set;}

    protected bool AttackInput { get; private set; }

    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, int hashID) : base(player, stateMachine, playerData, hashID)
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
        xInput = player.InputHandler.NormInputX;
        zInput = player.InputHandler.NormInputZ;
        moveDir = new Vector3(xInput , 0 , zInput);
        IsMove = xInput != 0 || zInput != 0;
        JumpInput = player.InputHandler.JumpInput;
        DefenseInput = player.InputHandler.DefenseInput;
        AttackInput = player.InputHandler.AttackInput;

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }


}
