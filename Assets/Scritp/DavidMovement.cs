using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DavidMovement : MonoBehaviour
{
    Rigidbody rigidboby;
    Animator anim;
    SpriteRenderer spirterender;
    [SerializeField] private GameObject HitBoxLeft ;
    [SerializeField] private GameObject HitBoxRight ;

    [SerializeField] Text controlsTestText; //Just for testing for printing the keys
    [SerializeField] List<KeyCode> KeysPressed; //List of all the Keys Pressed so far
    [SerializeField] List<ActionType> avilableSkills; //All the Avilable Moves

    [SerializeField] FixedJoystick joystick;

    /*Check all button pressed  */
    private bool isButtonMovePressed = false;
    private bool isButtonAttackPressed = false;
    private bool isButtonDefensePressed;
    
    /*Check all button pressed  */
    float distance_attack = 1f;
    float distance_defDownA = 0.8f;
    float _MoveSpeed = 0.8f;
    float _RunSpeed = 1.5f;
    private float GainDecreaseRunSpeed = 5f;


    private Vector3 LastMove;
    bool facingRight = true;

    private BoxCollider boxCollider;
    [SerializeField] private LayerMask GoundMask;
    [SerializeField] private float _jumpNormalSpeed = 4f;
    [SerializeField] private float _jumpRunSpeed = 3.5f;

    float clickWaitTime = 0.2f;
    
    float lastClickTimeRight;
    float lastClickTimeleft;

    int clickCount;
    private bool IsAttacking = false;
    private bool IsJumpping = false;
    // Direction taken from input action for Player movement
    private Vector3 moveDir;  
    // private int bitmask = 1 << 7;
    private Vector3 AttackDir ;

    // Animation Base
    // [SerializeField] AnimationBase animationBase;

    
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
        DefleftA,
        RunPunch
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
        AttackDir = new Vector3(1,0,0);
    }

    // Update is called once per frame
    void Update()
    {
        TransitionState();
        if (OnJumpButHitGround()) {
            anim.Play("EndJump_david_anim");        
        }

        if (isButtonDefensePressed)
        {
            DetectPressedKey();
            PrintControls();
            HandleAnimationSkill();
        }
    }

    private void TransitionState()
    {
        switch (state)
        {
            case State.Idle:
                HandleMovement();
                HandleAttack();
                HanldeDefense();
                DoubleClickedToRun();
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
                HandleDoubleJump();
                break;
            case State.DefleftA:
                HanldeShotDavidBall();
                break;
            case State.Run:
                CheckMovement();
                HandleRun();
                HandleJump();
                HanleAttackRun();
                break;
        }

    }

    private void SetStateIdle(){
        state = State.Idle ;
        IsJumpping = false;        
        _RunSpeed = 1.5f;
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
        float extraHeightTest = 0f;
        bool hit_ground = Physics.Raycast(boxCollider.bounds.center, Vector3.down,boxCollider.bounds.extents.y+extraHeightTest,GoundMask);
        Color rayColor;
        if (!hit_ground){
            rayColor = Color.green;
        }else {
            rayColor = Color.red;
        }
        Debug.DrawRay(boxCollider.bounds.center , Vector3.down * (boxCollider.bounds.extents.y+extraHeightTest),rayColor);
        
        return hit_ground;
    }
    // private bool IsHit_Something(){
    //     float extraHeightTest = 0.1f;
    //     RaycastHit hit_Somthing;
    //     bool hit_something = Physics.BoxCast(boxCollider.bounds.center ,boxCollider.bounds.extents , LastMove,Quaternion.identity,1f,bitmask);
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
        moveX = joystick.Horizontal;
        moveZ = -joystick.Vertical;
        // if (Input.GetKey(KeyCode.UpArrow)){
        //     moveZ = -1f;
        // }
        // if (Input.GetKey(KeyCode.DownArrow)){
        //     moveZ = +1f;
        // }
        // if (Input.GetKey(KeyCode.RightArrow)){
        //     moveX = 1f;
        //     // transform.localScale = new Vector3(-0.85f,1,1);
        //     // facingRight = true;
        // }
        // if (Input.GetKey(KeyCode.LeftArrow)){
        //     moveX = -1f;
        //     // transform.localScale = new Vector3(-0.85f,1,1);
        //     // facingRight = false;
        // }
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
    private void DoubleClickedToRun(){
        if (Input.GetKeyDown(KeyCode.D)){
            if (Time.time - lastClickTimeleft <= clickWaitTime) clickCount++;
            else clickCount = 1;
            lastClickTimeleft = Time.time;
            if (clickCount >= 2 ){
                SetStateRun();
                anim.Play("run_david_anim"); 
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
                anim.Play("run_david_anim"); 
            }
        }
    }

 
    private void HandleMovement(){
        CheckMovement();
        if (IsGround() && isButtonMovePressed){
            SetStateMove();
            anim.Play("Walk_anim");
        }
        else if (IsGround () && !isButtonMovePressed)  {
            SetStateIdle();
            anim.Play("Idle_anim");
        }
    }

    private void HandleRun(){ 
        if(facingRight && Input.GetKeyDown(KeyCode.Q) || !facingRight && Input.GetKeyDown(KeyCode.D) ){
            anim.Play("Sliding_david_anim");
            state = State.RunSliding;
        }
    }
    private void HandleRunSliding(){
        _RunSpeed -= _RunSpeed*Time.deltaTime*GainDecreaseRunSpeed;
        transform.position += -1f * AttackDir  * _RunSpeed * Time.deltaTime ;
        if (_RunSpeed <= 0.1f ){
            SetStateIdle();
        }
    }

    private void HandleAttack(){
        if (Input.GetKeyDown(KeyCode.Y) && !IsAttacking ){
            // int punch1 = UnityEngine.Random.Range(1,3);
            if (!IsJumpping && IsGround()){
                SetStatePunch(); // Need to review ???? 
                if (is_Punch1Animation()){
                    anim.Play("punch2_david_anim");

                    // Partical effet of Punch
                    Vector3 OffsetToPuch = new Vector3(0,0.25f,0) + AttackDir*0.1f;
                    SpriteAnimator.Create(GetPosition() + OffsetToPuch , AttackDir);
                    // Move forward when attacking
                    transform.position += AttackDir * distance_attack * Time.deltaTime ;
                    // rigidboby.velocity = AttackDir * distance_attack;
                    // Invoke("SetStateIdle", attackDelay);
                    // StopAnimation();
                }else{
                    anim.Play("punch1_david_anim");
                    // Move forward when 
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

            StartCoroutine(DoAttack());
            
        }
        
    }

    // need to review 
    private void HanleAttackRun(){
        if (Input.GetKeyDown(KeyCode.Y)){
            SetStateRun_Punch();
            anim.Play("runPunch_david_anim");
            // transform.position += AttackDir * _RunSpeed * Time.deltaTime ;
            rigidboby.velocity = AttackDir * _RunSpeed;
        }
    }

    private void SetStateRun_Punch()
    {
        state = State.RunPunch;
    }

    private void HandleJump(){
        if (IsGround() && Input.GetKeyDown(KeyCode.U) && state != State.Run){
            SetStateJump();
            anim.Play("jump_david_anim");
            rigidboby.velocity = Vector3.up* _jumpNormalSpeed + moveDir;

        }
        if (IsGround() && Input.GetKeyDown(KeyCode.U) && state == State.Run){
            // SetStateJump(); // In LF2 orginal cant not double jump when run jump
            anim.Play("jumpRun_david_anim");
            rigidboby.velocity = Vector3.up* _jumpRunSpeed + AttackDir * _MoveSpeed ;
        }

        
    }

    private void HandleDoubleJump(){
        float conditionToDoubleJump = 0.2f;
        if (Input.GetKeyDown(KeyCode.U) && Mathf.Abs(rigidboby.velocity.y) < conditionToDoubleJump && is_End_JumpAnimation()){
            anim.Play("jumpRun_david_anim");
            rigidboby.velocity = Vector3.up* _jumpRunSpeed + AttackDir*_RunSpeed ;
        }
    }
    private bool OnJumpButHitGround(){
        // This function will stop all the jump animation to End jump animation
        // And Switch to Idle State
        if (is_Jump_KickAnimation() || is_Jump_RunAnimation() || is_JumpAnimation() ){
            if (IsGround())  
                return true;
        } 
        return false;
        

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
        if (Input.GetKeyDown(KeyCode.Y) && !isButtonAttackPressed){
            StartCoroutine(CreateBall());
        }
    }
    private IEnumerator CreateBall()
    {
        isButtonAttackPressed = true;
        // Creat particle ball projectil
        Vector3 OffsetToHand = new Vector3(0.1f, 0.25f, 0) + AttackDir * 0.1f;
        DavidBall.Create(GetPosition() + OffsetToHand, AttackDir);
        // Run animation
        D_left_a_Animation();
        yield return new WaitForSeconds(0.2f);
        isButtonAttackPressed = false;
    }

    private void D_left_a_Animation()
    {
        if (is_D_left_a_david_1Animation())
        {
            anim.Play("d_left_a_2david_anim");
        }
        else
        {
            anim.Play("d_left_a_david_anim");
        }
    }

//Check is run animation
    private bool is_Punch1Animation(){
        return anim.GetCurrentAnimatorStateInfo(0).IsName("punch1_david_anim");
    }

    private bool is_End_JumpAnimation(){
        return anim.GetCurrentAnimatorStateInfo(0).IsName("EndJump_david_anim");
    }
    private bool is_D_left_a_david_1Animation(){
        return anim.GetCurrentAnimatorStateInfo(0).IsName("d_left_a_david_anim");
    }

    private bool is_JumpAnimation(){
        return anim.GetCurrentAnimatorStateInfo(0).IsName("jump_david_anim");
    }
    
    private bool is_Jump_KickAnimation(){
        return anim.GetCurrentAnimatorStateInfo(0).IsName("jumpkick_david_anim");
    }

    private bool is_Jump_RunAnimation(){
        return anim.GetCurrentAnimatorStateInfo(0).IsName("jumpRun_david_anim");
    }

// PRoblem 
    private IEnumerator DoAttack(){
        IsAttacking = true;
        if (facingRight){
            HitBoxRight.SetActive(true);
        }else{
            HitBoxLeft.SetActive(true);
        }
        yield return new WaitForSeconds(0.15f);
        HitBoxLeft.SetActive(false);
        HitBoxRight.SetActive(false);
        IsAttacking = false;
    }


    // Perform the transform of the Player
    private void FixedUpdate() {
        // 2 methode to move the player : physic velocity or transform
        switch (state){
            case State.Idle:
                HandleJump();
                break;
            case State.Move:
                // velocity_target = moveDir;
                // rigidboby.velocity = moveDir;
                transform.position += moveDir * _MoveSpeed * Time.deltaTime ;
                HandleJump();
                break;
            case State.Run:
                transform.position += (AttackDir + new Vector3 (0,0,0.5f*moveDir.z))* _RunSpeed * Time.deltaTime ;
                break;
                // When we stop Run keep slide a litter bit (physic Inertia) 
            case State.RunSliding:
                HandleRunSliding();
                break;

            case State.DefdownA:
                rigidboby.velocity =  AttackDir * distance_defDownA  ;
                break;

            case State.RunPunch: 
                transform.position += AttackDir * _RunSpeed * Time.deltaTime ;
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
        // Can directly go back to idle 
        SetStateIdle();  
    }

    private void HandleAnimationSkill(){
        if (KeysPressed.Count == 3 && isButtonDefensePressed){
            int i = 0;
            foreach (ActionType move in avilableSkills){
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
                StartCoroutine(CreateBall());
                break;   
            case TypeSkills.DefRightAttack:
                SetStateDef_LeftA();
                StartCoroutine(CreateBall());
                break;  
            case TypeSkills.DefUpAttack:
                SetStateDef_UpA();
                rigidboby.velocity = Vector3.up* _jumpNormalSpeed + AttackDir*_RunSpeed;
                anim.Play("d_up_a_david_anim");
                break;
        }
    }
    

    //Printing Keys just for testing
    public void PrintControls() {
        controlsTestText.text = " Keys Pressed :";
        foreach (KeyCode kcode in KeysPressed)
            controlsTestText.text += kcode + ",";
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
    


