using UnityEngine;
using UnityEngine.AI;

public class NPCStateMachine : StateMachine
{
    [Header("Physics")]
    [field: SerializeField] public Rigidbody2D Rigidbody2D { get; private set; }
    [field: SerializeField] public BoxCollider2D BoxCollider2D { get; private set; }
    [field: SerializeField] public NavMeshAgent NavMeshAgent { get; private set; }
    [field: SerializeField] public CheckLimit CheckLimit { get; private set; }
    [field: SerializeField] public float Speed { get; private set; } = 4f;
    [field: SerializeField] public float SkinWidth { get; private set; } = .1f;

    [Header("Animation")]
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public float AnimatorCrossFade { get; private set; }
    [field: SerializeField] public float AnimatorDamping { get; private set; }

    [Header("Movement")]
    [field: SerializeField] public float ChasingRange { get; private set; }
    [field: SerializeField] public float AttackRange { get; private set; }

    [Header("Pooled Object")]
    [field: SerializeField] public PooledObject PooledObject { get; private set; }

    [Header("Schedule")]
    [field: SerializeField] public Schedule Schedule { get; private set; }

    public PlayerStateMachine Player { get; private set; }

    void Start()
    {
        NavMeshAgent.updateRotation = false;
        NavMeshAgent.updateUpAxis = false;
    }
}
