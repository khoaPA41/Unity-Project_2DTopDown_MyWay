using UnityEngine;

public class TargetedBy : MonoBehaviour
{
    public NPCStateMachine targetedBy { get; set; } = null;
    public bool isTarget { get; set; } = false;

    public void Targeted()
    {
        isTarget = true;
    }
}
