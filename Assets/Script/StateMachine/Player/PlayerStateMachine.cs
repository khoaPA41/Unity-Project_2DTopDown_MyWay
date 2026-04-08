using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    [Header("UI Sytem")]
    [field: SerializeField] public GameObject SystemObject { get; private set; }

    [Header("Physics")]
    [field: SerializeField] public Rigidbody2D Rigidbody2D { get; private set; }
    [field: SerializeField] public BoxCollider2D BoxCollider2D { get; private set; }
    [field: SerializeField] public CheckLimit CheckLimit { get; private set; }
    [field: SerializeField] public float Speed { get; private set; } = 4f;
    [field: SerializeField] public float SkinWidth { get; private set; } = .1f;

    [Header("Physic For Interact")]
    [field: SerializeField] public GameObject ToolBoxObject { get; private set; }
    [field: SerializeField] public float OffsetBaseDirection { get; private set; }
    [field: SerializeField] public PlayerFarmer PlayerFarmer { get; private set; }


    [Header("Animation")]
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public float AnimatorCrossFade { get; private set; }
    [field: SerializeField] public float AnimatorDamping { get; private set; }

    [Header("Input")]
    [field: SerializeField] public InputReader InputReader { get; private set; }

    public Vector2 prevDirection = new Vector2(0f, -1f);

    [Header("Shooting Bullet")]
    [field: SerializeField] public ObjectPooling BulletPooling { get; private set; }


    public string toolType;
    public string toolName;
    public int itemIndex;
    //public string currentToolType { get; set; } = "";

    void Start()
    {
        ReturnLocomotion();
    }

    public void ReturnLocomotion()
    {
        SwitchState(new PlayerLocomotionState(this));
        return;
    }


    public void ActiveSliceCollider()
    {
        ToolBoxObject.SetActive(true);
    }

    public void ResetSliceCollider()
    {
        ToolBoxObject.SetActive(false);

    }
}
