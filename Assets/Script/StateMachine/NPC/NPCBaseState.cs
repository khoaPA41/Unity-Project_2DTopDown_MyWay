using UnityEngine;

public abstract class NPCBaseState : State
{
    protected NPCStateMachine nPCStateMachine;

    protected Vector2 direction;

    RaycastHit2D hit;

    public NPCBaseState(NPCStateMachine nPCStateMachine)
    {
        this.nPCStateMachine = nPCStateMachine;
    }


    //protected void Move(Vector2 target, float fixedDeltaTime)
    //{
    //    Vector2 currentPos = nPCStateMachine.Rigidbody2D.position;

    //    diretion = (target - currentPos).normalized;
    //    Vector2 movementDelta = diretion * nPCStateMachine.Speed * fixedDeltaTime;

    //    if (Mathf.Abs(movementDelta.x) > 0.0001f)
    //    {
    //        Vector2 dirX = new Vector2(Mathf.Sign(movementDelta.x), 0f);
    //        float castLegnth = Mathf.Abs(dirX.x) * nPCStateMachine.SkinWidth;
    //        hit = nPCStateMachine.CheckLimit.Cast(dirX, castLegnth);

    //        if (hit.collider != null)
    //        {
    //            float minDistance = hit.distance - nPCStateMachine.SkinWidth;
    //            minDistance = Mathf.Max(0, minDistance);
    //            movementDelta.x = dirX.x * minDistance;
    //        }
    //    }

    //    if (Mathf.Abs(movementDelta.y) > 0.0001f)
    //    {
    //        Vector2 dirY = new Vector2(0f, Mathf.Sign(movementDelta.y));
    //        float castLength = Mathf.Abs(dirY.y) * nPCStateMachine.SkinWidth;
    //        hit = nPCStateMachine.CheckLimit.Cast(dirY, castLength);

    //        if (hit.collider != null)
    //        {
    //            float minDistance = hit.distance * nPCStateMachine.SkinWidth;
    //            minDistance = Mathf.Max(0, minDistance);
    //            movementDelta.y = dirY.y * minDistance;
    //        }
    //    }

    //    diretion.x = movementDelta.x > 0 ? Mathf.Sign(movementDelta.x) : diretion.x;
    //    diretion.y = movementDelta.y > 0 ? Mathf.Sign(movementDelta.y) : diretion.y;
    //    nPCStateMachine.Rigidbody2D.MovePosition(currentPos + movementDelta);
    //}
    protected void Flip()
    {
        if (direction.x < 0)
        {
            nPCStateMachine.transform.localScale = new Vector3(1f, 1f, 1f);
            return;
        }

        if (direction.x > 0)
        {
            nPCStateMachine.transform.localScale = new Vector3(-1f, 1f, 1f);
            return;
        }
    }

    protected void UpdateAnimation(float deltaTime)
    {
        direction = nPCStateMachine.NavMeshAgent.velocity.normalized;
        if (direction == Vector2.zero)
        {
            nPCStateMachine.Animator.SetFloat("MovementX", 0, nPCStateMachine.AnimatorDamping, deltaTime);
            nPCStateMachine.Animator.SetFloat("MovementY", 0, nPCStateMachine.AnimatorDamping, deltaTime);
            return;
        }

        nPCStateMachine.Animator.SetFloat("MovementX", direction.x * 2, nPCStateMachine.AnimatorDamping, deltaTime);
        nPCStateMachine.Animator.SetFloat("MovementY", direction.y * 2, nPCStateMachine.AnimatorDamping, deltaTime);
        //if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        //{
        //    if (direction.x > 0)
        //    {
        //        nPCStateMachine.Animator.SetFloat("MovementX", 2, nPCStateMachine.AnimatorDamping, deltaTime);
        //        return;

        //    }

        //    if (direction.x < 0)
        //    {
        //        nPCStateMachine.Animator.SetFloat("MovementX", -2, nPCStateMachine.AnimatorDamping, deltaTime);
        //        return;
        //    }
        //}

        //else
        //{
        //    if (direction.y > 0)
        //    {
        //        nPCStateMachine.Animator.SetFloat("MovementY", 2, nPCStateMachine.AnimatorDamping, deltaTime);
        //        return;
        //    }

        //    if (direction.y < 0)
        //    {
        //        nPCStateMachine.Animator.SetFloat("MovementY", -2, nPCStateMachine.AnimatorDamping, deltaTime);
        //        return;
        //    }
        //}
    }
}
