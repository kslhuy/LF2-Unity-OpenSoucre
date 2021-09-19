using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EMoveState : State
{

    private float timer = 0.0f;
    private float maxDistance;
    private float maxTime;
    float speedx;
    float speedz;

    public EMoveState(Entity entity, FiniteStateMachine stateMachine, int animationName) : base(entity, stateMachine, animationName)
    {
        speedx = entity.Ennemydata.movementVelocityX;
        speedz = entity.Ennemydata.movementVelocityZ;
        maxDistance = entity.Ennemydata.maxDistance;
        maxTime =  entity.Ennemydata.maxTime;
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
        // ChasePlayerNavMesh();
        // AttackPlayer();
        
        if (Vector3.Distance(entity.transform.position, entity.PlayerTransform.position) < maxDistance)
        {
            // to close : Stop and Attack or do something 
            stateMachine.ChangeState(entity.EAttackState);

        }else
        {
            Vector3 targetDir = (entity.PlayerTransform.position - entity.transform.position ).normalized;
            // Too  far : continue
            entity.SetVelocityXZ(targetDir, speedx, speedz);
            entity.CheckIfShouldFlip(Mathf.Sign(targetDir.x));
        }

    }

    private void AttackPlayer()
    {
        float AttackRange = 0.5f;
        if (Vector3.Distance(entity.transform.position, entity.PlayerTransform.position) < AttackRange)
        {
            stateMachine.ChangeState(entity.EAttackState);
        }

    }

    private void ChasePlayerNavMesh()
    {
        if (!entity.agentNavMesh.enabled)
        {
            return;
        }

        timer -= Time.deltaTime;
        if (!entity.agentNavMesh.hasPath)
        {
            entity.agentNavMesh.destination = entity.PlayerTransform.position;
        }

        if (timer < 0.0f)
        {
            float sqdistance = (entity.PlayerTransform.position - entity.agentNavMesh.destination).sqrMagnitude;
            if (sqdistance > maxDistance * maxDistance)
            {
                entity.agentNavMesh.destination = entity.PlayerTransform.position;
                
                entity.CheckIfShouldFlip(entity.agentNavMesh.desiredVelocity.x);
            }
            timer = maxTime;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }


}
