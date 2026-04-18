using UnityEngine;

public class NPCGoHomeState : NPCBaseState
{
    int LocomotionAnimationHash = Animator.StringToHash("Locomotion");

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
                WorldTime.Instance.isGoOut = false;
                nPCStateMachine.PooledObject.Release();
                //nPCStateMachine.SwitchState(new NPCIdleState(nPCStateMachine));

            }
        }
        UpdateAnimation(deltaTime);
        Flip();
    }

    public override void PhysicTick(float fixedDeltatime)
    {

    }

    public override void Exit()
    {
        nPCStateMachine.NavMeshAgent.isStopped = true;
    }

}
