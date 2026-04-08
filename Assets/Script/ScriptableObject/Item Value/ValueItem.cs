using UnityEngine;

[CreateAssetMenu(fileName = "ValueItem", menuName = "Scriptable Objects/ValueItem")]
public class ValueItem : ScriptableObject
{
    public string type;
    public string itemName;
    public int maxStack;
    public Sprite itemSprite;

    public virtual void UseItem(GameObject targetObject)
    {
        Debug.Log("Do Something!");
    }
}
