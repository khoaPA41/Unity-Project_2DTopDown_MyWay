using UnityEngine;

public class ItemInteract : MonoBehaviour
{
    [SerializeField] ValueItem valueItem;
    Inventories inventories;
    PooledObject pooledObject;

    void Start()
    {
        pooledObject = GetComponent<PooledObject>();
        inventories = FindFirstObjectByType<Inventories>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inventories.AddItem(valueItem, 1);
            pooledObject.Release();
        }
    }
}
