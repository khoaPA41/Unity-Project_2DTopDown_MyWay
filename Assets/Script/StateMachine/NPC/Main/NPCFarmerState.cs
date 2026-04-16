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

        UpdateAnimation();

        //if (!nPCStateMachine.NavMeshAgent.pathPending && nPCStateMachine.NavMeshAgent.remainingDistance <= nPCStateMachine.NavMeshAgent.stoppingDistance)
        //{
        //    if (!nPCStateMachine.NavMeshAgent.hasPath || nPCStateMachine.NavMeshAgent.velocity.sqrMagnitude == 0)
        //    {
        //        Debug.Log("Farm");
        //    }
        //}
    }



    public override void Exit()
    {
        if (targetCrop != null) targetCrop.targetedBy = null;
        nPCStateMachine.NavMeshAgent.isStopped = false;
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

    void FindNextVegetable()
    {
        SpawnAfterDestroy bestCrop = null;
        float minDistance = float.MaxValue;

        foreach (var crop in GameManagers.Instance.vegetableList)
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
            Debug.Log("Ve nha");
        }
    }

    public override void PhysicTick(float fixedDeltatime)
    {
    }
}
