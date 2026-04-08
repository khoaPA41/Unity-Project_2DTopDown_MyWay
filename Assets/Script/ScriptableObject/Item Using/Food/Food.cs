using UnityEngine;

[CreateAssetMenu(fileName = "Food", menuName = "Scriptable Objects/Food")]

public class Food : ValueItem
{
    public int satiety = 3;

    public override void UseItem(GameObject targetObject)
    {
        base.UseItem(targetObject);
    }
}
