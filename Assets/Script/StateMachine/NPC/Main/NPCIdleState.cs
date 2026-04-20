using UnityEngine;

public class NPCIdleState : NPCBaseState
{
    int LocomotionAnimationHash = Animator.StringToHash("Locomotion");
    public NPCIdleState(NPCStateMachine nPCStateMachine) : base(nPCStateMachine)
    {
    }

    public override void Enter()
    {
        Debug.Log("Idle");
        nPCStateMachine.Animator.CrossFadeInFixedTime(LocomotionAnimationHash, nPCStateMachine.AnimatorCrossFade);
        //nPCStateMachine.NavMeshAgent.isStopped = true;
    }

    public override void Tick(float deltaTime)
    {
        nPCStateMachine.Animator.SetFloat("MovementX", 0, nPCStateMachine.AnimatorDamping, deltaTime);
        nPCStateMachine.Animator.SetFloat("MovementY", 0, nPCStateMachine.AnimatorDamping, deltaTime);
    }

    public override void PhysicTick(float fixedDeltatime)
    {

    }

    public override void Exit()
    {
        nPCStateMachine.NavMeshAgent.isStopped = false;
    }
}
