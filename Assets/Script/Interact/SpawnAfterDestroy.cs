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
        ObjectPooling objectPooling = GameObject.FindGameObjectWithTag("Wood").GetComponent<ObjectPooling>();
        objectPooling.GetObjectPooled(scriptableObject.prefabsToSpawn.name, transform.position);
    }
}
