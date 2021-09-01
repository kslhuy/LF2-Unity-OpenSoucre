using System;
using System.Collections.Generic;
using UnityEngine;

public class AiBrain 
{
    private enum AIStateType
    {
        ATTACK,
        //WANDER,
        IDLE,
    }

    static readonly AIStateType[] k_AIStates = (AIStateType[])Enum.GetValues(typeof(AIStateType));
    // Bot 
    public Deep m_deep;
    public Dennis denis;
    public DavidMovement m_david;

    private ActionPlayer m_ActionPlayer;

    private AIStateType m_CurrentState;
    private Dictionary<AIStateType, AIState> m_Logics;

    private ServerCharacter m_ServerCharacter;
    // List ennemies
    public List<CharacterClass> m_HatedEnemies;


    public AiBrain(Deep deep , ActionPlayer myActionPlayer){
        m_deep = deep;
        m_ActionPlayer = myActionPlayer;
        m_Logics = new Dictionary<AIStateType, AIState>{
            [AIStateType.IDLE] = new IdleAIState(this),
            [AIStateType.ATTACK] = new AttackAIState(this , m_ActionPlayer),
        };
        m_HatedEnemies = new List<CharacterClass>();
        m_CurrentState = AIStateType.IDLE;

    }

    private void Update() {
        AIStateType newState = FindBestEligibleAIState();
        if (m_CurrentState != newState){
            m_Logics[newState].Initialize();
        }
        // If not thing change , so new == old state
        m_CurrentState = newState;
        m_Logics[m_CurrentState].Update();
    }

    private AIStateType FindBestEligibleAIState()
    {
        foreach (AIStateType aiStateType in k_AIStates){
            // m_Logics[aiStateType] == classe (AttackAIState or IdleAIState) who inherited by AIState 
            if (m_Logics[aiStateType].IsEligible()){
                return aiStateType;
            }
        }
        Debug.LogError("No AI states are valid!?!");
        return AIStateType.IDLE;
    }

    public Deep GetMyData(){
        return m_deep;
    }

    public Vector3 GetPositionEnemies(){
        return m_david.GetPosition();
    }

    public void Hate(CharacterClass character){
        if (!m_HatedEnemies.Contains(character)){
            m_HatedEnemies.Add(character);
        }
    }

    public List<CharacterClass> GetHatedEnemies(){
        return m_HatedEnemies;
    }

    



}
