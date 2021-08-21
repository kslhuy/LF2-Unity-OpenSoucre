using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationBase : MonoBehaviour
{
    Animator anim;
    private void Start() {
        anim = GetComponent<Animator>();
    }
    public void PlayWalkingAnimation (Vector3 animationDir){
        anim.SetBool("isWalking" , true);
    }
    public void PlayRunAnimation (Vector3 animationDir){
        anim.SetBool("isRunning" , true);
    }
}
