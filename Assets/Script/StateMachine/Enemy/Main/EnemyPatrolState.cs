using UnityEngine;

public class EnemyPatrolState : EnemyBaseState
{
    readonly int MoveAnimationHash = Animator.StringToHash("Run");
    float countTime = 0f;
    public EnemyPatrolState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
    }

    public override void Enter()
    {
        GetRandomPosition();
        countTime = 0f;
        enemyStateMachine.Animator.CrossFadeInFixedTime(MoveAnimationHash, enemyStateMachine.AnimatorCrossFade);
    }

    public override void Tick(float deltaTime)
    {
        countTime += deltaTime;
        if (countTime >= enemyStateMachine.CountTimePatrol)
        {
            enemyStateMachine.SwitchState(new EnemyLocomotionState(enemyStateMachine));
            return;
        }
        TouchPosLimit();
    }

    public override void PhysicTick(float fixedDeltatime)
    {
        Move(fixedDeltatime);
        Flip();
    }

    public override void Exit()
    {
        countTime = 0;
    }
}
