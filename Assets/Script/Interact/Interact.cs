using UnityEngine;


public class Interact : MonoBehaviour
{
    [SerializeField] PlayerSelectItem selectItem;
    public ValueItem item { get; set; }
    Tools tool;
    SpawnAfterDestroy spawnItem;
    PooledObject pooled;


    void OnEnable()
    {
        item = Inventories.Instance.inventoriesList[selectItem.currentIndex].itemData;
        tool = item as Tools;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        spawnItem = collision?.GetComponent<SpawnAfterDestroy>();
        pooled = collision?.GetComponent<PooledObject>();

        if (tool != null)
        {
            if (collision.CompareTag("Ore"))
            {
                spawnItem.durability -= tool.efficiencyOre;
            }


            if (collision.CompareTag("Tree"))
            {
                spawnItem.durability -= tool.efficiencyWood;
            }

            if (collision.CompareTag("Vegetable"))
            {
                spawnItem.durability = 0;
            }
        }
    }
}
