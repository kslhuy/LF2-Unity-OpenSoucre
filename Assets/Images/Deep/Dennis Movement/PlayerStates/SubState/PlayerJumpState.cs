using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAbilityState
{
    private int amountOfJumpLeft ;

    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, int hashID) : base(player, stateMachine, playerData, hashID)
    {
        amountOfJumpLeft = playerData.amountOfJumpLeft;
    }

    public override void Enter()
    {
        base.Enter();
        amountOfJumpLeft--;
        isAbilityDone = true;
    }

    public bool CanJump(){
        if (amountOfJumpLeft > 0){
            return true;
        }else return false;
    }
    

    public void ResetAmountOfJumpsLeft()=> amountOfJumpLeft = playerData.amountOfJumpLeft;

    public void DecreaseAmountOfJumpsLeft()=>amountOfJumpLeft--;
}
