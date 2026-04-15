using UnityEngine;

public class NPCGoOutState : NPCBaseState
{
    int LocomotionAnimationHash = Animator.StringToHash("Locomotion");
    public NPCGoOutState(NPCStateMachine nPCStateMachine) : base(nPCStateMachine)
    {
    }

    public override void Enter()
    {
        Debug.Log("Go Out");
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
