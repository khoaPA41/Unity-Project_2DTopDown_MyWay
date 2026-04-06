using UnityEngine;

public class PlayerLocomotionState : PlayerBaseState
{
    readonly int LocomotionBlendTreeHash = Animator.StringToHash("Locomotion");
    readonly int LocomotionXParams = Animator.StringToHash("MovementX");
    readonly int LocomotionYParams = Animator.StringToHash("MovementY");

    float speed = 0f;
    Vector2 motion;

    public PlayerLocomotionState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        playerStateMachine.Animator.CrossFadeInFixedTime(LocomotionBlendTreeHash, playerStateMachine.AnimatorCrossFade);
    }

    public override void Tick(float deltaTime)
    {

        if (playerStateMachine.InputReader.Movement == Vector2.zero)
        {
            speed = 0;
        }
        else
        {
            playerStateMachine.prevDirection.x = Mathf.Sign(playerStateMachine.InputReader.Movement.x);
            playerStateMachine.prevDirection.y = Mathf.Sign(playerStateMachine.InputReader.Movement.y);

            speed = playerStateMachine.Speed;
        }

        motion = playerStateMachine.InputReader.Movement * speed * Time.fixedDeltaTime;

        SwitchUSeToolState();
        SwitchShooting();
        UpdateAnimation(deltaTime);
        FlipCharacter();

    }

    public override void PhysicTick(float fixedDeltatime)
    {
        Move(motion);
    }

    public override void Exit()
    {

    }


    void UpdateAnimation(float deltaTime)
    {
        if (playerStateMachine.InputReader.Movement == Vector2.zero)
        {
            playerStateMachine.Animator.SetFloat(LocomotionXParams, playerStateMachine.prevDirection.x, playerStateMachine.AnimatorDamping, deltaTime);
            playerStateMachine.Animator.SetFloat(LocomotionYParams, playerStateMachine.prevDirection.y, playerStateMachine.AnimatorDamping, deltaTime);
        }
        else
        {
            playerStateMachine.Animator.SetFloat(LocomotionXParams, playerStateMachine.InputReader.Movement.x * 2, playerStateMachine.AnimatorDamping, deltaTime);
            playerStateMachine.Animator.SetFloat(LocomotionYParams, playerStateMachine.InputReader.Movement.y * 2, playerStateMachine.AnimatorDamping, deltaTime);
        }
    }

    void SwitchUSeToolState()
    {
        if (playerStateMachine.InputReader.IsAttack)
        {
            playerStateMachine.SwitchState(new PlayerUseToolState(playerStateMachine));
            //playerStateMachine.currentToolType
            return;
        }
    }

    void SwitchShooting()
    {
        if (playerStateMachine.InputReader.IsShooting)
        {
            playerStateMachine.SwitchState(new PlayerShootingState(playerStateMachine));
            return;
        }
    }


}
