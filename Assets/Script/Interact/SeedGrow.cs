using UnityEngine;

public class SeedGrow : MonoBehaviour
{
    [SerializeField] GameObject germinateObject;
    [SerializeField] GameObject growObject;
    [SerializeField] PooledObject seedRelease;
    [SerializeField] Seed seed;
    [SerializeField] BoxCollider2D boxMain;
    public int watered { get; set; } = 0;

    ObjectPooling itemPool;

    void Start()
    {
        itemPool = GameObject.FindGameObjectWithTag("Vegetable").GetComponent<ObjectPooling>();
    }

    private void OnDisable()
    {
        Reset();
    }

    void Update()
    {
        if (watered == 1)
        {
            Debug.Log(watered);
            germinateObject.SetActive(true);
        }

        if (watered == seed.timeToGrow)
        {
            string[] parts = seed.itemName.Split('_');
            itemPool.GetObjectPooled(parts[0], this.transform.position);
            seedRelease.Release();
        }
    }


    void Reset()
    {
        watered = 0;
        boxMain.enabled = true;
        germinateObject.SetActive(false);
    }
}
