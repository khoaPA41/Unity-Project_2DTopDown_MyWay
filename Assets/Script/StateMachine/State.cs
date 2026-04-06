using UnityEngine;

public abstract class State
{
    public abstract void Enter();
    public abstract void Tick(float deltaTime);
    public abstract void PhysicTick(float fixedDeltatime);
    public abstract void Exit();

    public float GetNormalizeTime(Animator animator, string animationTag)
    {
        AnimatorStateInfo currentState = animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextState = animator.GetNextAnimatorStateInfo(0);
        if (animator.IsInTransition(0) && currentState.IsTag(animationTag))
        {
            return currentState.normalizedTime;
        }
        else if (!animator.IsInTransition(0) && nextState.IsTag(animationTag))
        {
            return nextState.normalizedTime;
        }
        else
        {
            return 0f;
        }
    }

}
