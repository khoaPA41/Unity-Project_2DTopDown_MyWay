using UnityEngine;

public class CheckLimit : MonoBehaviour
{
    [SerializeField] LayerMask layerMask = 255;
    BoxCollider2D box2D;
    void Awake()
    {
        box2D = GetComponent<BoxCollider2D>();
    }

    public RaycastHit2D Cast(Vector2 direction, float castLength)
    {
        return Physics2D.BoxCast(
            box2D.bounds.center,
            box2D.bounds.size,
            0f,
            direction,
            castLength,
            layerMask
            );
    }

    private void OnDrawGizmos()
    {
        //if (box2D == null)
        //{
        //    return;
        //}
        //Color color;
        //color = Color.red;
        ////Gizmos.DrawCube(box2D.bounds.center, box2D.bounds.size);
        //if (hit.collider)
        //{
        //    color = Color.green;
        //}

        //Debug.DrawLine(box2D.bounds.center, box2D.bounds.center * direction.x * castLength, color, Time.fixedDeltaTime);
        //Debug.DrawLine(box2D.bounds.center, box2D.bounds.center * direction.y, color, Time.fixedDeltaTime);
        //Debug.DrawLine(box2D.bounds.center, box2D.bounds.center + Vector3.left * castLength, color, Time.deltaTime);
        //Debug.DrawLine(box2D.bounds.center, box2D.bounds.center + Vector3.right * castLength, color, Time.deltaTime);

    }
}
