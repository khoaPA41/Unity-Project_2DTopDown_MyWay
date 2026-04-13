using UnityEngine;

public class NPCFarmerState : NPCBaseState
{
    public NPCFarmerState(NPCStateMachine nPCStateMachine) : base(nPCStateMachine)
    {
    }

    public override void Enter()
    {
        Debug.Log("Farmer");
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
