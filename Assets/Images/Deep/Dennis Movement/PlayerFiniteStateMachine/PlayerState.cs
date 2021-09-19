using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState 
{
    protected Core core;
    protected Player player;
    protected PlayerStateMachine stateMachine;
    protected PlayerData playerData;
    public AttackType attackType;

    protected bool isAnimationFinished;

    protected float startTime;
    private int HashIDanimName;


    public PlayerState(Player player , PlayerStateMachine stateMachine , PlayerData playerData ,  int hashID){
        this.player = player;
        this.stateMachine = stateMachine;
        this.playerData = playerData;
        this.HashIDanimName = hashID;
        core = player.Core;

    }

    public virtual void Enter(){
        DoChecks();
        player.Anim.Play(HashIDanimName);
        startTime = Time.time;
        isAnimationFinished = false;
        
    }

    public virtual void Exit(){}


    public virtual void LogicUpdate() {
        
    }

    public virtual void PhysicsUpdate(){}

    public virtual void DoChecks(){}
    public virtual void AnimationTrigger(){}

    public virtual void AnimationFinishTrigger() => isAnimationFinished = true;

    protected bool isFinishedAnimation(){
        return player.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1;}

}
