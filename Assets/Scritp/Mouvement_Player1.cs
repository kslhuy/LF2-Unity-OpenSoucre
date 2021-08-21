using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Mouvement_Player1 : MonoBehaviour
{
    Rigidbody rigidboby;
    Animator anim;
    SpriteRenderer spirterender;
    [SerializeField] private GameObject HitBoxLeft ;
    [SerializeField] private GameObject HitBoxRight ;

    public event EventHandler CheckKeypressed;
    [SerializeField] Text controlsTestText; //Just for testing for printing the keys
    [SerializeField] List<KeyCode> KeysPressed; //List of all the Keys Pressed so far

    [SerializeField] List<KeyCode> KeysPressedLeft; //List of all the Keys Pressed so far
    [SerializeField] List<Move> avilableSkills; //All the Avilable Moves

    /*Check all button pressed  */
    private bool isButtonMovePressed = false;
    private bool isButtonAttackPressed;
    private bool isButtonJumpPressed;
    private bool isButtonDefensePressed;
    
    /*Check all button pressed  */
    float distance_attack = 1f;
    float distance_defDownA = 0.8f;
    float moveSpeed = 0.7f;
    float runSpeed = 1.5f;
    private float GainDecreaseRunSpeed = 5f;

    

    [SerializeField] private float ballSpeed = 3f;
    bool isHurting , isDead;
    private Vector3 LastMove;
    bool facingRight = true;

    float walkSpeed = 0.5f;
    private BoxCollider boxCollider;
    [SerializeField] private LayerMask GoundMask;
    [SerializeField] private float jumpVelocity = 4f;
    [SerializeField] private float attackDelay = 0.33f;
    [SerializeField] private float jumpDelay = 1f;
    
    private float timer;


    // private bool comboPrioriy ;

    // private float ComboResetTime = 0.2f; 

    float clickWaitTime = 0.2f;
    
    float lastClickTimeRight;
    float lastClickTimeleft;
    int clickCountright ;
    int clickCountleft;

    int clickCount;
    private bool IsAttacking;
    private bool IsJumpping = false;
    // Direction taken from input action for Player movement
    private Vector3 moveDir;  
    // private int bitmask = 1 << 7;
    private Vector3 AttackDir;

    
    private State state;

    private enum State{
        Idle,
        Move,
        Run,
        RunSliding,
        Attack,
        Jump,
        Defense,
        DefdownA,
        DefupA,
        DefleftA

    }

    private void Awake()
    {
        // animationBase = GetComponent<Animation_Base>();
        rigidboby = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        anim = GetComponent<Animator>();
        spirterender = GetComponent<SpriteRenderer>(); 
        
        SetStateIdle();
        Physics.IgnoreLayerCollision(7,7,true);
        
    }
    private void Start() {
        moveDir = new Vector3(1,0,0);
    }

    // Update is called once per frame
    void Update()
    {
        // CheckMovement();
        CheckDoubleClicked();
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
            case State.Defense:
                break;
            case State.DefdownA:
                break;
            case State.DefleftA:
                HanldeShotDavidBall();
                break;
            case State.Run:
                CheckMovement();
                HandleRun();
                HandleJump();
                break;

        }
        
        if (isButtonDefensePressed){
            DetectPressedKey();
            PrintControls();
            HandleAnimationSkill();
        }
        
        // Debug.Log(state);


    }


    private void SetStateIdle(){
        state = State.Idle ;
        IsJumpping = false;        
        runSpeed = 1.5f;
    }
    private void SetStatePunch(){
        state = State.Attack ; 

    }
    private void SetStateMove(){
        state = State.Move ;
        IsJumpping = false;        

    }
    private void SetStateJump(){
        state = State.Jump ;
        IsJumpping = true;
    }
    private void SetStateDefense(){
        state = State.Defense ;
        IsAttacking = false;
        IsJumpping = false;  
    }

    private void SetStateDef_DownA(){
        state = State.DefdownA;
    }
    private void SetStateDef_UpA(){
        state = State.DefupA;
    }
    private void SetStateDef_LeftA(){
        state = State.DefleftA;
    }

    private void SetStateRun(){
        state = State.Run;
    }
    
    // What wrong with this function 
    private bool IsGround(){
        float extraHeightTest = 0.2f;
        bool hit_ground = Physics.Raycast(boxCollider.bounds.center, Vector3.down,boxCollider.bounds.extents.y+extraHeightTest,GoundMask);
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

    // Function to Run 
    void CheckDoubleClicked(){
        if (Input.GetKeyDown(KeyCode.D)){

            if (Time.time - lastClickTimeleft <= clickWaitTime) clickCount++;
            else clickCount = 1;
            
            lastClickTimeleft = Time.time;

            if (clickCount >= 2 ){
                SetStateRun();
            }
  
        }
        
        else if (Input.GetKeyDown(KeyCode.Q)){
            if (Time.time - lastClickTimeRight <= clickWaitTime){
                clickCount++;   
            }
            else{
                clickCount = 1;
            }
            lastClickTimeRight = Time.time;
            if (clickCount >= 2 ){
                SetStateRun();
            }
        }
    }

 
    private void HandleMovement(){
        CheckMovement();
        if (IsGround() && isButtonMovePressed){
            SetStateMove();
            anim.Play("walk_david_anim");
        }
        else if (IsGround () && !isButtonMovePressed)  {
            SetStateIdle();
            anim.Play("idel_david_anim");
        }
    }

    private void HandleRun(){  
        anim.Play("run_david_anim"); 
        if(facingRight && Input.GetKeyDown(KeyCode.Q) || !facingRight && Input.GetKeyDown(KeyCode.D) ){
            anim.Play("Sliding_david_anim");
            state = State.RunSliding;
        }
    }
    private void HandleRunSliding(){
        runSpeed -= runSpeed*Time.deltaTime*GainDecreaseRunSpeed;
        transform.position += -1f * AttackDir  * runSpeed * Time.deltaTime ;
        if (runSpeed <= 0.1f ){
            SetStateIdle();
        }
    }

    private void HandleAttack(){
        if (Input.GetKeyDown(KeyCode.Y) ){
            // int punch1 = UnityEngine.Random.Range(1,3);
            SetStatePunch();
            if (!IsJumpping && IsGround()){
                if (isPlayingPunch1Animation()){
                    anim.Play("punch2_david_anim");
                    Vector3 OffsetToPuch = new Vector3(0,0.25f,0) + AttackDir*0.1f;
                    SpriteAnimator.Create(GetPosition() + OffsetToPuch , AttackDir);
                    transform.position += AttackDir * distance_attack * Time.deltaTime ;
                    // rigidboby.velocity = AttackDir * distance_attack;
                    // Invoke("SetStateIdle", attackDelay);
                    // StopAnimation();
                }else{
                    anim.Play("punch1_david_anim");
                    transform.position += AttackDir * distance_attack * Time.deltaTime;
                    // rigidboby.velocity = AttackDir * distance_attack;
                    // Invoke("SetStateIdle", attackDelay);
                    // StopAnimation();
                }
            // On air 
            }
            if  (IsJumpping ){
                // float JumpKickDelay = 0.5f;
                anim.Play("jumpkick_david_anim");
                // Invoke("SetStateIdle", JumpKickDelay);
                // StopAnimation();
            }

            // StartCoroutine(DoAttack());
            
        }
        
    }

    private void HandleJump(){
        if (IsGround() && Input.GetKeyDown(KeyCode.U) && state != State.Run){
            SetStateJump();
            anim.Play("jump_david_anim");
            rigidboby.velocity = Vector3.up* jumpVelocity + moveDir;
            // Jump one 
            // Invoke("Jump" , 0.2f);
            // Invoke("SetStateIdle", jumpDelay);
        }
        if (IsGround() && Input.GetKeyDown(KeyCode.U) && state == State.Run){
            SetStateJump();
            anim.Play("jumpRun_david_anim");
            rigidboby.velocity = Vector3.up* jumpVelocity + AttackDir*runSpeed;
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


    private void HanldeShotDavidBall(){
        if (Input.GetKeyDown(KeyCode.Y)){
            CreateBall();
        }
    }
    private void CreateBall(){
        Vector3 OffsetToHand = new Vector3(0.1f,0.25f,0) +  AttackDir*0.1f;
        DavidBall.Create(GetPosition() + OffsetToHand, AttackDir);
        
        if (isD_left_a_david_1Animation()){
            anim.Play("d_left_a_2david_anim");
        }else{
            anim.Play("d_left_a_david_anim");
        }

    }

    private bool isPlayingPunch1Animation(){
        return anim.GetCurrentAnimatorStateInfo(0).IsName("punch1_david_anim");
    }
    private bool isD_left_a_david_1Animation(){
        return anim.GetCurrentAnimatorStateInfo(0).IsName("d_left_a_david_anim");
    }

    private bool isPlayingJumpAnimation(){
        return anim.GetCurrentAnimatorStateInfo(0).IsName("jump_david_anim");
    }
    private IEnumerator DoAttack(){
        if (facingRight){
            HitBoxRight.SetActive(true);
        }else
        {
            HitBoxLeft.SetActive(true);
        }
        yield return new WaitForSeconds(0.1f);
        HitBoxLeft.SetActive(false);
        HitBoxRight.SetActive(false);
    }
    private void FixedUpdate() {
        // 2 methode to move the player : physic velocity or transform
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
            case State.Run:
                transform.position += (AttackDir + new Vector3 (0,0,0.5f*moveDir.z))* runSpeed * Time.deltaTime ;
                break;
            case State.RunSliding:
                HandleRunSliding();
                break;
            case State.Jump:
                break;
            // case State.Attack:
            //     rigidboby.velocity = AttackDir*0.5f;
            //     break;
            case State.DefdownA:
                // Debug.Log(AttackDir * distance_defDownA);
                rigidboby.velocity =  AttackDir * distance_defDownA  ;
                // if (facingRight){
                //     rigidboby.velocity =  Vector3.right * distance_defDownA ;
                // }else
                // {
                //     rigidboby.velocity =  Vector3.left * distance_defDownA ;
                // }

                //// no physic
                // transform.position = Vector3.left * distance_defDownA * Time.deltaTime;
                break;

        }
    }
    public Vector3 GetPosition(){
        return transform.position;
    }

    public void DetectPressedKey()
    {
        //Go through all the Keys
        //To make it faster we can attach a class and put all the keys that are allowed to be pressed
        //This will make the process a bit faster rather than moving through all keys
            
        foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(kcode))
            {
                KeysPressed.Add(kcode); //Add the Key to the List
                // if (KeysPressed[0] == KeyCode.D ){
                // StartCoroutine(ResetComboTimer());
            // }
            }

        }
    }


    public void StopAnimation(){
        
        if (IsJumpping){
            // Run animation landing first
            // Debug.Log(IsJumpping);
            anim.Play("EndJump_david_anim");
        }
        
        else {
            // Can directly go back to idle 
            SetStateIdle();  
        } 
    }

    // Need to be done ! 
    // private void StopJumpAnimation(){
    //     if (IsJumpping && IsGround() && isPlayingJumpAnimation()){
    //         anim.Play("EndJump_david_anim");
    //     }
    // }
    private void HandleAnimationSkill(){
        if (KeysPressed.Count == 3 && isButtonDefensePressed){
            int i = 0;
            foreach (Move move in avilableSkills){
                // Debug.Log("true");
                i += 1 ; 
                if (move.isMoveAvilable(KeysPressed)){
                    // 2 parameter : name class of Scriptable Object
                    //                      Function return type of skill 
                    // Debug.Log(move.GetTypeOfSkill());
                    PlayAnimationSkill(move.GetTypeOfSkill());
                    KeysPressed.Clear();
                    isButtonDefensePressed = false;
                }
                if (avilableSkills.Count == i)
                {
                    KeysPressed.Clear();
                    isButtonDefensePressed = false;
                }
            }
        }
        // if (KeysPressed.Count > 3){
        //     KeysPressed.Clear();
        //     isButtonDefensePressed = false;
        // }
    }

    private void PlayAnimationSkill(TypeSkills TypeMove){
        switch (TypeMove){
            case TypeSkills.DefDownAttack : 
                SetStateDef_DownA();
                anim.Play("d_down_a_david_anim");
                break; 
            case TypeSkills.DefLeftAttack:
                SetStateDef_LeftA();
                CreateBall();
                break;   
            case TypeSkills.DefRightAttack:
                SetStateDef_LeftA();
                CreateBall();
                break;  
            case TypeSkills.DefUpAttack:
                // SetStateDef_UpA();
                anim.Play("d_up_a_david_anim");
                Invoke("SetStateIdle", 0.667f);
                break;
        }
    }
    

    //Printing Keys just for testing
    public void PrintControls() {
        controlsTestText.text = " Keys Pressed :";
        foreach (KeyCode kcode in KeysPressed)
            controlsTestText.text += kcode + ",";
    }

        //     bool isIdle = moveZ == 0 && moveX == 0;

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
    


