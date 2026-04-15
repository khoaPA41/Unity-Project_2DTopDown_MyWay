using UnityEngine;

public class NPCFarmerState : NPCBaseState
{
    int LocomotionAnimationHash = Animator.StringToHash("Locomotion");

    Transform target;
    public NPCFarmerState(NPCStateMachine nPCStateMachine, Transform target) : base(nPCStateMachine)
    {
        this.target = target;
    }

    public override void Enter()
    {
        nPCStateMachine.Animator.CrossFadeInFixedTime(LocomotionAnimationHash, nPCStateMachine.AnimatorCrossFade);
        nPCStateMachine.NavMeshAgent.isStopped = false;
        nPCStateMachine.NavMeshAgent.SetDestination(target.position);
    }

    public override void Tick(float deltaTime)
    {
        UpdateAnimation();

        if (!nPCStateMachine.NavMeshAgent.pathPending && nPCStateMachine.NavMeshAgent.remainingDistance <= nPCStateMachine.NavMeshAgent.stoppingDistance)
        {
            if (!nPCStateMachine.NavMeshAgent.hasPath || nPCStateMachine.NavMeshAgent.velocity.sqrMagnitude == 0)
            {
                Debug.Log("Farm");
            }
        }
    }

    public override void PhysicTick(float fixedDeltatime)
    {
        //Move(target.position, fixedDeltatime);
    }

    public override void Exit()
    {
        nPCStateMachine.NavMeshAgent.isStopped = true;
    }

    void UpdateAnimation()
    {
        Debug.Log(diretion);
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
