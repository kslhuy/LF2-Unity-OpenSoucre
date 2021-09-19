using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class State 
{
    protected FiniteStateMachine stateMachine;
    protected Entity entity;

    protected float startTime;

    protected int animationID;

    protected bool isAnimationFinished;
    

    public State(Entity entity, FiniteStateMachine stateMachine , int animationName){
        this.entity= entity;
        this.stateMachine = stateMachine;
        this.animationID = animationName;
    }

    public virtual void Enter(){
        startTime = Time.deltaTime;
        entity.Animator.Play(animationID);
    }
    public virtual void LogicUpdate(){}
    
    public virtual void PhysicsUpdate(){}
    
    public virtual void Exit(){}

    public virtual void AnimationFinishTrigger() => isAnimationFinished = true;

    protected bool isFinishedAnimation()  {
        return entity.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1;}


    
}
