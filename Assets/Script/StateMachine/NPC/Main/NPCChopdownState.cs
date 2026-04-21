using UnityEngine;

public class NPCChopdownState : NPCBaseState
{
    int LocomotionAnimationHash = Animator.StringToHash("Locomotion");
    SpawnAfterDestroy target = null;
    float countTimeChopdown = 0;
    public NPCChopdownState(NPCStateMachine nPCStateMachine) : base(nPCStateMachine)
    {
    }

    public override void Enter()
    {
        nPCStateMachine.Animator.CrossFadeInFixedTime(LocomotionAnimationHash, nPCStateMachine.AnimatorCrossFade);
        FindTree();
    }

    public override void Tick(float deltaTime)
    {
        if (target == null)
        {
            FindTree();
            return;
        }

        float distance = Vector2.Distance(nPCStateMachine.transform.position, target.transform.position);

        if (distance <= nPCStateMachine.NavMeshAgent.stoppingDistance + .2f)
        {
            nPCStateMachine.NavMeshAgent.isStopped = true;
            countTimeChopdown += deltaTime;

            if (countTimeChopdown >= nPCStateMachine.HarvestTime)
            {
                target.Harvested();
                target = null;
                countTimeChopdown = 0;
                nPCStateMachine.SwitchState(new NPCChopdownState(nPCStateMachine));
            }
            else
            {
                nPCStateMachine.NavMeshAgent.isStopped = false;
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

    }

    void FindTree()
    {
        SpawnAfterDestroy bestTree = null;
        float minDistance = float.MaxValue;

        foreach (var tree in WorldTime.Instance.treeList)
        {
            if (tree.isAvailable)
            {
                float distance = Vector2.Distance(nPCStateMachine.transform.position, tree.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    bestTree = tree;
                }
            }
        }

        if (bestTree != null)
        {
            target = bestTree;
            target.targetedBy = nPCStateMachine;
            nPCStateMachine.NavMeshAgent.isStopped = false;
            nPCStateMachine.NavMeshAgent.SetDestination(target.transform.position);
        }
        else
        {
            nPCStateMachine.SwitchState(new NPCIdleState(nPCStateMachine));
        }
    }
}
