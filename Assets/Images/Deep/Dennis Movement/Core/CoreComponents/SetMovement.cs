using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMovement : CoreComponent
{

    private Vector3 workSpace;
    public int FacingDirection{get ; private set;}
    public Rigidbody Rigidbody{get;private set;}

    protected override void Awake() {
        base.Awake();
        Rigidbody = GetComponentInParent<Rigidbody>();
        FacingDirection = 1;
    }

    #region Set Functions
         
    public void SetVelocityXZ(float velocityx, float velocityz){
        workSpace.Set(velocityx ,0, velocityz);
        // Debug.Log (workSpace);
        Rigidbody.velocity = workSpace;
        // transform.position += workSpace*Time.deltaTime;
    }

    public void SetVelocityY(float velocityy , Vector3 moveDir){
        Rigidbody.velocity = Vector3.up*velocityy + moveDir ;
    }

    public void SetVelocityRun(float velocityRun ){
        workSpace.Set(velocityRun*FacingDirection ,0, 0);
        Rigidbody.velocity = workSpace;

        // transform.position += workSpace*Time.deltaTime;
    }
    public void SetFallingDown(){
        Rigidbody.velocity += 0.5f*Physics.gravity.y*Vector3.up*Time.deltaTime ;
    }

    public void SetVolocityDoubleJump( Vector3 velocitydoubleJump){
        workSpace.Set(velocitydoubleJump.x*FacingDirection ,velocitydoubleJump.y, velocitydoubleJump.z);
        Rigidbody.velocity = workSpace;
    }


    public void SetVelocitySliding(float GainDecreaseRunSpeed , float RunSpeed ){
        workSpace.Set(RunSpeed*FacingDirection,0,0);
        Rigidbody.velocity = workSpace;

        // transform.position += FacingDirection  * workSpace * Time.deltaTime ;
    }

    public void SetVelocityRolling(float RollingSpeed  ){
        workSpace.Set(RollingSpeed*FacingDirection,0,0);
        Rigidbody.velocity = workSpace;

        // transform.position += FacingDirection  * workSpace * Time.deltaTime ;
    }

    public void CheckIfShouldFlip(int xInput){
        if (xInput != 0 && xInput != FacingDirection){
            Flip();
        }
    }

    public void Flip(){
        FacingDirection *=-1;
        Rigidbody.transform.Rotate(0.0f,180.0f,0.0f);

    }


    #endregion
}
