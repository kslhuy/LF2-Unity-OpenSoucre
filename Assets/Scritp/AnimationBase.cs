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
    public void PlayRunAnimation (){
        anim.Play("run_david_anim");
    }

    public void PlaySlidingAfterRunAnimation() {
        anim.Play("Sliding_david_anim");
    }

    public void PlayPunch2Animation (){
        anim.Play("punch2_david_anim");
    }
    public void PlayPunch1Animation (){
        anim.Play("punch1_david_anim");
    }

    public void PlayJumpKickAnimation(){
        anim.Play("jumpkick_david_anim");
    } 

    public void PlayJumpAnimation(){
        anim.Play("jump_david_anim");

    }
    public void PlayJumpRunAnimation(){
        anim.Play("jumpRun_david_anim");

    }

    public void PlayDefAnimation(){
        anim.Play("def_david_anim");
    }

    private void PlayD_left_a_Animation()
    {
        if (isD_left_a_david_1Animation())
        {
            anim.Play("d_left_a_2david_anim");
        }
        else
        {
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
    
    // public void PlayJumpAnimation (bool IsGround){
    //     if (IsGround)
    //     anim.SetBool("IsJumping" , false);
    //     else{
    //     anim.SetBool("IsJumping" , true);
    //     }
    // }
}
