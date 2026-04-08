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
            if (tool.type == "Watering")
            {
                if (collision.CompareTag("Seed"))
                {
                    SeedGrow seed = collision.gameObject.GetComponent<SeedGrow>();
                    seed.watered++;
                }
            }
            else
            {
                if (collision.CompareTag("Ore"))
                {
                    spawnItem.currentDurability -= tool.efficiencyOre;
                }


                if (collision.CompareTag("Tree"))
                {
                    spawnItem.currentDurability -= tool.efficiencyWood;
                }

                if (collision.CompareTag("Vegetable"))
                {
                    spawnItem.currentDurability = 0;
                }
            }
        }
    }
}
