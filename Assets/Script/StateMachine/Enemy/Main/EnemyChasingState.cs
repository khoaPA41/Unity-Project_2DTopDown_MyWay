using UnityEngine;

public class EnemyChasingState : EnemyBaseState
{
    readonly int MoveAnimationHash = Animator.StringToHash("Run");
    public EnemyChasingState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
    }

    public override void Enter()
    {
        enemyStateMachine.Animator.CrossFadeInFixedTime(MoveAnimationHash, enemyStateMachine.AnimatorCrossFade);
    }

    public override void Tick(float deltaTime)
    {
        if (IsAttackRange())
        {
            enemyStateMachine.SwitchState(new EnemyAttackState(enemyStateMachine));
            return;
        }

        if (!IsChasingRange())
        {
            enemyStateMachine.SwitchState(new EnemyLocomotionState(enemyStateMachine));
            return;
        }

        Flip();
    }

    public override void PhysicTick(float fixedDeltatime)
    {
        target = enemyStateMachine.Player.transform.position;
        Move();
    }

    public override void Exit()
    {

    }
}
