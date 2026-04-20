using UnityEngine;

public class NPCFarmerState : NPCBaseState
{
    int LocomotionAnimationHash = Animator.StringToHash("Locomotion");

    SpawnAfterDestroy targetCrop = null;

    float countTimeHarvest = 0;
    public NPCFarmerState(NPCStateMachine nPCStateMachine) : base(nPCStateMachine)
    {
    }

    public override void Enter()
    {
        Debug.Log("Farmer");
        nPCStateMachine.Animator.CrossFadeInFixedTime(LocomotionAnimationHash, nPCStateMachine.AnimatorCrossFade);
        FindNextVegetable();
    }

    public override void Tick(float deltaTime)
    {
        if (targetCrop == null)
        {
            FindNextVegetable();
            return;
        }

        float distance = Vector2.Distance(nPCStateMachine.transform.position, targetCrop.transform.position);

        if (distance <= nPCStateMachine.NavMeshAgent.stoppingDistance + 0.2f)
        {
            nPCStateMachine.NavMeshAgent.isStopped = true;
            countTimeHarvest += deltaTime;
            if (countTimeHarvest >= nPCStateMachine.HarvestTime)
            {
                targetCrop.Harvested();
                targetCrop = null;
                countTimeHarvest = 0f;
                nPCStateMachine.SwitchState(new NPCFarmerState(nPCStateMachine));
            }
        }
        else
        {
            nPCStateMachine.NavMeshAgent.isStopped = false;
        }

        UpdateAnimation(deltaTime);
        Flip();
    }



    public override void Exit()
    {
        if (targetCrop != null) targetCrop.targetedBy = null;
        nPCStateMachine.NavMeshAgent.isStopped = false;
    }

    void FindNextVegetable()
    {
        SpawnAfterDestroy bestCrop = null;
        float minDistance = float.MaxValue;

        foreach (var crop in WorldTime.Instance.vegetableList)
        {
            if (crop.isAvailable)
            {
                Debug.Log(crop);
                float distance = Vector2.Distance(nPCStateMachine.transform.position, crop.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    bestCrop = crop;
                }
            }
        }

        if (bestCrop != null)
        {
            targetCrop = bestCrop;
            targetCrop.targetedBy = nPCStateMachine;
            nPCStateMachine.NavMeshAgent.isStopped = false;
            nPCStateMachine.NavMeshAgent.SetDestination(targetCrop.transform.position);
        }
        else
        {
            nPCStateMachine.SwitchState(new NPCIdleState(nPCStateMachine));
        }
    }

    public override void PhysicTick(float fixedDeltatime)
    {
    }
}
