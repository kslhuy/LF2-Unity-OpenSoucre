using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EIdleState : State
{
    public EIdleState(Entity entity, FiniteStateMachine stateMachine, int animationName) : base(entity, stateMachine, animationName)
    {
    }

}
