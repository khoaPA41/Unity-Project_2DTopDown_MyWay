using UnityEngine;

[CreateAssetMenu(fileName = "Seed", menuName = "Scriptable Objects/Seed")]
public class Seed : ValueItem
{
    public int timeToGrow = 2;

    public override void UseItem(GameObject player)
    {
        base.UseItem(player);
    }
}
