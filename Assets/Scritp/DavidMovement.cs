using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DavidMovement : MonoBehaviour{
    Rigidbody rigidboby;
    Animator anim;
    SpriteRenderer spirterender;

    [SerializeField] private GameObject HitBox ;

    [SerializeField] Text controlsTestText; //Just for testing for printing the keys
    [SerializeField] List<KeyCode> KeysPressed; //List of all the Keys Pressed so far
    [SerializeField] List<Move> avilableSkills; //All the Avilable Moves

    /*Check all button pressed  */
    private bool isButtonMovePressed;
    // private bool isButtonAttackPressed;
    // private bool isButtonJumpPressed;
    private bool isButtonDefensePressed;
    private float ComboResetTime = 0.4f;
    
    /*Check all button pressed  */

    float distance_defDownA = 1f;
    // float moveSpeed = 2.5f;
    // bool isHurting , isDead;
    private Vector3 LastMove;
    bool facingRight = true;

    float walkSpeed = 0.5f;
    private BoxCollider boxCollider;
    [SerializeField] private LayerMask GoundMask;
    [SerializeField] private float jumpVelocity = 5f;
    [SerializeField] private float attackDelay = 0.33f;
    [SerializeField] private float jumpDelay = 0.3f;

    private bool isIdle; 
    
    private bool IsAttacking;
    private bool IsJumpping = false;
    // Direction taken from input action for Player movement
    private Vector3 moveDir;  
    private int bitmask = 1 << 7;
    private Vector3 AttackDir;

    
    private State state;

    private enum State{
        Idle,
        Move,
        Attack,
        Jump,
        Defense,


    }

    private void Awake()
    {
        // animationBase = GetComponent<Animation_Base>();
        rigidboby = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        anim = GetComponent<Animator>();
        spirterender = GetComponent<SpriteRenderer>(); 
        // movesManager = GetComponent<MovesManager>();
        SetStateIdle();
        
    }
    private void Start() {
        // Physics.IgnoreLayerCollision(7,7);

        // rigidboby = GetComponent<Rigidbody>();
        // boxCollider = GetComponent<BoxCollider>();
        // anim = GetComponent<Animator>();
        // spirterender = GetComponent<SpriteRenderer>(); 
        // // movesManager = GetComponent<MovesManager>();
        // SetStateIdle();
    }

    // Update is called once per frame
    void Update()
    {
        // CheckMovement();
    
        switch (state){
            case State.Idle:
                HandleMovement();
                HandleAttack();
                HanldeDefense();
                // HanldeShotDavidBall();
                break;
            case State.Move:
                HandleMovement();
                HandleAttack();
                break;
            case State.Attack:
                HandleAttack();
                break;
            case State.Jump:
                CheckMovement();
                HandleAttack(); 
                break;

            
        }

        Debug.Log(state);
    }


    private void SetStateIdle(){
        state = State.Idle ;
        IsJumpping = false;        
    }
    private void SetStatePunch(){
        state = State.Attack ; 

    }
    private void SetStateMove(){
        state = State.Move ;
    }
    private void SetStateJump(){
        state = State.Jump ;
    }
    private void SetStateDefense(){
        state = State.Defense ;
        IsAttacking = false;
        IsJumpping = false;  
    }


    private bool IsGround(){
        float extraHeightTest = 0.1f;

        bool hit_ground = Physics.Raycast(transform.position , Vector3.down,boxCollider.bounds.extents.y+extraHeightTest,GoundMask);
        // Color rayColor;
        // if (!hit_ground){
        //     rayColor = Color.green;
        // }else {
        //     rayColor = Color.red;
        // }
        // Debug.DrawRay(boxCollider.bounds.center , Vector3.down * (boxCollider.bounds.extents.y+extraHeightTest),rayColor);
        // Debug.Log(hit_ground);
        return hit_ground;
    }
    // private bool IsHit_Something(){
    //     float extraHeightTest = 0.1f;
    //     RaycastHit hit_Somthing;
    //     bool hit_something = Physics.Raycast(transform.position , AttackDir,out hit_Somthing,boxCollider.bounds.extents.x+extraHeightTest,bitmask);
    //     Color rayColor;
    //     if (!hit_something){
    //         rayColor = Color.green;
    //     }else {
    //         Debug.Log(hit_Somthing.transform.name);
    //         rayColor = Color.red;
    //     }
    //     Debug.DrawRay(boxCollider.bounds.center , AttackDir * (boxCollider.bounds.extents.x+extraHeightTest),rayColor);
    //     return hit_something;
    // }

    private void CheckMovement(){
        float moveZ = 0f;
        float moveX = 0f;
        if (Input.GetKey(KeyCode.Z))
        {
            moveZ = -1f;
        }
        if (Input.GetKey(KeyCode.S)){
            moveZ = +1f;
        }
        if (Input.GetKey(KeyCode.D)){
            moveX = 1f;
            transform.localScale = new Vector3(0.9f,1,1);
        }
        if (Input.GetKey(KeyCode.Q)){
            moveX = -1f;
            transform.localScale = new Vector3(-0.9f,1,1);
        }
        isIdle = moveZ == 0 && moveX == 0;
        Vector3 velocity = new Vector3(0,rigidboby.velocity.y,0);
        moveDir = new Vector3(moveX , 0 , moveZ).normalized + velocity;
        

    }

    private void HandleMovement(){
        CheckMovement();
        if ( !isIdle){
            SetStateMove();
            anim.Play("walk_david_anim");
        }
        else if ( isIdle)  {
            SetStateIdle();
            anim.Play("idel_david_anim");
        }
    }

    private void HandleAttack(){
        if (Input.GetKeyDown(KeyCode.Y) ){
            // int punch1 = UnityEngine.Random.Range(1,3);
            SetStatePunch();
            if (IsGround()){
                if (isPlayingPunch1Animation()){
                    anim.Play("punch2_david_anim");
                    // Invoke("SetStateIdle", attackDelay);
                    // StopAnimation();
                }else{
                    anim.Play("punch1_david_anim");
                    // Invoke("SetStateIdle", attackDelay);
                    // StopAnimation();
                }
            // On air 
            }else if (IsJumpping && !IsGround())
            {
                // float JumpKickDelay = 0.5f;
                anim.Play("jumpkick_david_anim");
                // Invoke("SetStateIdle", JumpKickDelay);
                // StopAnimation();
            }
            StartCoroutine(DoAttack());
            
        }
        
    }

    private void HandleJump(){
        if (IsGround() && Input.GetKeyDown(KeyCode.U) && !IsJumpping){
            SetStateJump();
            IsJumpping = true;
            anim.Play("jump_david_anim");
            rigidboby.velocity = Vector3.up* jumpVelocity + moveDir;
            // Jump one 
            
            // Invoke("Jump" , 0.2f);
            // Invoke("SetStateIdle", jumpDelay);
            
        }
    }
    private void HanldeDefense(){
        if (IsGround() && Input.GetKeyDown(KeyCode.I)){
            isButtonDefensePressed = true;
            SetStateDefense();
            anim.Play("def_david_anim");
            // float defDelay = 0.2f;
            // Invoke("SetStateIdle", defDelay);
            // StopAnimation();
        }
    }
    // private void HanldeShotDavidBall(){

    // }


    private bool isPlayingPunch1Animation(){
        return anim.GetCurrentAnimatorStateInfo(0).IsName("punch1_david_anim");
    }
    private bool isPlayingPunch2Animation(){
        return anim.GetCurrentAnimatorStateInfo(0).IsName("punch2_david_anim");
    }

    private IEnumerator DoAttack(){
        HitBox.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        HitBox.SetActive(false);
    }
    private void FixedUpdate() {
        Vector3 velocity_target;
        switch (state){
            case State.Idle:
                HandleJump();
                break;
            case State.Move:
                velocity_target = moveDir;
                rigidboby.velocity = moveDir;
                HandleJump();
                break;
            case State.Jump:
                break;
        }
    }
    public void StopAnimation(){
        SetStateIdle();
    }


    void Flip (){
        if (LastMove.x > 0 && !facingRight){
            facingRight = !facingRight;
            spirterender.flipX = false;
        }else if (LastMove.x < 0 && facingRight) {
            spirterender.flipX = true;
            facingRight = !facingRight;
        }
        
    }
}
    


