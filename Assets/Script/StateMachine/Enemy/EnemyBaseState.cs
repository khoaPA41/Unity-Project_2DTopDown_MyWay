using UnityEngine;

public abstract class EnemyBaseState : State
{
    protected EnemyStateMachine enemyStateMachine { get; private set; }
    RaycastHit2D hit;
    Vector2 direction;
    public Vector2 target { get; set; }
    float randomX;
    float randomY;

    public EnemyBaseState(EnemyStateMachine enemyStateMachine)
    {
        this.enemyStateMachine = enemyStateMachine;
    }

    protected void Move()
    {
        Vector2 motion = Vector2.MoveTowards(enemyStateMachine.Rigidbody2D.position,
           target,
           enemyStateMachine.Speed * Time.fixedDeltaTime
           );

        float xDirToTarget = Mathf.Sign(target.x - enemyStateMachine.transform.position.x);
        float yDirToTarget = Mathf.Sign(target.y - enemyStateMachine.transform.position.y);
        direction.x = xDirToTarget;

        if (motion.x != 0)
        {
            Vector2 dirX = new Vector2(Mathf.Sign(motion.x), 0f);
            float castLength = Mathf.Abs(Mathf.Sign(motion.x)) + enemyStateMachine.SkinWidth;
            hit = enemyStateMachine.CheckLimit.Cast(dirX, castLength);

            if (hit.collider != null)
            {
                float minDistance = hit.distance - enemyStateMachine.SkinWidth;
                minDistance = Mathf.Max(0, minDistance);
                motion = enemyStateMachine.Rigidbody2D.position + dirX * minDistance;
            }
        }

        if (motion.y != 0)
        {
            Vector2 dirY = new Vector2(0, Mathf.Sign(motion.y));
            float castLength = Mathf.Abs(Mathf.Sign(motion.y)) + enemyStateMachine.SkinWidth;
            hit = enemyStateMachine.CheckLimit.Cast(dirY, castLength);

            if (hit.collider != null)
            {
                float minDistance = hit.distance - enemyStateMachine.SkinWidth;
                minDistance = Mathf.Max(0, minDistance);
                motion = enemyStateMachine.Rigidbody2D.position + dirY * minDistance;
            }
        }

        enemyStateMachine.Rigidbody2D.MovePosition(motion);
    }

    protected void TouchPosLimit()
    {
        /*If touch x position limit*/
        if ((enemyStateMachine.transform.position.x >= enemyStateMachine.XEndPos ||
            enemyStateMachine.transform.position.x <= enemyStateMachine.XStartPos) && (enemyStateMachine.XStartPos != 0 && enemyStateMachine.XEndPos != 0))
        {
            randomX = Random.Range(enemyStateMachine.XStartPos, enemyStateMachine.XEndPos + 1);
        }

        /*If touch y position limit*/
        if ((enemyStateMachine.transform.position.y >= enemyStateMachine.YEndPos ||
            enemyStateMachine.transform.position.y <= enemyStateMachine.YStartPos) && (enemyStateMachine.YStartPos == 0 && enemyStateMachine.YEndPos == 0)
            )
        {
            randomY = Random.Range(enemyStateMachine.YStartPos, enemyStateMachine.YEndPos + 1);
        }

        /*If touch target*/
        if ((Vector2)enemyStateMachine.transform.position == target)
        {
            GetRandomPosition();
        }
    }

    protected void GetRandomPosition() // Get new Position and direction
    {
        if (enemyStateMachine.XStartPos == 0 && enemyStateMachine.XEndPos == 0)
        {
            randomX = enemyStateMachine.Rigidbody2D.position.x;
        }
        else
        {
            randomX = Random.Range(enemyStateMachine.XStartPos, enemyStateMachine.XEndPos + 1);
        }

        if (enemyStateMachine.YStartPos == 0 && enemyStateMachine.YEndPos == 0)
        {
            randomY = enemyStateMachine.Rigidbody2D.position.y;
        }
        else
        {
            randomY = Random.Range(enemyStateMachine.YStartPos, enemyStateMachine.YEndPos + 1);
        }

        target = new Vector2(randomX, randomY);
    }

    protected bool IsChasingRange()
    {
        return ((enemyStateMachine.Player.transform.position - enemyStateMachine.transform.position).sqrMagnitude) <= enemyStateMachine.ChasingRange * enemyStateMachine.ChasingRange;
    }

    protected bool IsAttackRange()
    {
        return ((enemyStateMachine.Player.transform.position - enemyStateMachine.transform.position).sqrMagnitude) <= enemyStateMachine.AttackRange * enemyStateMachine.AttackRange;
    }

    protected void Flip()
    {

        enemyStateMachine.transform.localScale = new Vector3(direction.x, 1f, 1f);
    }

}
