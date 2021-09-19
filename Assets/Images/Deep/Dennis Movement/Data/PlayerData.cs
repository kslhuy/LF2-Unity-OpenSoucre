using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
    [Header("Move State")]
    public float movementVelocityX = 1.5f;
    public float movementVelocityZ = 1f;

    [Header("Jump State")]
    public float jumpVelocity = 4f;
    public int amountOfJumpLeft = 2;
    public float groundCheck = 0.2f;
    public LayerMask whatIsGround ; 

    [Header("Double Jump State")]
    public Vector3 DoublejumpVelocity = new Vector3(2f ,0f, 4f);


    [Header("Run State")]
    public float runVelocity = 2f;

    [Header("Sliding State ")]
    public float GainDecreaseRunSpeed=5f;

    [Header("Rolling Velocity")]
    public float rollingVelocity = 3f;
    public float distanceRolling = 3f;

    [Header("Attack General")]
    public float attackRadius;
    public LayerMask whatIsEnemy;
    
    [Header("Attack1 Distance")]
    [Header("Attack2 Distance")]
    public float att12distance = 2f;
    [Header("Attack3 Distance")]
    public float att3distance = 2f;


    [Header("AI Agent Config")]
    
    public float maxTime = 1.0f;
    public float maxDistance = 0.5f;

    [Header("Health")]
    public float healthMAX = 100f;



}
