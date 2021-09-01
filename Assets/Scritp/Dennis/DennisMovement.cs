using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DennisMovement : MonoBehaviour
{
    private bool b_isIdle;
    private Vector3 v_moveDir;
    private Vector3 v_LastMove;
    private Vector3 v_AttackDir;

    private bool b_facingRight = true;

    [SerializeField] SpriteRenderer spriteRenderer;
    void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>(); 
    }

    void Update()
    {
        CheckMovement();
    }

    private void CheckMovement(){
        float moveZ = 0f;
        float moveX = 0f;
        if (Input.GetKey(KeyCode.UpArrow)){
            moveZ = -1f;
        }
        if (Input.GetKey(KeyCode.DownArrow)){
            moveZ = +1f;
        }
        if (Input.GetKey(KeyCode.RightArrow)){
            moveX = 1f;
        }
        if (Input.GetKey(KeyCode.LeftArrow)){
            moveX = -1f;
        }
        // Sauf Run state not flip 
        Flip();
        b_isIdle = moveZ == 0 && moveX == 0;
        v_moveDir = new Vector3(moveX , 0 , moveZ).normalized  ;
        transform.position += v_moveDir * 1.5f * Time.deltaTime ;

        if (!b_isIdle){
            v_LastMove = new Vector3 (moveX ,0 , moveZ);
        }
        if (moveX != 0 ) {
            v_AttackDir = new Vector3 (moveX ,0 , 0);
        }
    }

    private void Flip(){
        if (v_LastMove.x > 0 && !b_facingRight){
            b_facingRight = !b_facingRight;
            spriteRenderer.flipX = false;
        }
        else if (v_LastMove.x < 0 && b_facingRight) {
            b_facingRight = !b_facingRight;
            spriteRenderer.flipX = true;
        }
    }

}
