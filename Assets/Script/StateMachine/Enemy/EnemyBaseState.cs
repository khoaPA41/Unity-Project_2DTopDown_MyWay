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

    protected void Move(float fixedDeltaTime)
    {
        Vector2 currentPos = enemyStateMachine.Rigidbody2D.position;

        // Calculate direction and movement delta 

        direction = (target - currentPos).normalized;

        Vector2 movementDelta = direction * enemyStateMachine.Speed * fixedDeltaTime;

        // Handle collision X
        if (Mathf.Abs(movementDelta.x) > 0.0001f)
        {// Get diretion of movement delta not  direction
            Vector2 dirX = new Vector2(Mathf.Sign(movementDelta.x), 0f);

            float castLength = Mathf.Abs(movementDelta.x) + enemyStateMachine.SkinWidth;

            hit = enemyStateMachine.CheckLimit.Cast(dirX, castLength);

            if (hit.collider != null)
            {
                float minDistance = hit.distance - enemyStateMachine.SkinWidth;
                minDistance = Mathf.Max(0, minDistance);
                movementDelta.x = dirX.x * minDistance;
            }
        }

        // Handle collision Y
        if (Mathf.Abs(movementDelta.y) > 0.0001f)
        {
            Vector2 dirY = new Vector2(0, Mathf.Sign(movementDelta.y));
            float castLength = Mathf.Abs(movementDelta.y) + enemyStateMachine.SkinWidth;
            hit = enemyStateMachine.CheckLimit.Cast(dirY, castLength);

            if (hit.collider != null)
            {
                float minDistance = hit.distance - enemyStateMachine.SkinWidth;
                minDistance = Mathf.Max(0, minDistance);
                movementDelta.y = dirY.y * minDistance;
            }
        }


        direction.x = movementDelta.x != 0 ? Mathf.Sign(movementDelta.x) : direction.x;
        direction.y = movementDelta.y != 0 ? Mathf.Sign(movementDelta.y) : direction.y;
        enemyStateMachine.Rigidbody2D.MovePosition(currentPos + movementDelta);
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
