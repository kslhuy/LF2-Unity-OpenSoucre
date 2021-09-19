using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EAttackState : State
{
    public EAttackState(Entity entity, FiniteStateMachine stateMachine, int animationName) : base(entity, stateMachine, animationName)
    {
    }

    public override void Enter()
    {
        // base.Enter();
        startTime = Time.time;
        entity.Animator.Play(animationID);
    }

    public override void Exit()
    {
        base.Exit();

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isAnimationFinished || Time.time > startTime + 0.3f){
            stateMachine.ChangeState(entity.EIdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}
