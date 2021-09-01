using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deep : MonoBehaviour
{

    public CharacterClass m_deep;
    private ActionPlayer m_ActionPlayer;

    private AiBrain m_AIBrain;
    bool IsNPC {
        get {return m_deep.IsNpc;} 
    }
    private CharacterMovement m_Movement;
         
    
    // private void Awake() {
    //     // m_Movement = GetComponents<CharacterMovement>();
    //     m_ActionPlayer = new ActionPlayer(this);
    //     if (IsNPC){
    //         m_AIBrain = new(this , m_ActionPlayer);
    //     }
    // }
    private void Start() {
       
    }


}
