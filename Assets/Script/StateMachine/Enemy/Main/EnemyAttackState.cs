using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    readonly int AttackAnimationHash = Animator.StringToHash("Attack");
    readonly string AttackTag = "Attack";
    public EnemyAttackState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
    }

    public override void Enter()
    {
        enemyStateMachine.Animator.CrossFadeInFixedTime(AttackAnimationHash, enemyStateMachine.AnimatorCrossFade);
    }

    public override void Tick(float deltaTime)
    {
        float normalize = GetNormalizeTime(enemyStateMachine.Animator, AttackTag);

        if (normalize > 0.7f && normalize <= 1f)
        {
            enemyStateMachine.SwitchState(new EnemyLocomotionState(enemyStateMachine));
        }

        if (!IsAttackRange())
        {
            enemyStateMachine.SwitchState(new EnemyLocomotionState(enemyStateMachine));
        }

    }

    public override void PhysicTick(float fixedDeltatime)
    {

    }

    public override void Exit()
    {

    }
}
