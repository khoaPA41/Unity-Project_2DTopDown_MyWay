using UnityEngine;

[CreateAssetMenu(fileName = "Tools", menuName = "Scriptable Objects/Tools")]
public class Tools : ValueItem
{
    public int durability = 10;
    public int damage = 5;
    public int efficiencyWood = 4;
    public int efficiencyOre = 5;

    public override void UseItem(GameObject targetObject)
    {
        base.UseItem(targetObject);
    }
}
