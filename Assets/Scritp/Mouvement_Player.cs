using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouvement_Player : MonoBehaviour
{
    // public event EventHandler OnLanding;

    private Animation_Base animationBase;
    Rigidbody rigidboby;
    Animator anim;

    float moveSpeed = 2.5f;
    bool isHurting , isDead;
    // bool facingRight = true;
    // float moveX;
    // float moveZ;
    float walkSpeed = 0.5f;
    private BoxCollider boxCollider;
    // [SerializeField] private LayerMask GoundMask;
    [SerializeField] private float jumpVelocity = 5f;
    [SerializeField] private float attackDelay = 0.3f;
    [SerializeField] private float jumpDelay = 0.3f;
    private bool isAttacking;
    // Direction taken from input action for Player movement
    private Vector3 moveDir;  
    Vector3 localScale;
    // Start is called before the first frame update
    private State state;
    private enum State{
        Idle,
        Attack,
        Jump,
    }
    private void Awake()
    {
        animationBase = GetComponent<Animation_Base>();
        rigidboby = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        anim = GetComponent<Animator>();
        
        SetStateIdle();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state){
            case State.Idle:
                HandleMovement();
                HandleAttack();
                HandleJump();
                HanldeShotDavidBall();
                break;
            case State.Attack:
                HandleAttack();
                break;
            case State.Jump:
                HandleAttack();
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

    private bool IsGround(){
        float extraHeightTest = 0.5f;
        // Color rayColor;
        return Physics.Raycast(boxCollider.bounds.center , Vector3.down,boxCollider.bounds.extents.y+extraHeightTest);
        // if (!hit_ground){
        //     rayColor = Color.green;
        // }else {
        //     rayColor = Color.red;
        // }
        // Debug.DrawRay(boxCollider.bounds.center , Vector3.down * (boxCollider.bounds.extents.y+extraHeightTest),rayColor);
        // Debug.Log(hit_ground);
    }
    private void HandleMovement(){
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
        bool isIdle = moveZ == 0 && moveX == 0;
        if (!isIdle && IsGround()){
            animationBase.PlayWalkingAnimation();
        }else{
            animationBase.PlayIdelAnimation();
        }
        Vector3 velocity = new Vector3(0,rigidboby.velocity.y,0);
        moveDir = new Vector3(moveX , 0 , moveZ).normalized + velocity;
        
    }

    private void HanldeShotDavidBall(){

    }
    private void FixedUpdate() {
        rigidboby.velocity = moveDir;
    }
    private void HandleAttack(){
        if (Input.GetKey(KeyCode.Y)){
            
            SetStatePunch();
            if (!isAttacking)
            {
                isAttacking = true;
                // anim.Play("punch1_david_anim");
                // Invoke("SetStateIdle", attackDelay);
                if (isPlayingPunch1Animation()){
                    anim.Play("punch2_david_anim");
                    Invoke("SetStateIdle", attackDelay);
        
                }else{
                    anim.Play("punch1_david_anim");
                    Invoke("SetStateIdle", attackDelay);
                }
            }
            // anim.Play("punch1_david_anim");
                
        }
    }

    private bool isPlayingPunch1Animation(){
        return anim.GetCurrentAnimatorStateInfo(0).IsName("punch1_david_anim");
    }
    private bool isPlayingPunch2Animation(){
        return anim.GetCurrentAnimatorStateInfo(0).IsName("punch2_david_anim");
    }

    private void HandleJump(){
        if (IsGround() && Input.GetKey(KeyCode.U)){
            SetStateJump();
            anim.Play("jump_david_anim");
            Invoke("Jump" , 0.2f);
            Invoke("SetStateIdle", jumpDelay);
        }
    }
    private 
    void Jump(){
        rigidboby.velocity = Vector3.up* jumpVelocity;
    }
    
    
    // void Facing(){
    //     if (facingRight && moveX < 0){
    //         facingRight = false;
    //         transform.localScale = new Vector3(-0.9f,1,1);

    //     }
    //     else if(!facingRight && moveX >0) {
    //         facingRight = true;
    //         transform.localScale = new Vector3(0.9f,1,1);
    //     }
            
    // }
}
