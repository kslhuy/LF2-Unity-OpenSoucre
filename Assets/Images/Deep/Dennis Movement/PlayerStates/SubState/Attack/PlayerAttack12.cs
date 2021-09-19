using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack12 : PlayerState
{
    // private List<IDamageable> dectectedDamageable = new List<IDamageable>();
    float attack12distance;
    // Transform attackTransform ;
    public PlayerAttack12(Player player, PlayerStateMachine stateMachine, PlayerData playerData, int hashID) : base(player, stateMachine, playerData, hashID)
    {
        attack12distance = playerData.att12distance;
        // attackTransform = player.AttackTransform;
    }

    public override void Enter()
    {
        isAnimationFinished = false ; 
        if (isAnimationFinished){

        }
        switch (stateMachine.CurrentState.attackType){
            case AttackType.Attack1:
                int Attack = UnityEngine.Random.Range(1, 3);
                if (Attack == 1) player.Anim.Play(player.Attack1,-1,0f);
                else player.Anim.Play(player.Attack2,-1,0f);
                break;
            case AttackType.Attack3:
                player.Anim.Play(player.Attack3);   
                break;
            case AttackType.Attack4:
                player.Anim.Play(player.Attack4);
                break;
            case AttackType.Attack5:
                player.Anim.Play(player.Attack5);
                break;
        }
        

    }   

    public override void PhysicsUpdate()
    {
        switch (stateMachine.CurrentState.attackType)
        {
            case AttackType.Attack1:
                AttackToIdle();
                break;
            case AttackType.Attack3:
                AttackToIdle();
                break;
            case AttackType.Attack4:
                FlyAttackLand();
                break;
            case AttackType.Attack5:
                FlyAttackLand();
                break;
        }
     
    }



    private void AttackToIdle()
    {
        if (isAnimationFinished || isFinishedAnimation())
        {
            isAnimationFinished = false;
            stateMachine.ChangeState(player.IdleState);
        }
    }

    private void FlyAttackLand()
    {
        if (player.CheckrGounded() && Mathf.Abs(player.currentVelocity.y) < 0.01f)
        {
            stateMachine.ChangeState(player.LandState);
        }
    }



}
