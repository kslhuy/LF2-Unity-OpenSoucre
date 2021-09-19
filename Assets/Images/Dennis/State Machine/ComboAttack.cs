using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ComboAttackType", menuName = "ComboAttack/Type")]
public class ComboAttack : ScriptableObject
{
    [SerializeField] List<KeyPress> NeedKeyPress ; //the List and order of the Moves
    [SerializeField] TypeSkills typeCombo; //The kind of the move
    // [SerializeField] int ComboPriorty = 0; //the more complicated the move the higher the Priorty

    public bool isTheSame(List<KeyPress> playerKeyCodes) //Check if we can perform this move from the entered keys
    {
        int comboIndex = 0;

        for (int i = 0; i < playerKeyCodes.Count; i++)
        {
            if (playerKeyCodes[i] == NeedKeyPress[comboIndex])
            {
                comboIndex++;
                if (comboIndex == NeedKeyPress.Count) //The end of the Combo List
                    return true;
            }
            else
                comboIndex = 0;
        }
        return false;

    }
    
    public TypeSkills GetTypeOfState(){
        return typeCombo;
    }
}
