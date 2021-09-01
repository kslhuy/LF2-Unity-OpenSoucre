
using System;
using UnityEngine;

public class AttackAIState : AIState
{
    public enum ActionTypeAvailable
    {
        None,
        Attack,

    }

    private AiBrain m_Brain;
    private ActionPlayer m_ActionPlayer;
    private ServerCharacter m_Foe;
    private ActionTypeAvailable m_CurAttackAction;



    public AttackAIState (AiBrain brain , ActionPlayer actionPlayer){
        m_Brain = brain;
        m_ActionPlayer = actionPlayer;
    }

    public override bool IsEligible()
    {
        return true;}
        // return ChoseFoe()! = null;
    //     return m_Foe != null || ChooseFoe() != null;
    // }

    // private CharacterClass ChoseFoe()
    // {
    //     Vector3 myPosition = m_Brain.GetMyData().transform.position;
    //     float closestDistanceSqr = int.MaxValue;
    //     CharacterClass closestFoe = null;
    //     foreach (var foe in m_Brain.GetHatedEnemies()){
    //         float distanceSqr = (myPosition - foe.).sqrMagnitude;
    //     }
    // }

    public override void Initialize()
    {
    
    }

    public override void Update()
    {
        // while idle, we are scanning for jerks to hate
        DetectFoes();   
    }

    private void DetectFoes()
    {
        throw new NotImplementedException();
    }
}
