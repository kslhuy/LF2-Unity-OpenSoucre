using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_Base : MonoBehaviour
{
    Animator anim;
    private void Start() {
        anim = GetComponent<Animator>();
    }
    public void PlayIdelAnimation (){
        anim.Play("idel_david_anim");
    }
    public void PlayWalkingAnimation (){
        anim.Play("walk_david_anim");
    }
    public void PlayRunAnimation (Vector3 animationDir){
        anim.Play("run_david_anim");
    }

    public void PlayPunch2Animation (){
        anim.Play("punch2_david_anim");
    }
    public void PlayPunch1Animation (){
        anim.Play("punch1_david_anim");
    }
    
    // public void PlayJumpAnimation (bool IsGround){
    //     if (IsGround)
    //     anim.SetBool("IsJumping" , false);
    //     else{
    //     anim.SetBool("IsJumping" , true);
    //     }
    // }
}
