using UnityEngine;
[RequireComponent(typeof(PooledObject))]
public class SpawnAfterDestroy : MonoBehaviour
{
    [SerializeField] Item scriptableObject;
    PooledObject pooledObject;
    public int durability = 20;

    void Start()
    {
        pooledObject = GetComponent<PooledObject>();
    }

    void Update()
    {
        if (durability <= 0)
        {
            SpawnPrefabs();
            pooledObject.Release();
        }
    }


    public void SpawnPrefabs()
    {
        ObjectPooling objectPooling = GameObject.FindGameObjectWithTag("Item").GetComponent<ObjectPooling>();
        foreach (var item in scriptableObject.prefabsToSpawn)
        {
            objectPooling.GetObjectPooled(item.name, transform.position);
        }
    }
}
