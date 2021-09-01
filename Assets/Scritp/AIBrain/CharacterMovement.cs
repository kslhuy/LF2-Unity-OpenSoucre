using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public enum MovementState
    {
        Idle = 0,
        PathFollowing = 1,
        Charging = 2,
        Knockback = 3,
    }

public class CharacterMovement : MonoBehaviour
{

    private Rigidbody m_rigidbody;
    private NavMeshAgent m_navMeshAgent;

    private MovementState m_movementState;

    private CharacterClass m_CharLogic;

    // when we are in charging and knockback mode, we use these additional variables
    private float m_ForcedSpeed;
    private float m_SpecialModeDurationRemaining;

    // this one is specific to knockback mode
    private Vector3 m_KnockbackVector;

    private void Awake() {
        m_navMeshAgent = GetComponent<NavMeshAgent>();
        m_CharLogic = GetComponent<CharacterClass>();
        m_rigidbody = GetComponent<Rigidbody>();
        
    }

}
