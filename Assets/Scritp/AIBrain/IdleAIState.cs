
using System;
using UnityEngine;

public class IdleAIState : AIState
{
    private AiBrain m_Brain;

    public IdleAIState (AiBrain brain){
        m_Brain = brain;
    }

    public override bool IsEligible()
    {
        return true;
        // Idle so dont see any ennemies
        // return m_Brain.GetHatedEnemies().Count == 0;
    }

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
        // This is usually the same value as is indicated by our game data Scrtiable Object (CharaterClass )
        // Need to Ref to that LATER 
        float detectionRange = 5f;
        float detectionRangeSqr = detectionRange * detectionRange;
        Vector3 position = m_Brain.GetMyData().transform.position;

        // in this game, NPCs only attack players (and never other NPCs), so we can just iterate over the players to see if any are nearby

        if ((m_Brain.GetPositionEnemies() - position).sqrMagnitude <= detectionRangeSqr){
            // m_Brain.Hate(CharacterClass);
        }

    }
}   
