using UnityEngine;

public class NPCGoHomeState : NPCBaseState
{
    public NPCGoHomeState(NPCStateMachine nPCStateMachine) : base(nPCStateMachine)
    {
    }

    public override void Enter()
    {
        Debug.Log("Go Home");
    }

    public override void Tick(float deltaTime)
    {

    }

    public override void PhysicTick(float fixedDeltatime)
    {

    }

    public override void Exit()
    {

    }
}
