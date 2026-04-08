using UnityEngine;

[CreateAssetMenu(fileName = "Seed", menuName = "Scriptable Objects/Seed")]
public class Seed : ValueItem
{
    public int timeToGrow = 2;

    public override void UseItem(GameObject targetObject)
    {
        BoxCollider2D seedCollider = targetObject.GetComponent<BoxCollider2D>();
        seedCollider.enabled = false;
    }
}
