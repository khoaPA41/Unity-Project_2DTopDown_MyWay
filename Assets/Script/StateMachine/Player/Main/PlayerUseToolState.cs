using UnityEngine;

public class PlayerUseToolState : PlayerBaseState
{
    readonly int AxeBlendTreeHash = Animator.StringToHash("Axe");
    readonly int CrushBlendTreeHash = Animator.StringToHash("Crush");
    readonly int SwordBlendTreeHash = Animator.StringToHash("Sword");
    string type = "";
    string name = "";
    public PlayerUseToolState(PlayerStateMachine playerStateMachine, string type, string name) : base(playerStateMachine)
    {
        this.type = type;
        this.name = name;
    }

    public override void Enter()
    {
        AnimationBaseType();
        if (type == "Seed")
        {
            GameObject seed = playerStateMachine.PlayerFarmer.PlantSeed(name, playerStateMachine.prevDirection, playerStateMachine.itemIndex);
            playerStateMachine.toolName = "";
            playerStateMachine.toolType = "";
        }

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
        if (type != "" && (type == "Axe" || type == "Crush" || type == "Sword" || type == "Watering"))
        {
            playerStateMachine.Animator.CrossFadeInFixedTime(type, playerStateMachine.AnimatorCrossFade);
        }
    }

}
