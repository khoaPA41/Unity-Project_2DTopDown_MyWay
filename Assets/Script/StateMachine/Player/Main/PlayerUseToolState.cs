using System;
using UnityEngine;

public class PlayerUseToolState : PlayerBaseState
{

    readonly int AxeBlendTreeHash = Animator.StringToHash("Axe");
    readonly int CrushBlendTreeHash = Animator.StringToHash("Crush");
    readonly int SwordBlendTreeHash = Animator.StringToHash("Sword");

    public PlayerUseToolState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        AnimationBaseType();
    }

    public override void Tick(float deltaTime)
    {
        if (!playerStateMachine.InputReader.IsAttack)
        {
            playerStateMachine.ReturnLocomotion();
        }

        UpdateColliderByDirection();
    }

    public override void PhysicTick(float fixedDeltatime)
    {

    }

    public override void Exit()
    {

    }


    void UpdateColliderByDirection()
    {

        if (playerStateMachine.InputReader.Movement.x != 0)
        {
            playerStateMachine.ToolBoxObject.GetComponent<BoxCollider2D>().offset = new Vector2(.3f, 0f);
        }

        if (playerStateMachine.InputReader.Movement.y > 0)
        {
            playerStateMachine.ToolBoxObject.GetComponent<BoxCollider2D>().offset = new Vector2(0f, .3f);
        }

        if (playerStateMachine.InputReader.Movement.y < 0)
        {
            playerStateMachine.ToolBoxObject.GetComponent<BoxCollider2D>().offset = new Vector2(0f, -.3f);
        }
    }


    void AnimationBaseType()
    {
        if (string.Equals(playerStateMachine.toolType, "Axe", StringComparison.OrdinalIgnoreCase))
        {
            playerStateMachine.Animator.CrossFadeInFixedTime(AxeBlendTreeHash, playerStateMachine.AnimatorCrossFade);
        }

        if (string.Equals(playerStateMachine.toolType, "Crush", StringComparison.OrdinalIgnoreCase))
        {
            playerStateMachine.Animator.CrossFadeInFixedTime(CrushBlendTreeHash, playerStateMachine.AnimatorCrossFade);
        }

        if (string.Equals(playerStateMachine.toolType, "Sword", StringComparison.OrdinalIgnoreCase))
        {
            playerStateMachine.Animator.CrossFadeInFixedTime(SwordBlendTreeHash, playerStateMachine.AnimatorCrossFade);
        }

        if (string.Equals(playerStateMachine.toolType, "", StringComparison.OrdinalIgnoreCase))
        {
            playerStateMachine.ReturnLocomotion();
        }
    }

}
