using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour , IDamageable
{
    #region State Variables
    public PlayerStateMachine StateMachine{ get; private set;}
    public PlayerIdleState IdleState{get; private set;}
    public PlayerMoveState MoveState{get; private set;}

    public PlayerJumpState JumpState{get ; private set;}


    public PlayerLandState LandState{get;private set;}
    public PlayerAirState AirState{get; private set;}

    public PlayerDoubleJumpState DoubleJumpState{get ; private set;}

    public PlayerRunState RunState{get; private set;}
    public RunSliding SlidingState{get; private set;}

    public PlayerAttack12 AttackState12{get;private set;}



    public PlayerDefenseState DefenseState{get;private set;}

    public PlayerRollingState RollingState {get; private set;}

    public PlayerHurtState HurtState {get; private set;} 

    public DLAstate DLAstate {get; private set;}
    public DDAstate DDAstate{get; private set;}
    public DUAstate DUAstate{get; private set;}

    public PlayerComboState ComboState{get; private set;}
         
    #endregion

    #region Components

    public Core Core {get; private set;}
    public PlayerInputHandler InputHandler{get;private set;}
    public Animator Anim {get ; private set;}
    public Rigidbody Rigidbody{ get; private set;}
    public BoxCollider boxCollider{get;private set;}
         
    #endregion
    
    #region Check Transforms
    public Transform AttackTransform;
    #endregion

    #region Other Variables
        
    [SerializeField] private PlayerData playerData;
    private Vector3 workSpace;
    public Vector3 currentVelocity{get; private set;}

 
    #endregion


    #region AnimationToHash

    int Idle = Animator.StringToHash("Idle_Deep_anim");
    int Walk = Animator.StringToHash("Walk_Deep_anim");

    int Jump = Animator.StringToHash("Jump_Deep_anim");

    int DoubleJump = Animator.StringToHash("DoubleJump_Deep_anim");
    int DoubleJump2 = Animator.StringToHash("DoubleJump2_Deep_anim");
    int Land = Animator.StringToHash("Land_Deep_anim");

    int Air = Animator.StringToHash("Air_Deep_anim");

    int Run = Animator.StringToHash("Run_Deep_anim");
    int Sliding = Animator.StringToHash("RunSliding_Deep_anim");
    private int attack1=  Animator.StringToHash("Attack1_Deep_anim") ;
    public int Attack1 {
        get {return attack1;}
    }

    int attack2 = Animator.StringToHash("Attack2_Deep_anim") ;
    public int Attack2 {get {return attack2;}}

    int attack3 = Animator.StringToHash("Attack3_Deep_anim") ;
    public int Attack3 {get {return attack3;}}

    int attack4 = Animator.StringToHash("Attack4_Deep_anim") ;
    public int Attack4 {get {return attack4;}}
    int attack5 = Animator.StringToHash("Attack5_Deep_anim") ;
    public int Attack5 {get {return attack5;}}

    

    int Defense = Animator.StringToHash("Defense_Deep_anim") ;

    int Rolling = Animator.StringToHash("Rolling_Deep_anim") ;

    int Hurt1 = Animator.StringToHash("Hurt1_anim") ;

    int DLA = Animator.StringToHash("D_L_A_anim");

    int DUA = Animator.StringToHash("D_U_A_anim");
    int DDA = Animator.StringToHash("D_D_A_anim");
    

             
    #endregion
    
    [SerializeField] private bool DebugPlayer ;
    
    #region Unity CallBack Function
         
    private void Awake() {

        Core = GetComponentInChildren<Core>();

        StateMachine = new PlayerStateMachine();

        IdleState =  new PlayerIdleState(this , StateMachine , playerData,Idle);
        MoveState =  new PlayerMoveState(this , StateMachine , playerData,Walk);
        AirState = new PlayerAirState(this , StateMachine , playerData, Air);
        LandState = new PlayerLandState(this , StateMachine , playerData,Land);
        DoubleJumpState = new PlayerDoubleJumpState(this , StateMachine , playerData,DoubleJump);
        RunState = new PlayerRunState(this , StateMachine,playerData,Run);
        SlidingState = new RunSliding(this , StateMachine,playerData,Sliding);
        RollingState = new PlayerRollingState (this , StateMachine,playerData,Rolling);

        AttackState12 = new PlayerAttack12(this , StateMachine,playerData,Attack1);
        DefenseState =  new PlayerDefenseState (this , StateMachine,playerData,Defense);
        JumpState = new PlayerJumpState(this , StateMachine , playerData, Jump);

        HurtState = new PlayerHurtState(this , StateMachine,playerData,Hurt1);
        
        DLAstate = new DLAstate(this , StateMachine,playerData,DLA);
        DUAstate = new DUAstate(this , StateMachine,playerData,DUA);
        DDAstate = new DDAstate(this , StateMachine,playerData,DDA);

        ComboState = new PlayerComboState(this , StateMachine,playerData,DDA);
        
    }

    private void Start() {
        Anim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        Rigidbody = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        StateMachine.Initialize(IdleState);

        InputHandler.ComboTrigger += HandleComboTrigger;
        
    }

    private void HandleComboTrigger(TypeSkills StateCombo)
    {
        switch (StateCombo){
        case TypeSkills.DefLeftAttack:
            StateMachine.ChangeState(DLAstate);
            break;
        
        case TypeSkills.DefUpAttack:
            StateMachine.ChangeState(DUAstate);
            break;
        
        case TypeSkills.DefDownJump:
            StateMachine.ChangeState(DDAstate);
            break;
        }
        // Debug.Log(StateCombo);

    }

    private void Update() {
        currentVelocity = Rigidbody.velocity;
        StateMachine.CurrentState.LogicUpdate();
        // Debug.Log(StateMachine.CurrentState);
    
    }
    private void FixedUpdate(){
        StateMachine.CurrentState.PhysicsUpdate();
    }
    #endregion
    

    #region CheckFunction
 
    public bool CheckrGounded(){
        return  Physics.Raycast(boxCollider.bounds.center,Vector3.down ,boxCollider.bounds.extents.y,playerData.whatIsGround);
    }    
    #endregion




    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();

    private void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();


    void IDamageable.ReceiveHP(int HP) {
        StateMachine.ChangeState(HurtState);
        // function nay ko co y nghia nhieu 
        // chi muon moi State co the bi nhan DAME khac nhau 
        // StateMachine.CurrentState.ReceiveHP(HP);
        // ModifierHealth?.Invoke(HP);
    }
    private void AnimationTriggerAttack()
    {
        
        // RaycastHit[]HitDectecd = Physics.BoxCastAll(boxCollider.bounds.center,boxCollider.bounds.extents, Vector3.right,Quaternion.identity, playerData.whatIsEnemy) ;

        // foreach (RaycastHit item in HitDectecd)
        // {
        //     IDamageable damageable = item.collider.GetComponent<IDamageable> ();
        //     // item.GetComponent<IDamageable>();
        //     Debug.Log(damageable);
        //     if (damageable != null)
        //     {
        //         damageable.Damage(10f);
        //     }
        // }

        Collider[] HitDected = Physics.OverlapSphere(AttackTransform.position,playerData.attackRadius,playerData.whatIsEnemy);
        // LayerMask.GetMask("")
        foreach (Collider item in HitDected)
        {
            IDamageable damageable = item.GetComponent<IDamageable>();
            // item.GetComponent<IDamageable>();
            Debug.Log(item);
            if (damageable != null)
            {
                damageable.ReceiveHP(10);
            }
        }
        

    }

    private void OnDrawGizmos() {
        if (DebugPlayer){
            Gizmos.DrawSphere(AttackTransform.position,playerData.attackRadius);
            // Gizmos.DrawCube(boxCollider.bounds.center,boxCollider.bounds.extents);
        }
    }

    public void ReceiveHP(int HP)
    {
        Debug.Log("NGU roif");
    }
}
