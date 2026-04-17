using UnityEngine;

public class NPCGoHomeState : NPCBaseState
{
    int LocomotionAnimationHash = Animator.StringToHash("Locomotion");

    TargetedBy targetHome = null;
    public NPCGoHomeState(NPCStateMachine nPCStateMachine) : base(nPCStateMachine)
    {
    }

    public override void Enter()
    {
        nPCStateMachine.Animator.CrossFadeInFixedTime(LocomotionAnimationHash, nPCStateMachine.AnimatorCrossFade);
        nPCStateMachine.NavMeshAgent.isStopped = false;
        Debug.Log(nPCStateMachine.BrainController.homeTransform.position);
        nPCStateMachine.NavMeshAgent.SetDestination(nPCStateMachine.BrainController.homeTransform.position);

    }

    public override void Tick(float deltaTime)
    {
        if (!nPCStateMachine.NavMeshAgent.pathPending && nPCStateMachine.NavMeshAgent.remainingDistance <= nPCStateMachine.NavMeshAgent.stoppingDistance)
        {
            if (!nPCStateMachine.NavMeshAgent.hasPath || nPCStateMachine.NavMeshAgent.velocity.sqrMagnitude == 0f)
            {
                Debug.Log("Ve nha roi!");
                nPCStateMachine.SwitchState(new NPCIdleState(nPCStateMachine));
            }
        }
        UpdateAnimation();
    }

    public override void PhysicTick(float fixedDeltatime)
    {

    }

    public override void Exit()
    {

    }

    void UpdateAnimation()
    {
        if (diretion.x > 0)
        {
            nPCStateMachine.Animator.SetFloat("MovementX", 2);

        }

        if (diretion.x < 0)
        {
            nPCStateMachine.Animator.SetFloat("MovementX", -2);
        }

        if (diretion.y > 0)
        {
            nPCStateMachine.Animator.SetFloat("MovementY", 2);
        }

        if (diretion.y < 0)
        {
            nPCStateMachine.Animator.SetFloat("MovementY", -2);
        }
    }
}
