using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class Entity : MonoBehaviour,IDamageable
{
    #region Components
         
    public FiniteStateMachine stateMachine;

    public Rigidbody Rigidbody{get ; private set;}
    public Animator Animator{get; private set;}
    #region Transforms
         
    public Transform PlayerTransform;
    
    public Transform AttackPosition;

    #endregion

    public NavMeshAgent agentNavMesh{get ; private set;}

    [SerializeField] public PlayerData Ennemydata;

    #endregion

    #region AI State 
    public EIdleState EIdleState {get ; private set;}
    public EMoveState EMoveState {get ; private set;}
    public EAttackState EAttackState{get ; private set;}
    public EHurtState EHurtState{get ; private set;}
    public EDeadState EDeadState{get ; private set;}

     
    #endregion

    #region Animation Name
    
    int Idle = Animator.StringToHash("Idle_anim");
    int Walk = Animator.StringToHash("Walk_anim");

    int Run = Animator.StringToHash("Run_anim");

    int Jump = Animator.StringToHash("Jump_anim");
    int Sliding = Animator.StringToHash("Sliding_anim");

    int Hurt1 = Animator.StringToHash("Hurt1_anim");
    int FallContre = Animator.StringToHash("Fall_anim");
    int FallPour = Animator.StringToHash("Fall2_anim");

    private int attack1=  Animator.StringToHash("Attack1_anim") ;
    public int Attack1 {
        get {return attack1;}
    }
    private int attack2=  Animator.StringToHash("Attack2_anim") ;
    public int Attack2 {
        get {return attack2;}
    }
    private int attack3=  Animator.StringToHash("Attack3_anim") ;
    public int Attack3 {
        get {return attack3;}
    }

         
    #endregion

    #region Other Variables
    private Vector3 velocityWorkSpace;
    public int FacingDirection{get ; private set;}
    [SerializeField]private bool DebugEnemy ;

    private float healthMAX ;
    private float currentHealth ;

    #endregion

    public event Action<float> ModifierHealth;
    private void Awake() {
        Rigidbody = GetComponent<Rigidbody>();
        Animator = GetComponent<Animator>();
        stateMachine = new FiniteStateMachine();
        agentNavMesh = GetComponent<NavMeshAgent>();        

    }
    public virtual void  Start() {
    
        if (PlayerTransform == null) PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        EIdleState = new EIdleState(this , stateMachine , Idle); 
        EMoveState= new EMoveState(this , stateMachine, Walk);
        EAttackState = new EAttackState(this , stateMachine, Attack1);
        EHurtState = new EHurtState(this , stateMachine,Hurt1);
        EDeadState = new EDeadState(this , stateMachine,FallContre);
        FacingDirection = 1;
        stateMachine.Initalize(EMoveState);

        healthMAX = Ennemydata.healthMAX;
        currentHealth = healthMAX;
        
    }
    public virtual void Update() {
        stateMachine.currentState.LogicUpdate();
        // Debug.Log(stateMachine.currentState);

    }

    public virtual void FixedUpdate() {
        stateMachine.currentState.PhysicsUpdate();   
    }

    public virtual void SetVelocityXZ (Vector3 velocityXZ , float speedx,float speedz){
        velocityWorkSpace.Set(velocityXZ.x*speedx,0,velocityXZ.z*speedz);
        Rigidbody.velocity = velocityXZ;
    }
    private void AnimationFinishTrigger() => stateMachine.currentState.AnimationFinishTrigger();

    void IDamageable.ReceiveHP(int HP) {
        currentHealth -= HP;
        float percentChange = (currentHealth /healthMAX);
        Debug.Log(percentChange);
        ModifierHealth?.Invoke(percentChange);
         
        if (currentHealth < 0f){
            Die();
            return;
        }
        stateMachine.ChangeState(EHurtState);
    }

    private void Die()
    {
        stateMachine.ChangeState(EDeadState);
    }

    private void AnimationTriggerAttack()
    {
        
        Collider[] HitDected = Physics.OverlapSphere(AttackPosition.position,0.5f,LayerMask.GetMask("Player"));
        // LayerMask.GetMask("")
        foreach (Collider item in HitDected)
        {
            IDamageable damageable = item.GetComponent<IDamageable>();
            // item.GetComponent<IDamageable>();
            // Debug.Log(item);
            if (damageable != null)
            {
                damageable.ReceiveHP(10);
            }
        }
        
    }

    public void CheckIfShouldFlip(float xInput){
        if (xInput != 0 && xInput != FacingDirection){
            Flip();
        }
    }
    public void Flip(){
        FacingDirection *=-1;
        transform.Rotate(0.0f,180.0f,0.0f);
    }


    private void OnDrawGizmos() {
        if (DebugEnemy){
            Gizmos.DrawSphere(AttackPosition.position,0.5f);
            // Gizmos.DrawCube(boxCollider.bounds.center,boxCollider.bounds.extents);
        }
    }



}

