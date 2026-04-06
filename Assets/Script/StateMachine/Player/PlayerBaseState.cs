using UnityEngine;

public abstract class PlayerBaseState : State
{
    protected PlayerStateMachine playerStateMachine;

    public PlayerBaseState(PlayerStateMachine playerStateMachine)
    {
        this.playerStateMachine = playerStateMachine;
    }


    protected void Move(Vector2 motion)
    {
        if (motion.x != 0)
        {
            Vector2 dirX = new Vector2(Mathf.Sign(motion.x), 0f);
            float castLength = Mathf.Abs(motion.x) + playerStateMachine.SkinWidth;
            RaycastHit2D hit = playerStateMachine.CheckLimit.Cast(dirX, castLength);

            if (hit.collider != null)
            {
                float minDistance = hit.distance - playerStateMachine.SkinWidth;
                minDistance = Mathf.Max(0, minDistance);
                motion.x = Mathf.Sign(motion.x) * minDistance;
            }
        }


        if (motion.y != 0)
        {
            Vector2 dirY = new Vector2(0f, Mathf.Sign(motion.y));
            float castLength = Mathf.Abs(motion.y) + playerStateMachine.SkinWidth;
            RaycastHit2D hit = playerStateMachine.CheckLimit.Cast(dirY, castLength);

            if (hit.collider != null)
            {
                float minDistance = hit.distance - playerStateMachine.SkinWidth;
                minDistance = Mathf.Max(0, minDistance);
                motion.y = Mathf.Sign(motion.y) * minDistance;
            }
        }

        playerStateMachine.Rigidbody2D.MovePosition(playerStateMachine.Rigidbody2D.position + motion);
    }

    protected void FlipCharacter()
    {
        if (playerStateMachine.InputReader.Movement.x != 0)
        {
            Vector3 direction = new Vector3(1f, 1f, 1f);
            if (playerStateMachine.InputReader.Movement.x > 0)
            {
                direction.x *= 1;
            }
            if (playerStateMachine.InputReader.Movement.x < 0)
            {
                direction.x *= -1;
            }
            playerStateMachine.transform.localScale = direction;
        }
    }

    protected Vector2 GetTarget()
    {
        if (playerStateMachine.InputReader.Movement != Vector2.zero)
        {
            return playerStateMachine.InputReader.Movement;
        }

        return playerStateMachine.prevDirection;
    }
}
