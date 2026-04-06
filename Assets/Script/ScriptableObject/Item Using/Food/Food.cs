using UnityEngine;

[CreateAssetMenu(fileName = "Food", menuName = "Scriptable Objects/Food")]

public class Food : ValueItem
{
    int satiety = 3;

    public override void UseItem(GameObject player)
    {
        base.UseItem(player);
    }
}
