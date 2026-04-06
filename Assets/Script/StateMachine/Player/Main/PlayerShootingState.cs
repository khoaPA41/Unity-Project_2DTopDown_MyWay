public class PlayerShootingState : PlayerBaseState
{

    readonly string bulletName = "FireBall";

    float countTime = .5f;

    public PlayerShootingState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {

    }

    public override void Enter()
    {
        PooledObject bullet = playerStateMachine.BulletPooling.GetObjectPooled(bulletName, playerStateMachine.transform.position);
        bullet.GetComponent<Bullet>().diretion = GetTarget();
    }

    public override void Tick(float deltaTime)
    {
        countTime -= deltaTime;
        if (countTime <= 0f)
        {
            playerStateMachine.ReturnLocomotion();
        }
    }

    public override void PhysicTick(float fixedDeltatime)
    {

    }

    public override void Exit()
    {

    }
}
