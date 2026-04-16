using UnityEngine;
[RequireComponent(typeof(PooledObject))]
public class SpawnAfterDestroy : MonoBehaviour
{

    [SerializeField] Item scriptableObject;
    [SerializeField] int durability = 20;

    [Header("For NPC")]
    public bool isHarvested { get; set; } = false;
    public NPCStateMachine targetedBy { get; set; } = null;
    public bool isAvailable => gameObject.activeInHierarchy && !isHarvested && targetedBy == null;


    PooledObject pooledObject;



    public int currentDurability { get; set; }

    void Start()
    {
        pooledObject = GetComponent<PooledObject>();
    }

    private void OnEnable()
    {
        currentDurability = durability;
    }

    void Update()
    {
        if (currentDurability <= 0)
        {
            SpawnPrefabs();
            pooledObject.Release();
        }
    }

    public void SpawnPrefabs()
    {
        ObjectPooling objectPooling = GameObject.FindGameObjectWithTag("ItemPool").GetComponent<ObjectPooling>();
        foreach (var item in scriptableObject.prefabsToSpawn)
        {
            objectPooling.GetObjectPooled(item.name, transform.position);
        }
    }

    public void Harvested()
    {
        isHarvested = true;
        currentDurability = 0;
    }
}
