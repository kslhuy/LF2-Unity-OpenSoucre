using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EDeadState : State
{
    public EDeadState(Entity entity, FiniteStateMachine stateMachine, int animationName) : base(entity, stateMachine, animationName)
    {
    }
}
