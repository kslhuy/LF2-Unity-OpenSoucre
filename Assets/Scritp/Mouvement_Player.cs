using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;

public class Mouvement_Player : NetworkBehaviour
{
    // public event EventHandler OnLanding;

    Rigidbody rigidboby;
    
    SpriteRenderer spirterender;
    float moveSpeed = 1f;
    bool facingRight = true;
    // float moveX;
    // float moveZ;
    float walkSpeed = 0.5f;

    private bool isButtonMovePressed;
    private BoxCollider boxCollider;
    // [SerializeField] private LayerMask GoundMask;
    [SerializeField] private float jumpVelocity = 5f;
    [SerializeField] private float attackDelay = 0.3f;
    [SerializeField] private float jumpDelay = 0.3f;
    private bool isAttacking;
    // Direction taken from input action for Player movement
    private Vector3 moveDir;  
    private Vector3 LastMove;
    private Vector3 AttackDir;


    // Start is called before the first frame update
    private State state;
    private enum State{
        Idle,
        Move,
        Attack,
        Jump,
    }
    private void Awake()
    {
        spirterender = GetComponentInChildren<SpriteRenderer>(); 
        rigidboby = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        SetStateIdle();
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner) return; 

        switch (state){
            case State.Idle:
                HandleMovement();
                HandleAttack();
                HandleJump();
                break;
            case State.Attack:
                HandleAttack();
                break;
            case State.Jump:
                HandleAttack();
                break;
            case State.Move:
                HandleMovement();
                break;
        }
    
        // Animation 
    }
    private void SetStatePunch(){
        state = State.Attack ;
    }
    private void SetStateIdle(){
        state = State.Idle ;
        isAttacking = false;
    }

    private void SetStateJump(){
        state = State.Jump ;
    }

    // private bool IsGround(){
    //     float extraHeightTest = 0.5f;
    //     // Color rayColor;
    //     return Physics.Raycast(boxCollider.bounds.center , Vector3.down,boxCollider.bounds.extents.y+extraHeightTest);
    //     // if (!hit_ground){
    //     //     rayColor = Color.green;
    //     // }else {
    //     //     rayColor = Color.red;
    //     // }
    //     // Debug.DrawRay(boxCollider.bounds.center , Vector3.down * (boxCollider.bounds.extents.y+extraHeightTest),rayColor);
    //     // Debug.Log(hit_ground);
    // }

    private void HandleMovement(){
        CheckMovement();
        if (isButtonMovePressed){
            state = State.Move;
            // anim.Play("walk_david_anim");
        }
        else if ( !isButtonMovePressed)  {
            state = State.Idle;
            // anim.Play("idel_david_anim");
        }
    }
    private void CheckMovement(){
        float moveZ = 0f;
        float moveX = 0f;
        if (Input.GetKey(KeyCode.Z)){
            moveZ = -1f;
        }
        if (Input.GetKey(KeyCode.S)){
            moveZ = +1f;
        }
        if (Input.GetKey(KeyCode.D)){
            moveX = 1f;
            // transform.localScale = new Vector3(-0.85f,1,1);
            // facingRight = true;
        }
        if (Input.GetKey(KeyCode.Q)){
            moveX = -1f;
            // transform.localScale = new Vector3(-0.85f,1,1);
            // facingRight = false;
        }
        // Sauf Run state not flip 
        Flip();
        isButtonMovePressed = moveZ != 0 || moveX != 0;
        // Vector3 velocity = new Vector3(0,rigidboby.velocity.y,0);
        moveDir = new Vector3(moveX , 0 , moveZ).normalized  ;
        if (isButtonMovePressed){
            LastMove = new Vector3 (moveX ,0 , moveZ);
            
        }
        if (moveX != 0 ) {
            AttackDir = new Vector3 (moveX ,0 , 0);
        }


    }


    private void FixedUpdate() {
        switch (state){
            case State.Idle:
                HandleJump();
                break;
            case State.Move:
                // velocity_target = moveDir;
                // rigidboby.velocity = moveDir;
                transform.position += moveDir * moveSpeed * Time.deltaTime ;
                HandleJump();
                break;
        }
        
    }
    private void HandleAttack(){
        if (Input.GetKey(KeyCode.Y)){
            
            SetStatePunch();
            if (!isAttacking)
            {
                isAttacking = true;
                // anim.Play("punch1_david_anim");
                // Invoke("SetStateIdle", attackDelay);
                // if (isPlayingPunch1Animation()){
                    // anim.Play("punch2_david_anim");
                Invoke("SetStateIdle", attackDelay);
        
                // }else{
                    // anim.Play("punch1_david_anim");
                    // Invoke("SetStateIdle", attackDelay);
                // }
            }
            // anim.Play("punch1_david_anim");
                
        }
    }

    // private bool isPlayingPunch1Animation(){
    //     return anim.GetCurrentAnimatorStateInfo(0).IsName("punch1_david_anim");
    // }
    // private bool isPlayingPunch2Animation(){
    //     return anim.GetCurrentAnimatorStateInfo(0).IsName("punch2_david_anim");
    // }

    private void HandleJump(){
        if ( Input.GetKey(KeyCode.U)){
            SetStateJump();
            // anim.Play("jump_david_anim");
            // Invoke("Jump" , 0.2f);
            rigidboby.velocity = Vector3.up* jumpVelocity + moveDir;
            Invoke("SetStateIdle", jumpDelay);
        }
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
