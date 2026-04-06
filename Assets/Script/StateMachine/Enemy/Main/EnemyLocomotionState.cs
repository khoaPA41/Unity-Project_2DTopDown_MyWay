using UnityEngine;

public class EnemyLocomotionState : EnemyBaseState
{
    readonly int IdleAnimationHash = Animator.StringToHash("Idle");
    float countTime = 0;
    public EnemyLocomotionState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
    }

    public override void Enter()
    {
        enemyStateMachine.Animator.CrossFadeInFixedTime(IdleAnimationHash, enemyStateMachine.AnimatorCrossFade);
    }

    public override void Tick(float deltaTime)
    {
        countTime += deltaTime;
        if (countTime >= enemyStateMachine.CountTimeIdle)
        {
            enemyStateMachine.SwitchState(new EnemyPatrolState(enemyStateMachine));
            return;
        }

        if (IsAttackRange())
        {
            enemyStateMachine.SwitchState(new EnemyAttackState(enemyStateMachine));
            return;
        }

        if (IsChasingRange())
        {
            enemyStateMachine.SwitchState(new EnemyChasingState(enemyStateMachine));
            return;
        }
    }

    public override void PhysicTick(float fixedDeltatime)
    {

    }

    public override void Exit()
    {
        countTime = 0;
    }
}
