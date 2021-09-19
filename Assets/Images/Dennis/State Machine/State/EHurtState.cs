using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EHurtState : State
{
    private float durationHurt = 0.5f;

    public EHurtState(Entity entity, FiniteStateMachine stateMachine, int animationName) : base(entity, stateMachine, animationName)
    {
    }


    public override void Enter()
    {
        base.Enter();

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Time.time > startTime + durationHurt){
            stateMachine.ChangeState(entity.EMoveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }


}
